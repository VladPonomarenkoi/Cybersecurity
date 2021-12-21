using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace lab11x12
{
    class User
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public string[] Roles { get; set; }
    }

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

        public static byte[] HashPasswordSHA256(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA256))
            {
                return rfc2898.GetBytes(32);
            }
        }
    }

    class Protector
    {
        private static Dictionary<string, User> _users = new Dictionary<string, User>();

        public static void Register(string userName, string password, string[] roles = null)
        {
            if (_users.ContainsKey(userName))
            {
                Console.WriteLine("This user is already registered!");
            }
            else
            {
                byte[] generatedSalt = PBKDF2.GenerateSalt();
                byte[] hashedPassword = PBKDF2.HashPasswordSHA256(Encoding.UTF8.GetBytes(password), generatedSalt, 2500);
                User newuser = new User();
                newuser.Login = userName;
                newuser.PasswordHash = Convert.ToBase64String(hashedPassword);
                newuser.Salt = generatedSalt;
                newuser.Roles = roles;

                _users.Add(userName, newuser);

                Console.WriteLine("New user was successfully registered!");
            }
        }

        public static bool CheckPassword(string userName, string password)
        {
            if (_users.ContainsKey(userName))
            {
                User user = _users[userName];
                byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedPassword = PBKDF2.HashPasswordSHA256(enteredPasswordBytes, user.Salt, 2500);
                string enteredPasswordHash = Convert.ToBase64String(hashedPassword);

                if (enteredPasswordHash == user.PasswordHash)
                {
                    Console.WriteLine("This password is correct!");
                    return true;
                }
                else
                {
                    Console.WriteLine("This password is incorrect!");
                    return false;
                }


            }
            else
            {
                Console.WriteLine("There is no registered user with this name");
                return false;
            }
        }

        public static void LogIn(string userName, string password)
        {
            var identity = new GenericIdentity(userName, "OIBAuth");
            var principal = new GenericPrincipal(identity, _users[userName].Roles);
            System.Threading.Thread.CurrentPrincipal = principal;
            Console.WriteLine("You were logged in!");
        }

        public static void OnlyForAdminsFeature()
        {
            if (Thread.CurrentPrincipal == null)
            {
                throw new SecurityException("Thread.CurrentPrincipal cannot be null.");
            }
            if (!Thread.CurrentPrincipal.IsInRole("Admin"))
            {
                throw new SecurityException("User must be a member of Admins to access this feature.");
            }
            Console.WriteLine("You have access to this secure feature.");
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number of registers:");
            int num_registers = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < num_registers; i++)
            {
                Console.WriteLine("For registration enter login, password and roles.");
                Console.WriteLine("Enter login: ");
                string login = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Enter password: ");
                string password = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Enter roles separated by comma:");
                string rolesString = Convert.ToString(Console.ReadLine());

                Regex sWhitespace = new Regex(@"\s+");
                string rolesWithoutSpaces = sWhitespace.Replace(rolesString, "");
                string[] roles = rolesWithoutSpaces.Split(',');

                Protector.Register(login, password, roles);
                Console.WriteLine();
            }

            Console.WriteLine("All users were registered");

            Console.WriteLine();

            Console.WriteLine("To log in, please, enter your credentials:");
            Console.WriteLine("Enter login: ");
            string enteredLogin = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter password: ");
            string enteredPassword = Convert.ToString(Console.ReadLine());

            if (Protector.CheckPassword(enteredLogin, enteredPassword))
            {
                Protector.LogIn(enteredLogin, enteredPassword);

                try
                {
                    Protector.OnlyForAdminsFeature();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.GetType()}: {ex.Message}");
                }
            }
        }
    }
}
