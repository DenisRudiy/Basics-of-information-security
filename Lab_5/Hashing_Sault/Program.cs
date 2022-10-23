using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using System.Diagnostics;


namespace lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            // Довжина ключів для хешування
            int saltLengthMD5 = 16;
            int saltLengthSHA1 = 20;
            int saltLengthSHA256 = 32;
            int saltLengthSHA384 = 48;
            int saltLengthSHA512 = 64;

            // ініціалізація паролів та логінів
            string username;
            string password;
            string usernameCheck;
            string passwordCheck;

            // ініціалізація солі(ключів)
            byte[] saltMD5, saltSHA1, saltSHA256, saltSHA384, saltSHA512, passwordInArray;


            // ініціалізація користувача
            Console.Write(" Enter your username ->  ");
            username = Console.ReadLine();                          // логін
            Console.Write(" Enter your password ->  ");
            password = Console.ReadLine();                          // пароль
            Console.WriteLine();

            passwordInArray = Encoding.Unicode.GetBytes(password);  // кладемо пароль у список

            // створюємо ключі
            saltMD5 = cryptoKey(saltLengthMD5);
            saltSHA1 = cryptoKey(saltLengthSHA1);
            saltSHA256 = cryptoKey(saltLengthSHA256);
            saltSHA384 = cryptoKey(saltLengthSHA384);
            saltSHA512 = cryptoKey(saltLengthSHA512);

            // Хешування
            var HmacMD5 = SaltedHash.ComputeHmacSHA1(passwordInArray, saltMD5);
            var HmacSHA1 = SaltedHash.ComputeHmacSHA1(passwordInArray, saltSHA1);
            var HmacSHA256 = SaltedHash.ComputeHmacSHA256(passwordInArray, saltSHA256);
            var HmacSHA384 = SaltedHash.ComputeHmacSHA384(passwordInArray, saltSHA384);
            var HmacSHA512 = SaltedHash.ComputeHmacSHA512(passwordInArray, saltSHA512);

            
            Console.WriteLine(" MD5: " + Convert.ToBase64String(HmacMD5));
            Console.WriteLine(" SHA1: " + Convert.ToBase64String(HmacSHA1));                                                                                // for delay
            Console.WriteLine(" SHA256: " + Convert.ToBase64String(HmacSHA256));
            Console.WriteLine(" SHA384: " + Convert.ToBase64String(HmacSHA384));
            Console.WriteLine(" SHA512:  " + Convert.ToBase64String(HmacSHA512));
            Console.WriteLine();
            Console.WriteLine();


            // аутентифікація користувача
            Console.Write(" Enter your username ->  ");
            usernameCheck = Console.ReadLine();               // логін
            Console.Write(" Enter your password ->  ");
            passwordCheck = Console.ReadLine();               // пароль
            Console.WriteLine();
            Console.WriteLine();

            var Hmac2MD5 = SaltedHash.ComputeHmacSHA1(passwordInArray, saltMD5);
            var Hmac2SHA1 = SaltedHash.ComputeHmacSHA1(passwordInArray, saltSHA1);
            var Hmac2SHA256 = SaltedHash.ComputeHmacSHA256(passwordInArray, saltSHA256);  
            var Hmac2SHA384 = SaltedHash.ComputeHmacSHA384(passwordInArray, saltSHA384);
            var Hmac2SHA512 = SaltedHash.ComputeHmacSHA512(passwordInArray, saltSHA512);


            Console.WriteLine(" MD5: " + Convert.ToBase64String(Hmac2MD5));
            Console.WriteLine(" SHA1: " + Convert.ToBase64String(Hmac2SHA1));                                                                              // for delay
            Console.WriteLine(" SHA256: " + Convert.ToBase64String(Hmac2SHA256));
            Console.WriteLine(" SHA384: " + Convert.ToBase64String(Hmac2SHA384));
            Console.WriteLine(" SHA512:  " + Convert.ToBase64String(Hmac2SHA512));
            Console.WriteLine();
            Console.WriteLine();


            // перевірка
            Console.Write("  MD5 check: ");
            Console.Write(CheckHashOnly(HmacMD5, Hmac2MD5));
            Console.Write(" SHA1 check: ");
            Console.Write(CheckHashOnly(HmacSHA1, Hmac2SHA1));
            Console.Write(" SHA256 check: ");
            Console.Write(CheckHashOnly(HmacSHA256, Hmac2SHA256));
            Console.Write(" SHA384 check: ");
            Console.Write(CheckHashOnly(HmacSHA384, Hmac2SHA384));
            Console.Write(" SHA512 check: ");
            Console.Write(CheckHashOnly(HmacSHA512, Hmac2SHA512));
            Console.WriteLine();
            Console.WriteLine();

        }

        // checking hash for accuracy
        public static string CheckHashOnly(byte[] hash1, byte[] hash2)
        {
            if (Convert.ToBase64String(hash1) == Convert.ToBase64String(hash2))
            {
                Console.WriteLine("Hash of password is accurate");
            }
            else
            {
                Console.WriteLine("Hash isn`t accurate. Password is corrupted");
            }
            return " ";
        }

        // checking hash and login for accuracy
        public static string CheckHash(byte[] hash1, byte[] hash2, string login1, string login2)
        {
            if (login1 == login2 && Convert.ToBase64String(hash1) == Convert.ToBase64String(hash2))
            {
                Console.WriteLine(" Login is accurate");
                Console.WriteLine(" Hash of password is accurate");
                Console.WriteLine();
                Console.WriteLine("  | Access permitted. Welcome to the system");

            }
            else if (login1 == login2 && Convert.ToBase64String(hash1) != Convert.ToBase64String(hash2))
            {
                Console.WriteLine(" Login is accurate");
                Console.WriteLine(" Hash isn`t accurate. Password is corrupted");
                Console.WriteLine();
                Console.WriteLine("  | Access denied. You were wrong");
            }
            else if (login1 != login2 && Convert.ToBase64String(hash1) == Convert.ToBase64String(hash2))
            {
                Console.WriteLine(" Login isn`t accurate");
                Console.WriteLine(" Hash is accurate. Password is correct");
                Console.WriteLine();
                Console.WriteLine("  | Access denied. You were wrong");
            }
            else
            {
                Console.WriteLine(" Login isn`t accurate");
                Console.WriteLine(" Hash isn`t accurate. Password is corrupted");
                Console.WriteLine();
                Console.WriteLine("  | Access denied. You were totally wrong");
            }
            return " ";
        }

        // generating salt
        public static byte[] cryptoKey(int seed)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider()) // class, that we are using, об`явлення
            {
                var Key = new byte[seed];
                rng.GetBytes(Key);
                return Key;
            }
        }

        // hash + salt functions for option 1
        public class SaltedHash
        {
            private static byte[] Combine(byte[] first, byte[] second)
            {
                var ret = new byte[first.Length + second.Length];
                Buffer.BlockCopy(first, 0, ret, 0, first.Length);
                Buffer.BlockCopy(second, 0, ret, first.Length,
                second.Length);
                return ret;
            }

            public static byte[] ComputeHmacMD5(byte[] toBeHashed, byte[] salt)
            {
                using (var hmac = new HMACMD5(salt))
                {
                    return hmac.ComputeHash(Combine(toBeHashed, salt));
                }
            }

            public static byte[] ComputeHmacSHA1(byte[] toBeHashed, byte[] salt)
            {
                using (var hmac = new HMACSHA1(salt))
                {
                    return hmac.ComputeHash(Combine(toBeHashed, salt));
                }
            }

            public static byte[] ComputeHmacSHA256(byte[] toBeHashed, byte[] salt)
            {
                using (var hmac = new HMACSHA256(salt))
                {
                    return hmac.ComputeHash(Combine(toBeHashed, salt));
                }
            }

            public static byte[] ComputeHmacSHA384(byte[] toBeHashed, byte[] salt)
            {
                using (var hmac = new HMACSHA384(salt))
                {
                    return hmac.ComputeHash(Combine(toBeHashed, salt));
                }
            }

            public static byte[] ComputeHmacSHA512(byte[] toBeHashed, byte[] salt)
            {
                using (var hmac = new HMACSHA512(salt))
                {
                    return hmac.ComputeHash(Combine(toBeHashed, salt));
                }
            }
        }

        // password based key deprivation function
        public class PBKDF2
        {
            public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds, int bytes)
            {
                using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds))
                {
                    return rfc2898.GetBytes(bytes);
                }
            }
        }

        // for time counting
        public static TimeSpan MeasureRunTime(Action codeToRun)
        {
            var watch = Stopwatch.StartNew();
            codeToRun();
            watch.Stop();
            return watch.Elapsed;
        }


    }
}