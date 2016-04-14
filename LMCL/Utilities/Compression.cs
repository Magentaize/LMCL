using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Magentaize.Net.LMCL.Utilities
{
    /// <summary>
    /// Extract a zip file to a directory or creat a zip file from a directory.
    /// </summary>
    public static class Compression
    {
        /// <summary>
        /// Creat a zip file from a directory with level 1.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        public static void Zip(string path, string file)
        {
            Zip(path, file, 1);
        }

        /// <summary>
        /// Creat a zip file from a directory with custom level.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        /// <param name="level"></param>
        public static void Zip(string path, string file, int level)
        {
            if (File.Exists(file))
                File.Delete(file);
            Task.Factory.StartNew(() =>
            {
                ZipFile.CreateFromDirectory(path, file, (CompressionLevel)level, true);
            });
        }

        /// <summary>
        /// Extract a zip file to a directory (not overwrite the same file).
        /// </summary>
        /// <param name="file">指定的压缩文件绝对路径</param>
        /// <param name="path">欲解压至的目录</param>
        public static void UnZip(string file, string path)
        {
            Task.Factory.StartNew(() =>
            {
                //path = path.PathFormat();
                if (!File.Exists(file))
                    throw new FileNotFoundException();
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string entryPath, tempPath = path;
                using (ZipArchive archive = ZipFile.OpenRead(file))
                {
                    foreach (ZipArchiveEntry zipEntry in archive.Entries)
                    {
                        entryPath = Path.Combine(tempPath, zipEntry.Name);
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(entryPath);
                        if (zipEntry.Name == string.Empty)
                        {
                            tempPath = Path.Combine(path, zipEntry.FullName);
                            Directory.CreateDirectory(tempPath);
                            continue;
                        }
                        else
                        {
                            if (!File.Exists(entryPath))
                                zipEntry.ExtractToFile(entryPath);
                            else
                            if (zipEntry.Length != fileInfo.Length)
                                zipEntry.ExtractToFile(entryPath, true);
                        }
                    }
                }
            });
        }
    }
}