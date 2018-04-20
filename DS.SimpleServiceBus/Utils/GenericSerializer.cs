using System.IO;
using System.IO.Compression;
using Polenter.Serialization;

namespace DS.SimpleServiceBus.Utils
{
    /// <summary>
    ///     Using SharpSerializer to Serialize/Deserialize without the need for Serializable attribute.
    ///     Also uses compression to minimize the data used in the transfer.
    /// </summary>
    public class GenericSerializer
    {
        /// <summary>
        ///     Serializes T and compresses the data
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="obj">Instance of T</param>
        /// <returns>Compressed byte[]</returns>
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

        /// <summary>
        ///     Decompresses byte[] and deserializes T
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="arrBytes">Instance of T as byte[]</param>
        /// <returns>Instance of T</returns>
        public static T DeSerialize<T>(byte[] arrBytes)
        {
            var serializer = new SharpSerializer();

            using (var memoryStream = new MemoryStream(Decompress(arrBytes)))
            {
                var obj = serializer.Deserialize(memoryStream);

                return (T) obj;
            }
        }

        /// <summary>
        ///     Compresses byte[] using GZip
        /// </summary>
        /// <param name="input">byte[]</param>
        /// <returns>byte[]</returns>
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

        /// <summary>
        ///     Decompresses byte[] using GZip
        /// </summary>
        /// <param name="input">byte[]</param>
        /// <returns>byte[]</returns>
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