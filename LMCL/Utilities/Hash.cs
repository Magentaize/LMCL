using System;
using System.IO;
using System.Security.Cryptography;

namespace Magentaize.Net.LMCL.Utilities
{
    /// <summary>
    /// Get the hash of a file.
    /// </summary>
    public static class Hash
    {
        public enum HashMode
        {
            MD5,
            SHA1,
        }

        /// <summary>
        /// Get the MD5 of a file.
        /// </summary>
        /// <param name="filePath">The absolute path of the target file.</param>
        /// <returns>The MD5 of the target file.</returns>
        public static string MD5File(string filePath)
        {
            return HashFile(filePath, HashMode.MD5);
        }

        /// <summary>
        /// Get the SHA1 of a file.
        /// </summary>
        /// <param name="filePath">The absolute path of the target file.</param>
        /// <returns>The SHA1 of the target file.</returns>
        public static string SHA1File(string filePath)
        {
            return HashFile(filePath, HashMode.SHA1);

        }

        /// <summary>
        /// Read the file as a stream 
        /// </summary>
        /// <param name="filePath">The absolute path of the target file.</param>
        /// <param name="hashMode">The mode of hash.</param>
        /// <returns>The upper hash value with no "-" of the target file.</returns>
        private static string HashFile(string filePath, HashMode hashMode)
        {
            FileStream fs;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            }
            catch (FileNotFoundException)
            {
                return string.Empty;
            }

            HashAlgorithm hasher = null;
            if (fs == null)
                throw new ArgumentNullException();
            switch (hashMode)
            {
                case HashMode.MD5: hasher = MD5.Create(); break;
                case HashMode.SHA1: hasher = SHA1.Create(); break;
            }
            byte[] hashBytes = hasher.ComputeHash(fs);
            fs.Close();

            return BitConverter.ToString(hashBytes).Replace("-", "");
        }

        //private static byte[] HashData(Stream stream, HashMode hashMode)
        //{
        //    HashAlgorithm hasher = null;
        //    if (stream == null)
        //        throw new ArgumentNullException();
        //    switch (hashMode)
        //    {
        //        case HashMode.MD5: hasher = MD5.Create(); break;
        //        case HashMode.SHA1: hasher = SHA1.Create(); break;
        //    }
        //    return hasher.ComputeHash(stream);
        //}
    }
}