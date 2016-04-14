using System;
using System.Collections.Generic;
using System.Linq;
using Magentaize.Net.LMCL.Helper;

namespace Magentaize.Net.LMCL
{

    public static class Cls
    {
        public static string baseDirectory = AppDomain.CurrentDomain.SetupInformation.ApplicationBase.PathFormat();
        public static class URL
        {
            public static string updateJson = "http://mcupdate.ime.moe/update.json";
            public static string CheckLauncherUpdate = "http://mcupdate.ime.moe/launcherver.html";
            public static string launcherUpdate = "http://mcupdate.ime.moe/launcherupdate.exe";
            public static string fileServer = "http://mcupdate.ime.moe/";
        }

        public static class File
        {
            public static string fileDebug = baseDirectory + "debug.bat";
            public static string fileLauncherUpdate = baseDirectory + "launcherupdate.exe";
        }

        public static class Api
        {
            public static string apiBangBang93 = "http://bmclapi.bangbang93.com/libraries/";
            public static string apiOffical = "https://libraries.minecraft.net/";
        }

        public static string hostServer = "mc.ime.moe";
       



    }
}
