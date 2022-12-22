﻿using System;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;


class Authorization
{
    static void Main()
    {
        var logger = NLog.LogManager.GetCurrentClassLogger();
        logger.Trace("Program is running");
        string username;
        int k = 1;
        int i = 0;
        string[][] data = new string[k][];
        string[][] data_prev = new string[10][];
        do
        {
            {
                Console.WriteLine("Admin");
                Console.WriteLine("User_1");
                Console.WriteLine("User_2");
                Console.WriteLine("User_3\n");
                Console.Write("Enter your role in the community: ");
                string role = Console.ReadLine();
                string[] roles = { "Admin", "User_1", "User_2", "User_3" };

                if (!roles.Contains(role))
                {
                    logger.Fatal("Entered wrong role {wrole}", role);
                }

                else
                {
                    string[] model = new[] { role.ToLower() };
                    Console.Write("Enter your login: ");
                    username = Console.ReadLine();
                    Console.Write("Enter your password: ");
                    string password = null;
                    while (true)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter)
                            break;
                        password += key.KeyChar;
                    }
                    Console.WriteLine();

                    string[] info = { username, password, role };
                    data[i] = info;
                    data_prev[i] = info;

                    logger.Trace("Authorization");
                    if (data.Count() == 1)
                    {
                        logger.Info("Current user {info}", data[i]);
                    }
                    else
                    {
                        logger.Info("Previous user {info}", data_prev[i - 1]);
                        logger.Info("Current user {info}", info);
                    }
                    k++;
                    Array.Resize(ref data, k);
                    i++;

                    Protector.Register(username, password, model);

                    logger.Trace("Authentication");

                    Console.Write("\nConfirm your login: ");
                    string username_a = Console.ReadLine();
                    Console.Write("Confirm your password: ");
                    string password_a = null;
                    while (true)
                    {
                        var key_a = Console.ReadKey(true);
                        if (key_a.Key == ConsoleKey.Enter)
                            break;
                        password_a += key_a.KeyChar;
                    }
                    Console.WriteLine("\n");

                    logger.Trace("Confirmation");
                    logger.Info("Login {login} Password {password} Role {role}", username_a, password_a, role);

                    Protector.LogIn(username_a, password_a);
                    Console.WriteLine("\nList of actions community is responsible for:");
                    Console.WriteLine("1 - Find a long-term strategic planning");
                    Console.WriteLine("2 - Find projections of profit/loss");
                    Console.WriteLine("3 - Find on-hand technical support");
                    Console.WriteLine("4 - Find documents with all regulations");
                    Console.Write("\nWhat do you want to have an access to: ");
                    int option = Convert.ToInt32(Console.ReadLine());

                    if (option == null)
                    {
                        logger.Fatal(option);
                    }
                    logger.Debug(option);
                    logger.Trace("Affiliation");

                    Role.Check(option);
                }
            }

        }
        while (true);
    }

    public class User
    {
        public string Login;
        public string PasswordHash;
        public byte[] Salt;
        public string[] Roles;

        public User(string login, string passwordhash, byte[] salt, string[] roles = null)
        {
            Login = login;
            PasswordHash = passwordhash;
            Salt = salt;
            Roles = roles;
        }
    }

    public class Hashing
    {
        public static byte[] GenerateSalt()
        {
            const int salt_length = 32;
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                var rnd = new byte[salt_length];
                randomNumberGenerator.GetBytes(rnd);

                return rnd;
            }
        }

        public static byte[] HashPassword(string passwordToHash, int numOfRounds, byte[] gensalt)
        {
            var hashedPassword = HashPasswordhash(Encoding.UTF8.GetBytes(passwordToHash), gensalt, numOfRounds);
            return hashedPassword;
        }

        public static byte[] HashPasswordhash(byte[] toBeHashed, byte[] salt, int numOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numOfRounds, HashAlgorithmName.SHA256))
            {
                return rfc2898.GetBytes(32);
            }
        }
    }

    public class Protector
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private static Dictionary<string, User> _users = new Dictionary<string, User>();

        public static User Register(string login, string password, string[] roles)
        {
            if (Iftherename(login, out User existed))
            {

                logger.Trace("Check in");
                logger.Warn("Login {login} Password {password}", login, password);

                Console.WriteLine("\n!!! Such user is already exists !!!\n\n");
                Console.WriteLine("Enter another login and password");
                Console.Write("Enter new login: ");
                string newname = Console.ReadLine();
                Console.Write("Enter new password: ");
                string newpas = null;
                while (true)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    newpas += key.KeyChar;
                }

                logger.Info("\nPrevious login {login_prev} Previous password {password_prev}", login, password);
                logger.Info("New login {login_new} New password {password_new}", newname, newpas);

                Console.WriteLine();
                byte[] salt = Hashing.GenerateSalt();
                byte[] hashpas = Hashing.HashPassword(newpas, 10, salt);
                string strnewpas = Convert.ToBase64String(hashpas);
                User users = new User(newname, strnewpas, salt, roles);
                _users.Add(newname, users);

                return existed;
            }
            else
            {
                byte[] salt = Hashing.GenerateSalt();
                byte[] hashpas = Hashing.HashPassword(password, 10, salt);
                string strhashpas = Convert.ToBase64String(hashpas);
                User users = new User(login, strhashpas, salt, roles);
                _users.Add(login, users);

                return users;
            }
        }
        public static bool CheckPassword(string login, string passwordtocompare)
        {
            if (!Iftherename(login, out User user))
            {
                Console.WriteLine("Such login is free");
                return false;
            }
            byte[] salttocompare = user.Salt;
            var hashtocompare = Hashing.HashPassword(passwordtocompare, 10, salttocompare);
            string comparehash = Convert.ToBase64String(hashtocompare);
            if (Equals(comparehash, user.PasswordHash))
            {
                logger.Trace("Authentication was succeed");
                return true;
            }
            else
            {
                logger.Error("Login {login} Password {password}", login, passwordtocompare);
                logger.Trace("Authentication was NOT succeed");
                return false;
            }
        }

        private static bool Iftherename(string userdata, out User data) =>
            _users.TryGetValue(userdata, out data);

        public static void LogIn(string userName, string password)
        {
            if (CheckPassword(userName, password))
            {
                var identity = new GenericIdentity(userName, "OIBAuth");
                var principal = new GenericPrincipal(identity, _users[userName].Roles);
                Thread.CurrentPrincipal = principal;
            }
        }
    }

    public class Role
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void OnlyForAdmins()
        {
            if (Thread.CurrentPrincipal == null)
            {
                logger.Error("Thread.CurrentPrincipal cannot be null.");
            }
            if (!Thread.CurrentPrincipal.IsInRole("Admin"))
            {
                logger.Error("User must be a Admin to access this feature.");
            }
            else
            {
                Console.WriteLine("\nAll material is on your page");
            }
        }
        public static void OnlyForFinancemanager()
        {
            if (Thread.CurrentPrincipal == null)
            {
                logger.Error("Thread.CurrentPrincipal cannot be null.");
            }
            if (!Thread.CurrentPrincipal.IsInRole("User_1"))
            {
                logger.Error("User must be a User_1 to access this feature.");
            }
            else
            {
                Console.WriteLine("\nAll material is on your page");
            }
        }
        public static void OnlyForITTechnician()
        {
            if (Thread.CurrentPrincipal == null)
            {
                logger.Error("Thread.CurrentPrincipal cannot be null.");
            }
            if (!Thread.CurrentPrincipal.IsInRole("User_2"))
            {
                logger.Error("User must be a User_2 to access this feature.");
            }
            else
            {
                Console.WriteLine("\nAll material is on your page");
            }
        }
        public static void OnlyForSafetyManager()
        {
            if (Thread.CurrentPrincipal == null)
            {
                logger.Error("Thread.CurrentPrincipal cannot be null.");
            }
            if (!Thread.CurrentPrincipal.IsInRole("User_3"))
            {
                logger.Error("User must be a User_3 to access this feature.");
            }
            else
            {
                Console.WriteLine("\nAll material is on your page");
            }
        }

        public static void Check(int opt)
        {
            if (opt == 1)
            {
                try
                {
                    OnlyForAdmins();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
            if (opt == 2)
            {
                try
                {
                    OnlyForFinancemanager();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
            if (opt == 3)
            {
                try
                {
                    OnlyForITTechnician();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
            if (opt == 4)
            {
                try
                {
                    OnlyForSafetyManager();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
            if (opt > 4 || opt < 0)
            {
                logger.Fatal("Entered unknown command {opt}", opt);
            }
        }
    }
}