using System;
using System.IO;
using System.Net;
using System.Text;

namespace Magentaize.Net.LMCL.Utilities
{
    public class Http
    {
        public static string GetContentFromUri(Uri url)
        {
            var stream = Network.GetHttpResponseStreamFromUri(url);
            if (stream != null)
                return (new StreamReader(stream,Encoding.Default).ReadToEnd());
            else
                return $"-1";
        }
        public static string GetContentFromUri(string url)
        {
           return GetContentFromUri(new Uri(url));
        }

        public static void GetFileFromUri(Uri url)
        {
            var stream = Network.GetHttpResponseStreamFromUri(url);
            if (stream != null)
            {
                
            }
        }
    }
}