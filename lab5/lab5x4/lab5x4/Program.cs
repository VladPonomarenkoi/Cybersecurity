﻿using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace lab5x4
{
    public class MBK4
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
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds, System.Security.Cryptography.HashAlgorithmName hashAlgorithm)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, hashAlgorithm))
            {
                return rfc2898.GetBytes(20);
            }
        }


    }
    class Program
    {

        static void Main(string[] args)
        {
            const string passwordToHash = "Free Google service";
            HashPassword(passwordToHash, 10000);
            HashPassword(passwordToHash, 60000);
            HashPassword(passwordToHash, 110000);
            HashPassword(passwordToHash, 160000);
            HashPassword(passwordToHash, 210000);
            HashPassword(passwordToHash, 260000);
            HashPassword(passwordToHash, 310000);
            HashPassword(passwordToHash, 360000);
            HashPassword(passwordToHash, 410000);
            HashPassword(passwordToHash, 460000);
            Console.ReadLine();
        }
        private static void HashPassword(string passwordToHash, int numberOfRounds)
        {
            var sw = new Stopwatch();
            sw.Start();
            var hashedPassword = MBK4.HashPassword(Encoding.UTF8.GetBytes(passwordToHash), MBK4.GenerateSalt(), numberOfRounds, HashAlgorithmName.SHA512);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("Пароль для хешування: " + passwordToHash);
            Console.WriteLine("Хешований пароль: " + Convert.ToBase64String(hashedPassword));
            Console.WriteLine("Iтерацi <" + numberOfRounds + "> Витрачений час: " + sw.ElapsedMilliseconds + "ms");
        }

    }
}