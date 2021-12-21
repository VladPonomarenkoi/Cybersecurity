using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab8
{
    class Program
    {
        private readonly static string CspContainerName = "Rsa";
        public static void GenerateKeys(string publicKeyPath)
        {
            CspParameters cspParameters = new CspParameters(1)
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore,
                ProviderName = "Microsoft Strong Cryptographic Provider"
            };
            using (var rsa = new RSACryptoServiceProvider(2048, cspParameters))
            {
                rsa.PersistKeyInCsp = true;
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
            }
        }
        public static void EncryptData(string publicKeyPath, byte[] dataToEncrypt, string chipherTextPath)
        {
            byte[] chipherBytes;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                chipherBytes = rsa.Encrypt(dataToEncrypt, true);
            }
            File.WriteAllBytes(chipherTextPath, chipherBytes);
        }
        public static byte[] DecryptData(string chipherTextPath)
        {
            byte[] chipherBytes = File.ReadAllBytes(chipherTextPath);
            byte[] plainTextBytes;
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            using (var rsa = new RSACryptoServiceProvider(2048, cspParams))
            {
                rsa.PersistKeyInCsp = true;
                plainTextBytes = rsa.Decrypt(chipherBytes, true);
            }
            return plainTextBytes;
        }
        static void Main(string[] args)
        {
            GenerateKeys("Ponomarenko.xml");
            Console.WriteLine("Press 1 to encrypt, press 2 to decrypt: ");
            string temp = Convert.ToString(Console.ReadLine());
            if (temp == "1")
            {
                Console.WriteLine("Enter message to encrypt: ");
                string message = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Enter the name of the recipient public key XML file [ex. MyKey.xml]: ");
                string recPublicKey = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Enter the file name where to encrypt the message [ex. MyData.dat]: ");
                string datFile = Convert.ToString(Console.ReadLine());
                EncryptData(recPublicKey, Encoding.UTF8.GetBytes(message), datFile);
                Console.WriteLine("The message is encrypted.");
            }
            else if (temp == "2")
            {
                Console.WriteLine("Enter the name of the file to decrypt [MyData.dat]: ");
                string fileToDecrypt = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Decrypted Message: " + Encoding.UTF8.GetString(DecryptData(fileToDecrypt)));
                Console.WriteLine("Done! The message was decrypted.");
            }
            else
            {
                Console.WriteLine("Incorrect Data!");
            }
        }
    }
}