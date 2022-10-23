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
            string variant;

            // ініціалізація солі(ключів)
            byte[] saltMD5, saltSHA1, saltSHA256, saltSHA384, saltSHA512, passwordInArray, passwordCheckInArray;

            // 10 значень із кроком 50'000; перше значення = номер варіанта * 10'000;
            int numOfRounds0 = 220000;


            // ініціалізація користувача
            Console.Write(" Enter your username ->  ");
            username = Console.ReadLine();                          // логін
            Console.Write(" Enter your password ->  ");
            password = Console.ReadLine();                          // пароль
            Console.WriteLine();
            Console.WriteLine();

            passwordInArray = Encoding.Unicode.GetBytes(password);  // кладемо пароль у список

            // створюємо ключі
            saltMD5 = cryptoKey(saltLengthMD5);
            saltSHA1 = cryptoKey(saltLengthSHA1);
            saltSHA256 = cryptoKey(saltLengthSHA256);
            saltSHA384 = cryptoKey(saltLengthSHA384);
            saltSHA512 = cryptoKey(saltLengthSHA512);


            do
            {
                Console.WriteLine();
                Console.WriteLine("Chose hashing algorithm:");
                Console.WriteLine("1 - MD5");
                Console.WriteLine("2 - SHA1");
                Console.WriteLine("3 - SHA256");
                Console.WriteLine("4 - SHA384");
                Console.WriteLine("5 - SHA512");
                Console.WriteLine("0 - exit");
                Console.WriteLine();
                Console.Write("Type: ");
                variant = Console.ReadLine();
                Console.WriteLine();

                if (variant == "1")
                {
                    // Для хешування-MD5 використовуємо різні числа
                    var Rfc0MD5 = PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds0, saltLengthMD5);

                    // шукаємо час для кожного числа
                    var time0MD5 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds0, saltLengthMD5);
                    });


                    Console.WriteLine(" " + numOfRounds0 + " MD5: " + Convert.ToBase64String(Rfc0MD5));
                    Console.WriteLine(" Iterations <" + numOfRounds0 + ">, Elapsed Time: " + time0MD5);
                    Console.WriteLine();
                    Console.WriteLine();


                    // початок аутентифікації
                    Console.WriteLine(" Starting user authentification ");
                    Console.Write(" Enter your username ->  ");
                    usernameCheck = Console.ReadLine();               // логін
                    Console.Write(" Enter your password ->  ");
                    passwordCheck = Console.ReadLine();               // пароль
                    Console.WriteLine();

                    passwordCheckInArray = Encoding.Unicode.GetBytes(passwordCheck);

                    var Rfc1MD5 = PBKDF2.HashPassword(passwordCheckInArray, saltMD5, numOfRounds0, saltLengthMD5); // хешування
                    var time1MD5 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordCheckInArray, saltMD5, numOfRounds0, saltLengthMD5);
                    });

                    Console.WriteLine(" " + numOfRounds0 + " MD5: " + Convert.ToBase64String(Rfc1MD5));
                    Console.WriteLine(" Iterations <" + numOfRounds0 + ">, Elapsed Time: " + time1MD5);
                    Console.WriteLine();
                    Console.WriteLine();

                    Console.Write(CheckHash(Rfc0MD5, Rfc1MD5, username, usernameCheck));
                    Console.WriteLine();
                    Console.WriteLine();

                }

                else if (variant == "2")
                {
                    // Для хешування-SHA1 використовуємо різні числа
                    var Rfc0SHA1 = PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds0, saltLengthSHA1);

                    // шукаємо час для кожного числа
                    var time0SHA1 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds0, saltLengthSHA1);
                    });


                    Console.WriteLine(" " + numOfRounds0 + " SHA1: " + Convert.ToBase64String(Rfc0SHA1));
                    Console.WriteLine(" Iterations <" + numOfRounds0 + ">, Elapsed Time: " + time0SHA1);
                    Console.WriteLine();


                    // початок аутентифікації
                    Console.WriteLine(" Starting user authentification ");
                    Console.WriteLine(" -----------------------------------------------------------------------------------------------------------------------");
                    Console.Write(" Enter your username ->  ");
                    usernameCheck = Console.ReadLine();               // логін
                    Console.Write(" Enter your password ->  ");
                    passwordCheck = Console.ReadLine();               // пароль
                    Console.WriteLine();

                    passwordCheckInArray = Encoding.Unicode.GetBytes(passwordCheck);

                    var Rfc1SHA1 = PBKDF2.HashPassword(passwordCheckInArray, saltSHA1, numOfRounds0, saltLengthSHA1); // Хешування
                    var time1SHA1 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordCheckInArray, saltSHA1, numOfRounds0, saltLengthSHA1);
                    });

                    
                    Console.WriteLine(" " + numOfRounds0 + " SHA1: " + Convert.ToBase64String(Rfc1SHA1));
                    Console.WriteLine(" Iterations <" + numOfRounds0 + ">, Elapsed Time: " + time1SHA1);
                    Console.WriteLine();
                    Console.WriteLine();

                    Console.Write(CheckHash(Rfc0SHA1, Rfc1SHA1, username, usernameCheck));
                    Console.WriteLine();
                    Console.WriteLine();

                }

                else if (variant == "3")
                {
                    // Для хешування-SHA256 використовуємо різні числа
                    var Rfc0SHA256 = PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds0, saltLengthSHA256);

                    // шукаємо час для кожного числа
                    var time0SHA256 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds0, saltLengthSHA256);
                    });

                    
                    Console.WriteLine(" " + numOfRounds0 + " SHA256: " + Convert.ToBase64String(Rfc0SHA256));
                    Console.WriteLine(" Iterations <" + numOfRounds0 + ">, Elapsed Time: " + time0SHA256);
                    Console.WriteLine();
                    Console.WriteLine();


                    // початок аутентифікації
                    Console.Write(" Enter your username ->  ");
                    usernameCheck = Console.ReadLine();               // логін
                    Console.Write(" Enter your password ->  ");
                    passwordCheck = Console.ReadLine();               // пароль
                    Console.WriteLine();

                    passwordCheckInArray = Encoding.Unicode.GetBytes(passwordCheck);

                    var Rfc1SHA256 = PBKDF2.HashPassword(passwordCheckInArray, saltSHA256, numOfRounds0, saltLengthSHA256); // Хешування
                    var time1SHA256 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordCheckInArray, saltSHA256, numOfRounds0, saltLengthSHA256);
                    });

                    
                    Console.WriteLine(" " + numOfRounds0 + " SHA256: " + Convert.ToBase64String(Rfc1SHA256));
                    Console.WriteLine(" Iterations <" + numOfRounds0 + ">, Elapsed Time: " + time1SHA256);
                    Console.WriteLine();
                    Console.WriteLine();

                    
                    Console.Write(CheckHash(Rfc0SHA256, Rfc1SHA256, username, usernameCheck));
                    Console.WriteLine();
                    Console.WriteLine();

                }

                else if (variant == "4")
                {
                    Console.WriteLine("  | You may need to wait a little bit longer");
                    Console.WriteLine();
                    // Для хешування-SHA384 використовуємо різні числа
                    var Rfc0SHA384 = PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds0, saltLengthSHA384);

                    // шукаємо час для кожного числа
                    var time0SHA384 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds0, saltLengthSHA384);
                    });

                    
                    Console.WriteLine(" " + numOfRounds0 + " SHA384: " + Convert.ToBase64String(Rfc0SHA384));
                    Console.WriteLine(" Iterations <" + numOfRounds0 + ">, Elapsed Time: " + time0SHA384);
                    Console.WriteLine();
                    Console.WriteLine();

                    // початок аутентифікації
                    Console.Write(" Enter your username ->  ");
                    usernameCheck = Console.ReadLine();               // логін
                    Console.Write(" Enter your password ->  ");
                    passwordCheck = Console.ReadLine();               // пароль
                    Console.WriteLine();

                    passwordCheckInArray = Encoding.Unicode.GetBytes(passwordCheck);

                    var Rfc1SHA384 = PBKDF2.HashPassword(passwordCheckInArray, saltSHA384, numOfRounds0, saltLengthSHA384); // Хешування
                    var time1SHA384 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordCheckInArray, saltSHA384, numOfRounds0, saltLengthSHA384);
                    });

                    
                    Console.WriteLine(" " + numOfRounds0 + " SHA384: " + Convert.ToBase64String(Rfc1SHA384));
                    Console.WriteLine(" Iterations <" + numOfRounds0 + ">, Elapsed Time: " + time1SHA384);
                    Console.WriteLine();
                    Console.WriteLine();

                    Console.Write(CheckHash(Rfc0SHA384, Rfc1SHA384, username, usernameCheck));
                    Console.WriteLine();
                    Console.WriteLine();

                }

                else if (variant == "5")
                {
                    // Для хешування-SHA512 використовуємо різні числа
                    var Rfc0SHA512 = PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds0, saltLengthSHA512);

                    // шукаємо час для кожного числа
                    var time0SHA512 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds0, saltLengthSHA512);
                    });

                    
                    Console.WriteLine(" " + numOfRounds0 + " SHA512: " + Convert.ToBase64String(Rfc0SHA512));
                    Console.WriteLine(" Iterations <" + numOfRounds0 + ">, Elapsed Time: " + time0SHA512);
                    Console.WriteLine();
                    Console.WriteLine();


                    // початок аутентифікації
                    Console.Write(" Enter your username ->  ");
                    usernameCheck = Console.ReadLine();               // логін
                    Console.Write(" Enter your password ->  ");
                    passwordCheck = Console.ReadLine();               // пароль
                    Console.WriteLine();

                    passwordCheckInArray = Encoding.Unicode.GetBytes(passwordCheck);

                    var Rfc1SHA512 = PBKDF2.HashPassword(passwordCheckInArray, saltSHA512, numOfRounds0, saltLengthSHA512); // Хешування
                    var time1SHA512 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordCheckInArray, saltSHA512, numOfRounds0, saltLengthSHA512);
                    });

                    
                    Console.WriteLine(" " + numOfRounds0 + " SHA512: " + Convert.ToBase64String(Rfc1SHA512));
                    Console.WriteLine(" Iterations <" + numOfRounds0 + ">, Elapsed Time: " + time1SHA512);
                    Console.WriteLine();
                    Console.WriteLine();

                    Console.Write(CheckHash(Rfc0SHA512, Rfc1SHA512, username, usernameCheck));
                    Console.WriteLine();
                    Console.WriteLine();

                }

            } while (variant != "0");

        }
    

        
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
        public static string CheckHash(byte[] hash1, byte[] hash2, string login1, string login2)
        {
            if (login1 == login2 && Convert.ToBase64String(hash1) == Convert.ToBase64String(hash2))
            {
                Console.WriteLine(" Login is accurate");
                Console.WriteLine(" Hash of password is accurate");
                Console.WriteLine();
                Console.WriteLine("Access permitted. Welcome to the system");

            }
            else if (login1 == login2 && Convert.ToBase64String(hash1) != Convert.ToBase64String(hash2))
            {
                Console.WriteLine(" Login is accurate");
                Console.WriteLine(" Hash isn`t accurate. Password is corrupted");
                Console.WriteLine();
                Console.WriteLine("Access denied. You were wrong");
            }
            else if (login1 != login2 && Convert.ToBase64String(hash1) == Convert.ToBase64String(hash2))
            {
                Console.WriteLine(" Login isn`t accurate");
                Console.WriteLine(" Hash is accurate. Password is correct");
                Console.WriteLine();
                Console.WriteLine("Access denied. You were wrong");
            }
            else
            {
                Console.WriteLine(" Login isn`t accurate");
                Console.WriteLine(" Hash isn`t accurate. Password is corrupted");
                Console.WriteLine();
                Console.WriteLine("Access denied. You were totally wrong");
            }
            return " ";
        }
        public static byte[] cryptoKey(int seed)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider()) // class, that we are using, об`явлення
            {
                var Key = new byte[seed];
                rng.GetBytes(Key);
                return Key;
            }
        }

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
        public static TimeSpan MeasureRunTime(Action codeToRun)
        {
            var watch = Stopwatch.StartNew();
            codeToRun();
            watch.Stop();
            return watch.Elapsed;
        }

    }
}
