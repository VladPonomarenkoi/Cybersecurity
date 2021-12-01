using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab7x2
{
    class RSAWithRSAParameterKey
    {
        private RSAParameters _privateKey;

        public void AssignNewKeys(string publicKeyPath)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
                _privateKey = rsa.ExportParameters(true);
            }
        }

        public byte[] EncryptData(string publicKeyPath, byte[] dataToEncrypt)
        {
            byte[] cypherbytes;

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));

                cypherbytes = rsa.Encrypt(dataToEncrypt, true);
            }

            return cypherbytes;
        }

        public byte[] DecryptData(byte[] dataToDecrypt)
        {
            byte[] plainText;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_privateKey);
                plainText = rsa.Decrypt(dataToDecrypt, true);
            }

            return plainText;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var rsaParams = new RSAWithRSAParameterKey();

            Console.WriteLine("Введiть текст для шифрування: ");
            string original = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Введiть потрiбний шлях до вiдкритого ключа, який буде згенеровано: ");
            string publicKeyPath = Convert.ToString(Console.ReadLine());
            rsaParams.AssignNewKeys(publicKeyPath);

            Console.WriteLine("Оригiнальний текст: " + original);

            var encrypted = rsaParams.EncryptData(publicKeyPath, Encoding.Unicode.GetBytes(original));
            Console.WriteLine("Зашифрований текст: " + Convert.ToBase64String(encrypted));

            var decrypted = rsaParams.DecryptData(encrypted);
            Console.WriteLine("Розшифрований текст: " + Encoding.Default.GetString(decrypted));

            Console.WriteLine("Хочете зашифрувати текст чиiмось вiдкритим ключем? т/н: ");
            string encryptAnother = Convert.ToString(Console.ReadLine());

            if (encryptAnother == "так")
            {
                Console.WriteLine("Введiть текст для шифрування: ");
                string additionalOriginal = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Введiть шлях до наявного вiдкритого ключа: ");
                string additionalPublicKeyPath = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Оригiнальний текст: " + additionalOriginal);
                var additionalEncrypted = rsaParams.EncryptData(additionalPublicKeyPath, Encoding.Unicode.GetBytes(additionalOriginal));
                Console.WriteLine("Зашифрований текст: " + Convert.ToBase64String(additionalEncrypted));
            }
        }
    }
}