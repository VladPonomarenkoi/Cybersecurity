using System;
using System.Security.Cryptography;
using System.Text;

namespace lab3x3
{
    class Program
    {
        static byte[] ComputeHashSHA256(byte[] DataForHash)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(DataForHash);
            }
        }
        public static byte[] ComputeHmacsha256(byte[] ToBeHashed, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(ToBeHashed);
            }
        }
        static void Main(string[] args)
        {
            string key = "Vladpon";
            string message = "love is a feeling";
            var Key = ComputeHashSHA256(Encoding.Unicode.GetBytes(key));
            var Mess = ComputeHmacsha256(Encoding.Unicode.GetBytes(message), Key);
            Console.WriteLine($"Сообщение: {message}");
            Console.WriteLine($"Хешированное сообщение: {Convert.ToBase64String(Mess)}");
            Console.WriteLine("Отправка сообщения получателю....");
            string key2 = "Vladpon";
            var Key2 = ComputeHashSHA256(Encoding.Unicode.GetBytes(key2));
            var Mess2 = ComputeHmacsha256(Encoding.Unicode.GetBytes(message), Key2);
            if (Convert.ToBase64String(Mess) == Convert.ToBase64String(Mess2))
            {
                Console.WriteLine("Сообщение верное!");
            }
            else
            {
                Console.WriteLine("Сообщение не верное!");
            }
        }
    }
}
