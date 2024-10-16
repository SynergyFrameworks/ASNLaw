using System;
using System.IO;
using System.IO.Compression;

namespace Infrastructure.Common.Extensions
{
    public static class StreamExtensions
    {
        public static void CopyTo(this Stream fromStream, Stream toStream)
        {
            if (fromStream == null)
                throw new ArgumentNullException("fromStream");
            if (toStream == null)
                throw new ArgumentNullException("toStream");

            var bytes = new byte[8092];
            int dataRead;
            while ((dataRead = fromStream.Read(bytes, 0, bytes.Length)) > 0)
                toStream.Write(bytes, 0, dataRead);
        }

        public static byte[] ReadFully(this Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static string ReadToString(this Stream stream)
        {
            // convert stream to string
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static Stream Compress(this Stream input)
        {
            var output = new MemoryStream();
            input.Position = 0;
            using (var compressor = new GZipStream(output, CompressionMode.Compress))
            {
                input.CopyTo(compressor);
                compressor.Close();
                return new MemoryStream(output.ToArray());
            }
        }

        public static Stream Decompress(this Stream input)
        {
            var output = new MemoryStream();
            input.Position = 0;
            using (var decompressor = new GZipStream(input, CompressionMode.Decompress))
            {
                decompressor.CopyTo(output);
                decompressor.Close();
                output.Position = 0;
                return output;
            }
        }

        public static byte[] GetArray(this Stream input)
        {
            using (var temp = new MemoryStream())
            {
                input.CopyTo(temp);
                return temp.ToArray();
            }
        }
    }
}
