using System;
using System.Security.Cryptography;
using System.Text;

namespace lab3
{
    class Program
    {
        static byte[] ComputeHashMd5(byte[] dataForHash)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(dataForHash);
            }
        }
        public static byte[] ComputeHashSha1(byte[] toBeHashed)
        {
            using (var sha1 = SHA1.Create())
            {
                return sha1.ComputeHash(toBeHashed);
            }
        }
        public static byte[] ComputeHashSha256(byte[] toBeHashed)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }
        public static byte[] ComputeHashSha384(byte[] toBeHashed)
        {
            using (var sha384 = SHA384.Create())
            {
                return sha384.ComputeHash(toBeHashed);
            }
        }
        public static byte[] ComputeHashSha512(byte[] toBeHashed)
        {
            using (var sha512 = SHA512.Create())
            {
                return sha512.ComputeHash(toBeHashed);
            }
        }
        static void Main(string[] args)
        {
            const string strForHash1 = "Hello World!";
            const string strForHash2 = "Hello world!";
            const string strForHash3 = "Hello world!";

            var md5ForStr1 = ComputeHashMd5(Encoding.Unicode.GetBytes(strForHash1));
            var md5ForStr2 = ComputeHashMd5(Encoding.Unicode.GetBytes(strForHash2));
            var md5ForStr3 = ComputeHashMd5(Encoding.Unicode.GetBytes(strForHash3));
            Guid guidForMD_1 = new Guid(md5ForStr1),
            guidForMD_2 = new Guid(md5ForStr2),
            guidForMD_3 = new Guid(md5ForStr3);
            Console.WriteLine("MD5:");
            Console.WriteLine(strForHash1);
            Console.WriteLine(strForHash2);
            Console.WriteLine(strForHash3);
            Console.WriteLine(Convert.ToBase64String(md5ForStr1));
            Console.WriteLine(Convert.ToBase64String(md5ForStr2));
            Console.WriteLine(Convert.ToBase64String(md5ForStr3));

            var sha1ForStr1 = ComputeHashSha1(Encoding.Unicode.GetBytes(strForHash1));
            var sha1ForStr2 = ComputeHashSha1(Encoding.Unicode.GetBytes(strForHash2));
            var sha1ForStr3 = ComputeHashSha1(Encoding.Unicode.GetBytes(strForHash3));

            Console.WriteLine("SHA1:");
            Console.WriteLine(strForHash1);
            Console.WriteLine(strForHash2);
            Console.WriteLine(strForHash3);
            Console.WriteLine(Convert.ToBase64String(sha1ForStr1));
            Console.WriteLine(Convert.ToBase64String(sha1ForStr2));
            Console.WriteLine(Convert.ToBase64String(sha1ForStr3));

            var sha256ForStr1 = ComputeHashSha256(Encoding.Unicode.GetBytes(strForHash1));
            var sha256ForStr2 = ComputeHashSha256(Encoding.Unicode.GetBytes(strForHash2));
            var sha256ForStr3 = ComputeHashSha256(Encoding.Unicode.GetBytes(strForHash3));
            Console.WriteLine("SHA256:");
            Console.WriteLine(strForHash1);
            Console.WriteLine(strForHash2);
            Console.WriteLine(strForHash3);
            Console.WriteLine(Convert.ToBase64String(sha256ForStr1));
            Console.WriteLine(Convert.ToBase64String(sha256ForStr2));
            Console.WriteLine(Convert.ToBase64String(sha256ForStr3));

            var sha384ForStr1 = ComputeHashSha384(Encoding.Unicode.GetBytes(strForHash1));
            var sha384ForStr2 = ComputeHashSha384(Encoding.Unicode.GetBytes(strForHash2));
            var sha384ForStr3 = ComputeHashSha384(Encoding.Unicode.GetBytes(strForHash3));
            Console.WriteLine("SHA384:");
            Console.WriteLine(strForHash1);
            Console.WriteLine(strForHash2);
            Console.WriteLine(strForHash3);
            Console.WriteLine(Convert.ToBase64String(sha384ForStr1));
            Console.WriteLine(Convert.ToBase64String(sha384ForStr2));
            Console.WriteLine(Convert.ToBase64String(sha384ForStr3));

            var sha512ForStr1 = ComputeHashSha512(Encoding.Unicode.GetBytes(strForHash1));
            var sha512ForStr2 = ComputeHashSha512(Encoding.Unicode.GetBytes(strForHash2));
            var sha512ForStr3 = ComputeHashSha512(Encoding.Unicode.GetBytes(strForHash3));
            Console.WriteLine("SHA512:");
            Console.WriteLine(strForHash1);
            Console.WriteLine(strForHash2);
            Console.WriteLine(strForHash3);
            Console.WriteLine(Convert.ToBase64String(sha512ForStr1));
            Console.WriteLine(Convert.ToBase64String(sha512ForStr2));
            Console.WriteLine(Convert.ToBase64String(sha512ForStr3));
        }
    }
}