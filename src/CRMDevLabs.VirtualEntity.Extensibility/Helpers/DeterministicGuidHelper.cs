using System;
using System.Security.Cryptography;
using System.Text;

namespace CRMDevLabs.VirtualEntity.Extensibility.Helpers
{
    public class DeterministicGuidHelper
    {
        static readonly Guid DnsNamespace = new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8");
        static readonly Guid UrlNamespace = new Guid("6ba7b811-9dad-11d1-80b4-00c04fd430c8");
        static readonly Guid IsoOidNamespace = new Guid("6ba7b812-9dad-11d1-80b4-00c04fd430c8");

        /// <summary>
        /// <seealso cref="https://github.com/LogosBible/Logos.Utility/blob/master/src/Logos.Utility/GuidUtility.cs"/>
        /// <seealso cref="http://www.ietf.org/rfc/rfc4122.txt"/>
        /// </summary>
        /// <param name="externalPkField">The primary key, simple or pre-computed, of the external system</param>
        /// <returns></returns>
        public static Guid Generate(string externalPkField)
        {
            if (string.IsNullOrEmpty(externalPkField)) throw new ArgumentNullException("externalPkField");

            var version = 3;
            externalPkField = $"http://{externalPkField}.com/";
            byte[] nameBytes = Encoding.UTF8.GetBytes(externalPkField);

            byte[] namespaceBytes = UrlNamespace.ToByteArray();
            SwapByteOrder(namespaceBytes);

            byte[] hash;
            using (HashAlgorithm algorithm = version == 3 ? (HashAlgorithm)MD5.Create() : SHA1.Create())
            {
                algorithm.TransformBlock(namespaceBytes, 0, namespaceBytes.Length, null, 0);
                algorithm.TransformFinalBlock(nameBytes, 0, nameBytes.Length);
                hash = algorithm.Hash;
            }

            byte[] newGuid = new byte[16];
            Array.Copy(hash, 0, newGuid, 0, 16);

            newGuid[6] = (byte)((newGuid[6] & 0x0F) | (version << 4));
            newGuid[8] = (byte)((newGuid[8] & 0x3F) | 0x80);
            SwapByteOrder(newGuid);

            return new Guid(newGuid);
        }

        private static void SwapByteOrder(byte[] guid)
        {
            SwapBytes(guid, 0, 3);
            SwapBytes(guid, 1, 2);
            SwapBytes(guid, 4, 5);
            SwapBytes(guid, 6, 7);
        }

        private static void SwapBytes(byte[] guid, int left, int right)
        {
            byte temp = guid[left];
            guid[left] = guid[right];
            guid[right] = temp;
        }
    }
}
