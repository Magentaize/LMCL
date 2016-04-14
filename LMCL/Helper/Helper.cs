using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Magentaize.Net.LMCL.Helper
{
   public static class StringHelper
    {
        public static string PathFormat(this string str)
        {
            if (str[str.Length - 1] != '\\')
                str += '\\';
            return str;
        }

       public static void CopyExpectionToClipboard(string e)
       {
           Clipboard.SetText(e, TextDataFormat.UnicodeText);
       }
      
    }
}
