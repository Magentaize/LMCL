// via eMZi.Gaming.Minecraft

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Magentaize.Net.LMCL.Utilities
{
    /// <summary>
    /// Represents retrieved Minecraft Server information.
    /// </summary>
    public sealed class MinecraftServerInfo
    {
        /// <summary>
        /// Gets the server's MOTD.
        /// </summary>
        public string ServerMotd { get; private set; }

        /// <summary>
        /// Gets the server's MOTD converted into HTML.
        /// </summary>
        public string ServerMotdHtml { get; }

        /// <summary>
        /// Gets the server's Minecraft version.
        /// </summary>
        public string MinecraftVersion { get; private set; }

        /// <summary>
        /// Gets the server's max player count.
        /// </summary>
        public int MaxPlayerCount { get; private set; }

        /// <summary>
        /// Gets the server's current player count.
        /// </summary>
        public int CurrentPlayerCount { get; private set; }

        /// <summary>
        /// Gets HTML colors associated with specific formatting codes.
        /// </summary>
        public static Dictionary<char, string> MinecraftColors
        {
            get
            {
                return new Dictionary<char, string>()
                {
                    { '0', "#000000" },
                    { '1', "#0000AA" },
                    { '2', "#00AA00" },
                    { '3', "#00AAAA" },
                    { '4', "#AA0000" },
                    { '5', "#AA00AA" },
                    { '6', "#FFAA00" },
                    { '7', "#AAAAAA" },
                    { '8', "#555555" },
                    { '9', "#5555FF" },
                    { 'a', "#55FF55" },
                    { 'c', "#FF5555" },
                    { 'd', "#FF55FF" },
                    { 'f', "#FFFFFF" }
                };
            }
        }

        /// <summary>
        /// Gets HTML styles associated with specific formatting codes.
        /// </summary>
        public static Dictionary<char,string> MinecraftStyles
        {
            get
            {
                return new Dictionary<char, string>()
                {
                    { 'k', "none;font-weight:normal;font-style:normal" },
                    { 'm', "line-through;font-weight:normal;font-style:normal" },
                    { 'l', "none;font-weight:900;font-style:normal" },
                    { 'n', "underline;font-weight:normal;font-style:normal;" },
                    { 'o', "none;font-weight:normal;font-style:italic;" },
                    { 'r', "none;font-weight:normal;font-style:normal;color:#FFFFFF;" }
                };
            }
        }

        ///<summary>
        ///Creates a new instance of <see cref="MinecraftServerInfo"/> with specified values.
        ///</summary>
        /// <param name="motd">Server's MOTD</param>
        /// <param name="maxplayers">Server's max player count</param>
        /// <param name="playercount">Server's current player count</param>
        /// <param name="version">Server's Minecraft version</param>
        private MinecraftServerInfo(string motd,int maxPlayers,int playerCount,string mcVersion)
        {
            ServerMotd = motd;
            MaxPlayerCount = maxPlayers;
            CurrentPlayerCount = playerCount;
            MinecraftVersion = mcVersion;
        }

        /// <summary>
        /// Gets the server's MOTD formatted as HTML
        /// </summary>
        /// <returns>HTML-formatted MOTD</returns>
        private string MotdHtml()
        {
            Regex regex = new Regex("§([k-oK-O])(.*?)(§[0-9a-fA-Fk-oK-OrR]|$)");
            string s = ServerMotd;
            while(regex.IsMatch(s))
                s = regex.Replace(s, m =>
                   {
                       string ast = "text-decoration:" + MinecraftStyles[m.Groups[1].Value[0]];
                       string html = "<span style=\"" + ast + "\">" + m.Groups[2].Value + "</span>" + m.Groups[3].Value;
                       return html;
                   });
            regex = new Regex("§([0-9a-fA-F])(.*?)(§[0-9a-fA-FrR]|$)");
            while (regex.IsMatch(s))
                s = regex.Replace(s, m =>
                   {
                       string ast = "color:" + MinecraftColors[m.Groups[1].Value[0]];
                       string html = "<span style=\"" + ast + "\">" + m.Groups[2].Value + "</span>" + m.Groups[3].Value;
                       return html;
                   });
            return s;
        }

        /// <summary>
        /// Gets the information from specified server
        /// </summary>
        /// <param name="ipoint">IP and Port of the server to get information from</param>
        /// <returns>A <see cref="MinecraftServerInfo"/> instance with retrieved data</returns>
        /// <exception cref="Exception">Upon failure, throws exception with descriptive information and InnerException with details</exception>
        public static MinecraftServerInfo GetServerInfo(IPEndPoint ipoint)
        {
            if (ipoint == null)
            {
                return null;
                throw new ArgumentNullException("IP and point can't be null.");
            }
            try
            {
                string[] packetData = null;
                using (TcpClient tcpClint = new TcpClient())
                {
                    tcpClint.Connect(ipoint);
                    using (NetworkStream netStream = tcpClint.GetStream())
                    {
                        netStream.Write(new byte[] { 0xFE, 0x01 }, 0, 2);
                        byte[] buf = new byte[2074];
                        int br = netStream.Read(buf, 0, buf.Length);
                        if (buf[0] != 0xFF)
                            throw new InvalidDataException("Received invalid packet.");
                        string packet = Encoding.BigEndianUnicode.GetString(buf, 3, br - 3);
                        if (!packet.StartsWith("§"))
                            throw new InvalidDataException("Received invalid data.");
                        packetData = packet.Split('\0');
                        netStream.Close();
                    }
                    tcpClint.Close();
                }
                return new MinecraftServerInfo(packetData[3], int.Parse(packetData[5]), int.Parse(packetData[4]), packetData[2]);
            }
            catch (SocketException)
            {
                //MessageBox.Show("Connected server failed")
                //Throw New Exception("There was a connection problem, look into InnerException for details", ex)
            }
            catch (InvalidDataException)
            {

            }
            catch (Exception)
            {

            }
            return null;
        }

        /// <summary>
        /// Gets the information from specified server
        /// </summary>
        /// <param name="ip">IP of the server to get info from</param>
        /// <param name="port">Port of the server to get info from</param>
        /// <returns>A <see cref="MinecraftServerInfo"/> instance with retrieved data</returns>
        /// <exception cref="Exception">Upon failure, throws exception with descriptive information and InnerException with details</exception>
        //public static MinecraftServerInfo GetServerInfo(IPAddress host, int port)
        //{
        //    return GetServerInfo(host, 25565);
        //}

        //public static MinecraftServerInfo GetServerInfo(string host,int port)
        //{
        //    IPAddress ip = Dns.GetHostAddresses(host)[0];
        //    return GetServerInfo(new IPEndPoint(ip, port));
        //}

    }
}
