using Magentaize.Net.LMCL;
using Magentaize.Net.LMCL.Helper;
using Magentaize.Net.LMCL.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Magentaize.Net.LMCL.Properties;

namespace LMCL
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LauncherUpdateInitialize();
        }

        public static async Task<string> Fun(string url)
        {
            Thread.Sleep(3000);
            string a = await Network.GetStringFromUrl(url);
            return a;
        }

        public static async void Fun2()
        {
            MessageBox.Show(await Fun(@"http://www.baidu.com/"));
        }

        public static void LauncherUpdateInitialize()
        {
           
            //string respVersion = string.Empty;
            //string url = Cls.URL.CheckLauncherUpdate;

            //LmclHttpWebRequest req = new LmclHttpWebRequest(@"https://files.ime.moe/tools/launcherver.html");


            //HttpWebResponse response = (HttpWebResponse)req.GetResponse();

            /*Try

        httpReq = CType(WebRequest.Create(httpURL), HttpWebRequest)

        httpReq.Method = "GET"

        httpResp = CType(httpReq.GetResponse(), HttpWebResponse)

        httpReq.KeepAlive = False

        Dim reader As StreamReader = New StreamReader(httpResp.GetResponseStream, System.Text.Encoding.Default)

        respVersion = reader.ReadToEnd()

        NetStatus = True

    Catch ex As Exception

        CopyExpectionToClipboard(ex.ToString)

        MessageBox.Show("Network Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        MessageBox.Show("Enable offline mode", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)

        My.Settings.Verify_APP = True

    End Try*/
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //MinecraftServerInfo a = MinecraftServerInfo.GetServerInfo(GlobalCls.hostServer);
            //MessageBox.Show(a.MinecraftVersion);

            //   string ip = null;
            //   IPHostEntry iph = Dns.GetHostEntry("mcbbs-play.com");
            //   foreach (IPAddress ip1 in iph.AddressList)
            //   {
            //       ip = ip1.ToString();
            //       break; // TODO: might not be correct. Was : Exit For
            //   }
            //MinecraftServerInfo a = null;
            //   IPAddress c = null;
            //   c = IPAddress.Parse(ip);
            //   IPEndPoint b = new IPEndPoint(c, 25565);
            //   a = MinecraftServerInfo.GetServerInfo(b);
            //   MessageBox.Show(a.MinecraftVersion);

            //ZipLib.UnZip(@"D:\Backup\少女战士2.zip", @"D:\1\1");

            //string str = @"C:\Users\PeoLeser\Documents\Tencent Files\877048764\FileRecv\Locale.Emulator.2.0.1.0.zip";
            //Compression.UnZip(str, @"D:\1\1\");
            Fun2();
        }
    }
}
