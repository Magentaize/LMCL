using Newtonsoft.Json.Linq;

namespace Magentaize.Net.LMCL.Model
{
    public class LibraryItem
    {
        public LibraryItemName Name { get; set; }

        public string Sha1 { get; set; }

        public string Path { get; set; }

        public string Url { get; set; }

        public bool Extract { get; set; }

        public string ExtractFolder { get; set; }



        public LibraryItem()
        {
            
        }

        public LibraryItem(JObject item)
        {
            Name = LibraryItemName.Parse(item["name"].ToString());
            item = (JObject)item["downloads"]["artifact"];
            Sha1 = item["sha1"].ToString();
            Path = item["path"].ToString();
            Url = item["url"].ToString();
        }
    }
}