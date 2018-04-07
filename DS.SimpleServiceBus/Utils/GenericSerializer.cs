using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using Polenter.Serialization;

namespace DS.SimpleServiceBus.Utils
{
    public class GenericSerializer
    {
        public static byte[] Serialize<T>(T obj)
        {
            var serializer = new SharpSerializer();

            if (obj == null)
                return null;

            using (var memoryStream = new MemoryStream())
            {
                serializer.Serialize(obj, memoryStream);

                return Compress(memoryStream.ToArray());
            }
        }

        public static T DeSerialize<T>(byte[] arrBytes)
        {
            var serializer = new SharpSerializer();

            using (var memoryStream = new MemoryStream(Decompress(arrBytes)))
            {
                var obj = serializer.Deserialize(memoryStream);

                return (T)obj;
            }
        }

        //public static byte[] Serialize<T>(T obj)
        //{
        //    if (obj == null)
        //        return null;

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        var binaryFormatter = new BinaryFormatter();
        //        binaryFormatter.Serialize(memoryStream, obj);
        //        var compressed = Compress(memoryStream.ToArray());

        //        return compressed;
        //    }
        //}

        //public static T DeSerialize<T>(byte[] arrBytes)
        //{
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        var binaryFormatter = new BinaryFormatter();
        //        var decompressed = Decompress(arrBytes);

        //        memoryStream.Write(decompressed, 0, decompressed.Length);
        //        memoryStream.Seek(0, SeekOrigin.Begin);

        //        return (T) binaryFormatter.Deserialize(memoryStream);
        //    }
        //}

        private static byte[] Compress(byte[] input)
        {
            byte[] compressesData;

            using (var outputStream = new MemoryStream())
            {
                using (var zip = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    zip.Write(input, 0, input.Length);
                }

                compressesData = outputStream.ToArray();
            }

            return compressesData;
        }

        private static byte[] Decompress(byte[] input)
        {
            byte[] decompressedData;

            using (var outputStream = new MemoryStream())
            {
                using (var inputStream = new MemoryStream(input))
                {
                    using (var zip = new GZipStream(inputStream, CompressionMode.Decompress))
                    {
                        zip.CopyTo(outputStream);
                    }
                }

                decompressedData = outputStream.ToArray();
            }

            return decompressedData;
        }
    }
}