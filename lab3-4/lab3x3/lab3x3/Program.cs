using System;
using System.Text;
using System.Security.Cryptography;

namespace _3part
{
    class Program
    {
        public static byte[] ComputeHmacsha1(byte[] toBeHashed, byte[] key)
        {
            using (var hmac = new HMACSHA1(key))
            {
                return hmac.ComputeHash(toBeHashed);
            }
        }
        public static void CheckIfConfident(string GettedMessage, string key, string GettedHash)
        {
            Console.WriteLine("Обчислення та порiвняння хешування");
            var tempComputedHash = ComputeHmacsha1(Encoding.Unicode.GetBytes(GettedMessage), Encoding.Unicode.GetBytes(key));
            if (GettedHash == Convert.ToBase64String(tempComputedHash))
            {
                Console.WriteLine("Обчислений хеш той самий, що й отриманий");
                Console.WriteLine("Авторизовано");
            }
            else
            {
                Console.WriteLine("Обчислений хеш НЕ той самий, що отриманий");
                Console.WriteLine("НЕ Авторизовано");
            }
        }

        static void Main(string[] args)
        {
            const string strForHash = "Hello World!";
            const string IncorectStrForHash = "Buy World!";
            const string keyForHash = "secret";
            const string HashWeGet = "+wS72Kx9qyWwG1LhKL4rDbgbNqc=";

            var sha1ForStr = ComputeHmacsha1(Encoding.Unicode.GetBytes(strForHash), Encoding.Unicode.GetBytes(keyForHash));
            Console.WriteLine($"Hash sha1:{Convert.ToBase64String(sha1ForStr)}");
            Console.WriteLine("-------------------");
            CheckIfConfident(strForHash, keyForHash, HashWeGet);
            Console.WriteLine("-------------------");
            CheckIfConfident(IncorectStrForHash, keyForHash, HashWeGet);


        }
    }
}
