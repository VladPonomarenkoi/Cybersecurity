using System;
using System.Security.Cryptography;
using System.Text;

namespace Exam
{
    public class PBKDF2
    {
        public static byte[] GenerateSalt()
        {
            const int saltLength = 32;
            using (var randomNumberGenerator =
            new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[saltLength];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(
            toBeHashed, salt, 19000))
            {
                return rfc2898.GetBytes(64);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter password");
            string password = Console.ReadLine();
            var hashedPassword = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(password), PBKDF2.GenerateSalt());
            Console.WriteLine("Password: " + password);
            Console.WriteLine("Hashed Password: " + Convert.ToBase64String(hashedPassword));

        }
    }
}
