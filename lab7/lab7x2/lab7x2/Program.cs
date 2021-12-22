using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab7_2
{
    class Program
    {
        public void AssignNewKey(string publicKey = "Public.xml", 
            string privateKey = "Private.xml")
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                File.WriteAllText(publicKey, rsa.ToXmlString(false));
                File.WriteAllText(privateKey, rsa.ToXmlString(true));
            }
        }
        public byte[] EncryptData(byte[] dataToEncrypt, string publicKey = "Public.xml")
        {
            byte[] cipherbytes;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKey));
                cipherbytes = rsa.Encrypt(dataToEncrypt, false);
            }
            return cipherbytes;
        }
        public byte[] DecryptData(byte[] dataToDecrypt, string privateKey = "Private.xml")
        {
            byte[] p;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(privateKey));
                p = rsa.Decrypt(dataToDecrypt, false);
            }
            return p;
        }
        static void Main(string[] args)
        {
            var rsap = new Program();
            const string original = "Vlad Hi";
            rsap.AssignNewKey();
            Console.WriteLine(" Original Text = " + original);
            var enc = rsap.EncryptData(Encoding.UTF8.GetBytes(original));
            var dec = rsap.DecryptData(enc);
            Console.WriteLine(" Encrypted Text = " + Convert.ToBase64String(enc));
            Console.WriteLine(" Decrypted Text = " + Encoding.Default.GetString(dec));

        }
    }
}

