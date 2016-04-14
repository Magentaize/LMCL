using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Magentaize.Net.LMCL.Model
{
    public class LibraryItemName
    {
        public string Type { get; set; }

        public string Company { get; set; }

        public string Product { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public string Natives { get; set; }

        /// <summary>
        /// Like "lwjgl-platform-2.9.2-nightly-20140822-natives-linux"
        /// </summary>
        /// <param name="type"></param>
        /// <param name="company"></param>
        /// <param name="product"></param>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <param name="natives"></param>
        public LibraryItemName(string type, string company, string product, string name, string version, string natives)
        {
            Type = type;
            Company = company;
            Product = product;
            Name = name;
            Version = version;
            Natives = natives;
        }

        /// <summary>
        /// Like "lwjgl-platform-2.9.2-nightly-20140822-natives-linux"
        /// </summary>
        /// <param name="type"></param>
        /// <param name="company"></param>
        /// <param name="product"></param>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <param name="natives"></param>
        public LibraryItemName(string type, string company, string product, string name, string version, JObject natives)
            : this(type, company, product, name, version, natives["windows"].ToString())
        {
        }

        /// <summary>
        /// Like "net.java.jinput:jinput:2.0.5"
        /// </summary>
        /// <param name="type"></param>
        /// <param name="company"></param>
        /// <param name="product"></param>
        /// <param name="name"></param>
        /// <param name="version"></param>
        public LibraryItemName(string type, string company, string product, string name, string version)
            : this(type, company, product, name, version, string.Empty)
        {
        }

        /// <summary>
        /// Like "io.netty:netty-all:4.0.10.Final"
        /// </summary>
        /// <param name="type"></param>
        /// <param name="product"></param>
        /// <param name="name"></param>
        /// <param name="version"></param>
        public LibraryItemName(string type, string product, string name, string version)
            : this(type, string.Empty, product, name, version)
        {
        }

        /// <summary>
        /// Like "java3d:vecmath:1.3.1"
        /// </summary>
        /// <param name="product">The first part</param>
        /// <param name="name">The middle part</param>
        /// <param name="version">The lastest part</param>
        public LibraryItemName(string product, string name, string version) : this(string.Empty, product, name, version)
        {
        }

        public static LibraryItemName Parse(string item)
        {
            var temp = item.Split(':');
            if (temp.Length != 3)
            {
                throw new Exception("It's not a library string");
            }
            var temp2 = temp[0].Split('.');

            LibraryItemName result;
            switch (temp2.Length)
            {
                case 1:
                    result = new LibraryItemName(temp2[0],temp[1],temp[2]);
                    break;
                case 2:
                    result = new LibraryItemName(temp2[0], temp2[1], temp[1], temp[2]);
                    break;
                case 3:
                    result = new LibraryItemName(temp2[0], temp2[1],temp2[2], temp[1], temp[2]);
                    break;
                default: throw new Exception("It's not a library string");
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="natives">Target platform natives JObject</param>
        /// <returns></returns>
        public static LibraryItemName Parse(string item, JObject natives)
        {
            var result = Parse(item);
            result.Natives = natives["windows"].ToString();
            return result;
        }

        /// <summary>
        /// 将当前 LibraryItem 对象转换为相对路径
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var fileName = Name + '-' + Version + '-' + Natives + @".jar";
            return Path.Combine(Type, Company, Product, Path.Combine(Name, Version, fileName));
        }
    }
}