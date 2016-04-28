using System;

namespace Magentaize.Net.LMCL.Utilities
{
    public class SystemInfo
    {
        public static bool Is64 ()
        {
            return Environment.Is64BitOperatingSystem;
        }

        public static string GetOperatingSystem()
        {
            return @"windows";
        }
    }
}