using System;
using System.IO;
using System.Text;

namespace Magentaize.Net.LMCL.Json
{
    public class JObject:JContainer
    {
        private static JObject Serialize(string json)
        {
            return JReader.ReadJObject(JReader.JReaderType.IsString, json);
        }

        private static JObject Serialize(Stream json)
        {
            return JReader.ReadJObject(JReader.JReaderType.IsStream, json);
        }

        public static JObject Parse(string json)
        {
            return Serialize(json);
        }
            
        public static JObject Parse(Stream json)
        {
            if (json is MemoryStream)
                return Serialize((MemoryStream) json);
            else
                return null;
        }
    }
}