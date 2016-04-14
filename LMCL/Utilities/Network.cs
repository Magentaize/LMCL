using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Magentaize.Net.LMCL.Utilities
{
    public class Network:HttpClient
    {
        public Network()
        {
            DefaultRequestHeaders.Add("UserAgent", "LMCL Network Manager/" + GetVersion.AssemblyVersion());
        }

        public static async Task<string> GetStringFromUrl(string url)
        {
            Network network = new Network();
            return await network.GetStringAsync(url);
        }

        public static Stream GetHttpResponseStreamFromUri(Uri url)
        {
            var httpReq = WebRequest.CreateHttp(url);
            httpReq.UserAgent = "LMCL Network Manager/" + GetVersion.AssemblyVersion();
            httpReq.Method = "GET";

            try
            {
                var httpResp = (HttpWebResponse)httpReq.GetResponse();
                return httpResp.GetResponseStream();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Stream GetHttpResponseStreamFromUri(string url)
        {
            return GetHttpResponseStreamFromUri(new Uri(url));
        }
    }
}