using System;
using System.Diagnostics;
using System.Reflection;

namespace Magentaize.Net.LMCL.Utilities
{
    /// <summary>
    /// Get some types of version.
    /// </summary>
    public static class GetVersion
    {
        /// <summary>
        /// Get the ProductVersion of the main exe.
        /// </summary>
        /// <returns></returns>
        public static string ProductVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion;
        }

        /// <summary>
        /// Get the AssemblyVersion of the main exe.
        /// </summary>
        /// <returns></returns>
        public static string AssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        /// <summary>
        /// Get the version of current .Net Framework.
        /// </summary>
        /// <returns></returns>
        public static string DotNetVerson()
        {
            return Environment.Version.ToString();
        }
    }
}