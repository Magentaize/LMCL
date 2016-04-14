using System.Collections;
using System.Linq.Expressions;

namespace Magentaize.Net.LMCL.Json
{
    public class JReader
    {
        public enum JReaderType
        {
            IsString,
            IsStream,
        }

        public static JObject ReadJObject(JReaderType type, object json)
        {
            //var result = new JObject();

            switch (type)
            {
                case JReaderType.IsString:
                {
                    return ReadNext((string) json);
                }
                default:
                    return null;
            }
        }

        private static JObject ReadNext(string json)
        {
            var result=new JObject();
            
            var endJson = false;
            var stack=new Stack();

            for (int i = 0; i <= json.Length; i++)
            {
                var str = json[i];
                while (!endJson)
                {
                    return result;
                }
            }

            return null;
        }
    }
}