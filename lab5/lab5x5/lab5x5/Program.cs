using System;
using System.Security.Cryptography;
using System.Text;

namespace lab5x5
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

        public static byte[] HashSHA256(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var sha256 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA256))
            {
                return sha256.GetBytes(64);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Для регистрации введите логин и пароль:");
            Console.WriteLine("Введите логин: ");
            string login = Convert.ToString(Console.ReadLine());
            byte[] salt = PBKDF2.GenerateSalt();
            Console.WriteLine("Введите пароль: ");
            string password = Convert.ToBase64String(PBKDF2.HashSHA256(Encoding.Unicode.GetBytes(Convert.ToString(Console.ReadLine())), salt, 220000));

            Console.WriteLine("Регистрация завершена!");

            Console.WriteLine("Для входа введите свои учетные данные:");
            Console.WriteLine("Введите логин: ");
            string eLogin = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите пароль: ");
            string ePassword = Convert.ToBase64String(PBKDF2.HashSHA256(Encoding.Unicode.GetBytes(Convert.ToString(Console.ReadLine())), salt, 220000));

            if (login != eLogin)
            {
                Console.WriteLine("Введен неверный логин!");
            }
            else if (password != ePassword)
            {
                Console.WriteLine("Введен неверный пароль!");
            }
            else
            {
                Console.WriteLine("Авторизация завершена!");
            }

        }
    }
}
