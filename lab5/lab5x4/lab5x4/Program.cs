using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace lab5x4
{
    public class PBKDF2
    {
        public static byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }

        public static byte[] HashPasswordMD5(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.MD5))
            {
                return rfc2898.GetBytes(16);
            }
        }

        public static byte[] HashPasswordSHA1(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA1))
            {
                return rfc2898.GetBytes(20);
            }
        }

        public static byte[] HashPasswordSHA256(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA256))
            {
                return rfc2898.GetBytes(32);
            }
        }

        public static byte[] HashPasswordSHA384(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA384))
            {
                return rfc2898.GetBytes(48);
            }
        }

        public static byte[] HashPasswordSHA512(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA512))
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
            string ToHash = Console.ReadLine();

            HashPassword(ToHash, 220000);
            HashPassword(ToHash, 720000);
            HashPassword(ToHash, 1220000);
            HashPassword(ToHash, 1720000);
            HashPassword(ToHash, 2220000);
            HashPassword(ToHash, 2720000);
            HashPassword(ToHash, 3220000);
            HashPassword(ToHash, 3720000);
            HashPassword(ToHash, 4220000);
            HashPassword(ToHash, 4720000);
            Console.ReadLine();
        }

        private static void HashPassword(string password, int number)
        {
            var hash = new Stopwatch();
            hash.Start();

            var hashedPassword = PBKDF2.HashPasswordSHA384(Encoding.UTF8.GetBytes(password), PBKDF2.GenerateSalt(), number);

            hash.Stop();

            Console.WriteLine();
            Console.WriteLine("Password: " + password);
            Console.WriteLine("Hashed Password: " + Convert.ToBase64String(hashedPassword));
            Console.WriteLine("Iterations <" + number + ">");
            Console.WriteLine("Time: " + hash.ElapsedMilliseconds + "ms");
        }
    }
}
