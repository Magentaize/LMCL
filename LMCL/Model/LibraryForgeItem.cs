using System;
using Magentaize.Net.LMCL.JsonLite.Linq;

namespace Magentaize.Net.LMCL.Model
{
    public class LibraryForgeItem : LibraryItem, ILibraryItem
    {
        public LibraryItemName Name { get; private set; }

        public string SubUrl { get; private set; }

        public string Path => Name.Path;

        public string Url => SubUrl + Path;

        /// <summary>
        /// 将包含Forge的 Library 的 JToken 反序列化为目标类型
        /// </summary>
        /// <param name="item"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static LibraryForgeItem Parse(JToken item, MinecraftVersion version)
        {
            var obj = new LibraryForgeItem();
            switch (version)
            {
                 case MinecraftVersion.OneDotSeven:PraseOneDotSeven(ref obj,ref item);break;
                case MinecraftVersion.OneDotEight:
                    break;
                case MinecraftVersion.OneDotNine:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(version), version, null);
            }
            return obj;
        }

        private static void PraseOneDotSeven(ref LibraryForgeItem obj, ref JToken item)
        {
            obj = new LibraryForgeItem
            {
                Name = LibraryItemName.Parse(item["name"].ToString()), SubUrl = item["url"].ToString()
            };
        }
    }
}