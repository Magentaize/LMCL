using System;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace Magentaize.Net.LMCL.Core
{
    public class LaunchCore
    {
        /// <summary>
        /// 默认附加参数
        /// </summary>
        private static readonly string[] DefaultParameter =
        {
            @"-Dfml.ignoreInvalidMinecraftCertificates=true",
            @"-Dfml.ignorePatchDiscrepancies=true"
        };

        /// <summary>
        /// 生成Minecraft启动命令行参数
        /// </summary>
        /// <param name="version">欲启动的Minecraft版本</param>
        /// <param name="avaliableMemory">设定JVM可用内存大小</param>
        /// <param name="username">Minecraft玩家用户名</param>
        /// <returns></returns>
        public static string GetLaunchArg(string version, int avaliableMemory, string username)
        {
            return GetLaunchArg(version, avaliableMemory, username, String.Empty);
        }

        /// <summary>
        /// 生成Minecraft启动命令行参数
        /// </summary>
        /// <param name="version">欲启动的Minecraft版本</param>
        /// <param name="avaliableMemory">设定JVM可用内存大小</param>
        /// <param name="username">Minecraft玩家用户名</param>
        /// <param name="customParameter">用户自定义参数</param>
        /// <returns></returns>
        public static string GetLaunchArg(string version, int avaliableMemory, string username, string customParameter)
        {
            if (avaliableMemory == 0)
            {
                throw new Exception("JVM memory cannot be 0.");
            }

            var result = new StringBuilder();

            foreach (var VARIABLE in DefaultParameter)
            {
                result.Append(VARIABLE + Convert.ToChar(32));
            }
            var customParameters = customParameter.Split(Convert.ToChar(32));
            foreach (var VARIABLE in customParameters)
            {
                result.Append(VARIABLE + Convert.ToChar(32));
            }

            if (new DirectoryInfo(Environment.CurrentDirectory + @"\natives").Exists == false)
            {
                UncompressNatives(version);
            }
            result.Append(@"-Djava.library.path=""" + Environment.CurrentDirectory + @"\.minecraft\natives"" ");
            result.Append(@"-Xmx" + avaliableMemory.ToString() + @"M -cp ");


        }

        public static void UncompressNatives(string version)
        {
            
        }

        /// <summary>
        /// 获得当前目录中给定版本的cp参数
        /// </summary>
        /// <param name="version">欲启动的Minecraft版本</param>
        /// <returns></returns>
        public static string[] GetCpArg(string version)
        {
            return GetCpArg(version, Environment.CurrentDirectory);
        }

        /// <summary>
        /// 获得指定目录中给定版本的cp参数
        /// </summary>
        /// <param name="version">欲启动的Minecraft版本</param>
        /// <param name="path">设定目录</param>
        /// <returns></returns>
        public static string[] GetCpArg(string version, string path)
        {
            path = Path.Combine(path, @"versions", version);
            if (new DirectoryInfo(path).Exists == false)
            {
                throw new Exception("The target version is not exist");
            }

            var json = JObject.Parse(File.ReadAllText(Path.Combine(path, version + @".json")));

            return GetCpArgBase(json);
        }

        public static string[] GetCpArgBase(JObject json)
        {
            
        }
    }
}