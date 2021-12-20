using System;
using System.Text;
using System.Security.Cryptography;

namespace lab3x2
{
    class Program
    {
        static byte[] Md5(byte[] DataForHash)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(DataForHash);
            }
        }
        static void Main(string[] args)
        {
            Guid guid = new Guid("564c8da6-0440-88ec-d453-0bbad57c6036");
            Console.WriteLine(guid);
            int mem;
            for (int i = 100000000; i < 200000003; i++)
            {
                var md5 = Md5(Encoding.Unicode.GetBytes(i.ToString().Substring(1, 8)));
                if (new Guid(md5) == guid)
                {
                    Console.WriteLine("Пароль= " + i);
                    Console.WriteLine(Convert.ToBase64String(md5));
                    mem = i;
                }
            }

        }
    }
}
