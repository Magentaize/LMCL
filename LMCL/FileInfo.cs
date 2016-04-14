using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Magentaize.Net.LMCL.Helper;

namespace Magentaize.Net.LMCL
{
    public abstract class FileInfo
    {
        public string Path { get; protected set; }

        public string Name { get; protected set; }

        public string Url { get; protected set; }

        public string FullPath()
        {
            return System.IO.Path.Combine(Path, Name);
        }
    }
}
