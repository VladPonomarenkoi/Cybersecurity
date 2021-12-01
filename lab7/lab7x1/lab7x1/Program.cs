using System;
using System.Security.Cryptography;
using System.Text;

namespace lab7x1
{
    class RSAWithRSAParameterKey
    {
        private RSAParameters _publicKey;
        private RSAParameters _privateKey;

        public void AssignNewKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                _publicKey = rsa.ExportParameters(false);
                _privateKey = rsa.ExportParameters(true);
            }
        }

        public byte[] EncryptData(byte[] dataToEncrypt)
        {
            byte[] cypherbytes;

            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_publicKey);

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

            const string original = "Subscribe";

            rsaParams.AssignNewKey();

            Console.WriteLine("Оригiнальний текст: " + original);
            Console.WriteLine();

            var encrypted = rsaParams.EncryptData(Encoding.Unicode.GetBytes(original));
            Console.WriteLine("Зашифрований текст: " + Convert.ToBase64String(encrypted));

            var decrypted = rsaParams.DecryptData(encrypted);
            Console.WriteLine();
            Console.WriteLine("Розшифрований текст: " + Encoding.Default.GetString(decrypted));
        }
    }
}
