using Magentaize.Net.LMCL.JsonLite.Linq;
using System;
using Magentaize.Net.LMCL.Utilities;

namespace Magentaize.Net.LMCL.Model
{
    public class LibraryItemName
    {
        private const string ExtensionName = @".jar";

        private const char SubNameLink = '-';

        /// <summary>
        /// 获取当前 LibraryItem 对象的文件名属性
        /// </summary>
        public string FileName => Name + ExtensionName;

        public string Type { get; set; }

        public string Company { get; set; }

        public string Product { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public string Natives { get; set; }

        public bool IsNatives { get; set; }

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

            switch (temp2.Length)
            {
                case 1:
                    return new LibraryItemName(temp2[0],temp[1],temp[2]);
                case 2:
                    return new LibraryItemName(temp2[0], temp2[1], temp[1], temp[2]);
                case 3:
                    return new LibraryItemName(temp2[0], temp2[1],temp2[2], temp[1], temp[2]);
                default: throw new Exception("It's not a library string");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="natives">Target platform natives JObject</param>
        /// <returns></returns>
        public static LibraryItemName Parse(string item, JToken natives)
        {
            var result = Parse(item);
            result.IsNatives = true;
            result.Natives = GetWindowsNatives(natives["windows"].ToString());
            return result;
        }

        /// <summary>
        /// 将当前 LibraryItem 对象转换为相对路径
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Path;
        }

        /// <summary>
        /// 获取当前 LibraryItem 对象的相对路径属性
        /// </summary>
        /// <returns></returns>
        public string Path
        {
            get
            {
                var fileName = Name + SubNameLink + Version + (IsNatives ? (SubNameLink + Natives) : string.Empty) +
                               ExtensionName;
                return System.IO.Path.Combine(Type, Company, Product, System.IO.Path.Combine(Name, Version, fileName));
            }
        }

        private static string GetWindowsNatives(string str)
        {
            return !str.Contains(@"${arch}") ? str.Replace(@"${arch}", SystemInfo.Is64() ? @"64" : @"32") : str;
        }
    }
}