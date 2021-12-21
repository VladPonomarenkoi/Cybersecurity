using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab9x10
{
    class Program
    {
        public static byte[] ComputeHashSha512(byte[] toBeHashed)
        {
            using (var sha512 = SHA512.Create())
            {
                return sha512.ComputeHash(toBeHashed);
            }
        }

        private readonly static string CspContainerName = "RsaKeyContainer";
        public void AssignNewKey()
        {
            CspParameters cspParameters = new CspParameters(1)
            {
                KeyContainerName = CspContainerName,

                ProviderName = "Microsoft Strong Cryptographic Provider"
            };
            var rsa = new RSACryptoServiceProvider(cspParameters)
            {
                PersistKeyInCsp = true
            };
            File.WriteAllText("Ponomarenko.xml", rsa.ToXmlString(false));
        }

        public byte[] SignData(byte[] hashOfDataToSign)
        {

            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
            };

            using (var rsa = new RSACryptoServiceProvider(2048, cspParams))
            {
                rsa.PersistKeyInCsp = true;
                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm("SHA512");
                return rsaFormatter.CreateSignature(hashOfDataToSign);
            }
        }
        public bool VerifySignature(byte[] hashOfDataToSign, byte[] signature)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText("Ponomarenko.xml"));
                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm("SHA512");
                return rsaDeformatter.VerifySignature(hashOfDataToSign, signature);
            }
        }

        static void Main(string[] args)
        {
            var doc = Encoding.UTF8.GetBytes("Signed by Vlad Ponomarenko");
            byte[] hashedDocument = ComputeHashSha512(doc);
            var digitalSignature = new Program();
            digitalSignature.AssignNewKey();
            var signature = digitalSignature.SignData(hashedDocument);
            var verified = digitalSignature.VerifySignature(hashedDocument, signature);

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Digital Signature Demonstration in .NET");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(" Original Text = " + Encoding.Default.GetString(doc));
            Console.WriteLine();
            Console.WriteLine(" Digital Signature = " + Convert.ToBase64String(signature));
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(verified
            ? "Electronic digital signature correct!"
            : "Electronic digital signature is not known!");
            Console.WriteLine("--------------------------------------------------");
            Console.ReadLine();
        }

    }
}