using System;
using System.IO;

namespace Magentaize.Net.LMCL.Model
{
    public class LibraryItem
    {
        public string Type { get; set; }

        public string Company { get; set; }

        public string Product { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        /// <summary>
        /// Like "net.java.jinput:jinput:2.0.5"
        /// </summary>
        /// <param name="type"></param>
        /// <param name="company"></param>
        /// <param name="product"></param>
        /// <param name="name"></param>
        /// <param name="version"></param>
        public LibraryItem(string type, string company, string product, string name, string version)
        {
            Type = type;
            Company = company;
            Product = product;
            Name = name;
            Version = version;
        }

        /// <summary>
        /// Like "io.netty:netty-all:4.0.10.Final"
        /// </summary>
        /// <param name="type"></param>
        /// <param name="product"></param>
        /// <param name="name"></param>
        /// <param name="version"></param>
        public LibraryItem(string type, string product, string name, string version)
            : this(type, string.Empty, product, name, version)
        {
        }

        /// <summary>
        /// Like "java3d:vecmath:1.3.1"
        /// </summary>
        /// <param name="product">The first part</param>
        /// <param name="name">The middle part</param>
        /// <param name="version">The lastest part</param>
        public LibraryItem(string product, string name, string version) : this(string.Empty, product, name, version)
        {
        }

        public static LibraryItem Parse(string item)
        {
            var temp = item.Split(':');
            if (temp.Length != 3)
            {
                throw new Exception("It's not a library string");
            }
            var temp2 = temp[0].Split('.');

            LibraryItem result;
            switch (temp2.Length)
            {
                case 1:
                    result = new LibraryItem(temp2[0],temp[1],temp[2]);
                    break;
                case 2:
                    result = new LibraryItem(temp2[0], temp2[1], temp[1], temp[2]);
                    break;
                case 3:
                    result = new LibraryItem(temp2[0], temp2[1],temp2[2], temp[1], temp[2]);
                    break;
                default: throw new Exception("It's not a library string");
            }

            return result;
        }

        /// <summary>
        /// 将当前LibraryItem对象转换为相对路径
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Path.Combine(Type, Company, Product, Path.Combine(Name, Version, Name + Version + @".jar"));
        }
    }
}