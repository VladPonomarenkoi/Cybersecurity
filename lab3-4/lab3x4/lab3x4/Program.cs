using System;
using System.Security.Cryptography;
using System.Text;

namespace lab3x4
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
        static void Main(string[] args)
        {
            Console.WriteLine("Для регистрации введите логин и пароль.");
            Console.WriteLine("Введите логин: ");

            string Login = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите пароль: ");

            string Password = Convert.ToBase64String(ComputeHashSHA256(Encoding.Unicode.GetBytes(Convert.ToString(Console.ReadLine()))));
            Console.WriteLine("Регистрация завершена!");
            Console.WriteLine("Для входа введите свои учетные данные:");
            Console.WriteLine("Введите логин: ");

            string EnteredLogin = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите пароль: ");

            string EnteredPassword = Convert.ToBase64String(ComputeHashSHA256(Encoding.Unicode.GetBytes(Convert.ToString(Console.ReadLine()))));
            if (Login != EnteredLogin)
            {
                Console.WriteLine("Введен неверный логин!");
            }
            else if (Password != EnteredPassword)
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

