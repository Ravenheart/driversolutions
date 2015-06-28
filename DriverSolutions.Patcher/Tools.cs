using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.Patcher
{
    public class Tools
    {
        public const int BufferSize = 20480;

        public static byte[] Compress(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            using (GZipStream gzip = new GZipStream(
                output,
                CompressionMode.Compress,
                CompressionLevel.BestCompression,
                true))
            {
                gzip.Write(data, 0, data.Length);
                gzip.Flush();
            }
            return output.ToArray();
        }
        public static void Compress(byte[] data, string path)
        {
            using (GZipStream gzip = new GZipStream(
                new FileStream(path, FileMode.Create, FileAccess.Write),
                CompressionMode.Compress, CompressionLevel.BestCompression,
                false))
            {
                gzip.Write(data, 0, data.Length);
                gzip.Flush();
            }
        }
        public static byte[] Decompress(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            byte[] buffer = new byte[BufferSize];
            int read;

            using (GZipStream gzip = new GZipStream(new MemoryStream(data), CompressionMode.Decompress, CompressionLevel.BestCompression, false))
            {

                while ((read = gzip.Read(buffer, 0, BufferSize)) > 0)
                {
                    output.Write(buffer, 0, read);
                }
            }

            return output.ToArray();
        }
        public static void Decompress(byte[] data, string path)
        {
            byte[] buffer = new byte[BufferSize];
            int read;

            using (FileStream output = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (GZipStream gzip = new GZipStream(new MemoryStream(data), CompressionMode.Decompress, CompressionLevel.BestCompression, false))
            {
                while ((read = gzip.Read(buffer, 0, BufferSize)) > 0)
                {
                    output.Write(buffer, 0, read);
                    output.Flush();
                }
            }
        }

        public static string Hash(byte[] data)
        {
            using (var md5 = MD5Cng.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(data)).Replace("-", string.Empty);
            }
        }
    }
}
