using System;
using Magentaize.Net.LMCL.JsonLite.Linq;

namespace Magentaize.Net.LMCL.Model
{
    public class LibraryOriginalItem : LibraryItem, ILibraryItem
    {
        public LibraryItemName Name { get; private set; }

        public string Path => Name.Path;

        public string Url { get; private set; }

        public bool MustExtract { get; private set; }

        private string _extractFolder;

        public string ExtractFolder
        {
            private set { _extractFolder = value; }
            get
            {
                if (MustExtract)
                {
                    return _extractFolder;
                }
                throw new MemberAccessException(@"This LibraryItem cannot be extracted.");
            }
        }

        /// <summary>
        /// 将包含MC本体的 Library 的 JToken 反序列化为目标类型
        /// </summary>
        /// <param name="item"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static LibraryOriginalItem Parse(JToken item, MinecraftVersion version)
        {
            var obj = new LibraryOriginalItem();

            switch (version)
            {
                case MinecraftVersion.OneDotSeven:
                    PraseOneDotSeven(ref obj, ref item);
                    break;
                case MinecraftVersion.OneDotEight:
                    PraseOneDotEight(ref obj, ref item);
                    break;
                case MinecraftVersion.OneDotNine:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(version), version, null);
            }

            return obj;
        }

        private static void PraseOneDotSeven(ref LibraryOriginalItem obj, ref JToken item)
        {
            JToken jNatives;
            if ((jNatives = item["natives"]) == null)
            {
                obj.Name = LibraryItemName.Parse(item["name"].ToString());
                return;
            }

            obj.Name = LibraryItemName.Parse(item["name"].ToString(), jNatives);
            obj.MustExtract = true;
            obj.ExtractFolder = item["extract"]["exclude"][0].ToString();
        }

        private static void PraseOneDotEight(ref LibraryOriginalItem obj, ref JToken item)
        {
            JToken jNatives;
            if ((jNatives = item["natives"]) == null)
            {
                obj.Name = LibraryItemName.Parse(item["name"].ToString());
                obj.Url = item["downloads"]["artifact"]["path"].ToString();
                return;
            }

            obj.Name = LibraryItemName.Parse(item["name"].ToString(), jNatives);
            obj.Url = item["downloads"]["classifiers"]["natives-windows"]["path"].ToString();
            obj.MustExtract = true;
            obj.ExtractFolder = item["extract"]["exclude"][0].ToString();
        }
    }
}