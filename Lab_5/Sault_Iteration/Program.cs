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

            int numOfRounds = 100;
            int numOfRounds1 = 1000;
            int numOfRounds10 = 10000;
            int numOfRounds100 = 100000;
            int numOfRounds1000 = 1000000;


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

            // Хешування
            // round 1 = 100
            var RfcMD5 = PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds, saltLengthMD5);
            var RfcSHA1 = PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds, saltLengthSHA1);
            var RfcSHA256 = PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds, saltLengthSHA256);
            var RfcSHA384 = PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds, saltLengthSHA384);
            var RfcSHA512 = PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds, saltLengthSHA512);

            var timeMD5 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds, saltLengthMD5);
            });
            var timeSHA1 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds, saltLengthSHA1);
            });
            var timeSHA256 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds, saltLengthSHA256);
            });
            var timeSHA384 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds, saltLengthSHA384);
            });
            var timeSHA512 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds, saltLengthSHA512);
            });

            Console.WriteLine(" Round 100:");
            Console.WriteLine(" 100 MD5: " + Convert.ToBase64String(RfcMD5));
            Console.WriteLine(" 100 SHA1: " + Convert.ToBase64String(RfcSHA1));
            Console.WriteLine(" 100 SHA256: " + Convert.ToBase64String(RfcSHA256));
            Console.WriteLine(" 100 SHA384: " + Convert.ToBase64String(RfcSHA384));
            Console.WriteLine(" 100 SHA512: " + Convert.ToBase64String(RfcSHA512));
            Console.WriteLine();
            Console.WriteLine(" Iterations <" + numOfRounds + ">, Method <MD5>,    Elapsed Time: " + timeMD5);
            Console.WriteLine(" Iterations <" + numOfRounds + ">  Metod <SHA1>,    Elapsed Time: " + timeSHA1);
            Console.WriteLine(" Iterations <" + numOfRounds + ">, Method <SHA256>, Elapsed Time: " + timeSHA256);
            Console.WriteLine(" Iterations <" + numOfRounds + ">, Method <SHA384>, Elapsed Time: " + timeSHA384);
            Console.WriteLine(" Iterations <" + numOfRounds + ">, Method <SHA512>, Elapsed Time: " + timeSHA512);
            Console.WriteLine();
            Console.WriteLine();



            // round 2 = 1000
            var Rfc1MD5 = PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds1, saltLengthMD5);
            var Rfc1SHA1 = PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds1, saltLengthSHA1);
            var Rfc1SHA256 = PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds1, saltLengthSHA256);
            var Rfc1SHA384 = PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds1, saltLengthSHA384);
            var Rfc1SHA512 = PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds1, saltLengthSHA512);

            var time1MD5 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds1, saltLengthMD5);
            });
            var time1SHA1 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds1, saltLengthSHA1);
            });
            var time1SHA256 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds1, saltLengthSHA256);
            });
            var time1SHA384 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds1, saltLengthSHA384);
            });
            var time1SHA512 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds1, saltLengthSHA512);
            });

            Console.WriteLine(" Round 1000");
            Console.WriteLine(" 1000 MD5: " + Convert.ToBase64String(Rfc1MD5));
            Console.WriteLine(" 1000 SHA1: " + Convert.ToBase64String(Rfc1SHA1));
            Console.WriteLine(" 1000 SHA256: " + Convert.ToBase64String(Rfc1SHA256));
            Console.WriteLine(" 1000 SHA384: " + Convert.ToBase64String(Rfc1SHA384));
            Console.WriteLine(" 1000 SHA512: " + Convert.ToBase64String(Rfc1SHA512));
            Console.WriteLine();
            Console.WriteLine(" Iterations <" + numOfRounds1 + ">, Method <MD5>,    Elapsed Time: " + time1MD5);
            Console.WriteLine(" Iterations <" + numOfRounds1 + ">  Metod <SHA1>,    Elapsed Time: " + time1SHA1);
            Console.WriteLine(" Iterations <" + numOfRounds1 + ">, Method <SHA256>, Elapsed Time: " + time1SHA256);
            Console.WriteLine(" Iterations <" + numOfRounds1 + ">, Method <SHA384>, Elapsed Time: " + time1SHA384);
            Console.WriteLine(" Iterations <" + numOfRounds1 + ">, Method <SHA512>, Elapsed Time: " + time1SHA512);
            Console.WriteLine();
            Console.WriteLine();

            // round 3 = 10 000
            var Rfc10MD5 = PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds10, saltLengthMD5);
            var Rfc10SHA1 = PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds10, saltLengthSHA1);
            var Rfc10SHA256 = PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds10, saltLengthSHA256);
            var Rfc10SHA384 = PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds10, saltLengthSHA384);
            var Rfc10SHA512 = PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds10, saltLengthSHA512);

            var time10MD5 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds10, saltLengthMD5);
            });
            var time10SHA1 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds10, saltLengthSHA1);
            });
            var time10SHA256 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds10, saltLengthSHA256);
            });
            var time10SHA384 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds10, saltLengthSHA384);
            });
            var time10SHA512 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds10, saltLengthSHA512);
            });

            Console.WriteLine(" Round 10 000");
            Console.WriteLine(" 10 000 MD5: " + Convert.ToBase64String(Rfc10MD5));
            Console.WriteLine(" 10 000 SHA1: " + Convert.ToBase64String(Rfc10SHA1));
            Console.WriteLine(" 10 000 SHA256: " + Convert.ToBase64String(Rfc10SHA256));
            Console.WriteLine(" 10 000 SHA384: " + Convert.ToBase64String(Rfc10SHA384));
            Console.WriteLine(" 10 000 SHA512: " + Convert.ToBase64String(Rfc10SHA512));
            Console.WriteLine();
            Console.WriteLine(" Iterations <" + numOfRounds10 + ">, Method <MD5>,    Elapsed Time: " + time10MD5);
            Console.WriteLine(" Iterations <" + numOfRounds10 + ">  Metod <SHA1>,    Elapsed Time: " + time10SHA1);
            Console.WriteLine(" Iterations <" + numOfRounds10 + ">, Method <SHA256>, Elapsed Time: " + time10SHA256);
            Console.WriteLine(" Iterations <" + numOfRounds10 + ">, Method <SHA384>, Elapsed Time: " + time10SHA384);
            Console.WriteLine(" Iterations <" + numOfRounds10 + ">, Method <SHA512>, Elapsed Time: " + time10SHA512);
            Console.WriteLine();
            Console.WriteLine();

            // round 4 = 100 000
            var Rfc100MD5 = PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds100, saltLengthMD5);
            var Rfc100SHA1 = PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds100, saltLengthSHA1);
            var Rfc100SHA256 = PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds100, saltLengthSHA256);
            var Rfc100SHA384 = PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds100, saltLengthSHA384);
            var Rfc100SHA512 = PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds100, saltLengthSHA512);

            var time100MD5 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds100, saltLengthMD5);
            });
            var time100SHA1 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds100, saltLengthSHA1);
            });
            var time100SHA256 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds100, saltLengthSHA256);
            });
            var time100SHA384 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds100, saltLengthSHA384);
            });
            var time100SHA512 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds100, saltLengthSHA512);
            });

            Console.WriteLine(" Round 100 000");
            Console.WriteLine(" 100 000 MD5: " + Convert.ToBase64String(Rfc100MD5));
            Console.WriteLine(" 100 000 SHA1: " + Convert.ToBase64String(Rfc100SHA1));
            Console.WriteLine(" 100 000 SHA256: " + Convert.ToBase64String(Rfc100SHA256));
            Console.WriteLine(" 100 000 SHA384: " + Convert.ToBase64String(Rfc100SHA384));
            Console.WriteLine(" 100 000 SHA512: " + Convert.ToBase64String(Rfc100SHA512));
            Console.WriteLine();
            Console.WriteLine(" Iterations <" + numOfRounds100 + ">, Method <MD5>,    Elapsed Time: " + time100MD5);
            Console.WriteLine(" Iterations <" + numOfRounds100 + ">  Metod <SHA1>,    Elapsed Time: " + time100SHA1);
            Console.WriteLine(" Iterations <" + numOfRounds100 + ">, Method <SHA256>, Elapsed Time: " + time100SHA256);
            Console.WriteLine(" Iterations <" + numOfRounds100 + ">, Method <SHA384>, Elapsed Time: " + time100SHA384);
            Console.WriteLine(" Iterations <" + numOfRounds100 + ">, Method <SHA512>, Elapsed Time: " + time100SHA512);
            Console.WriteLine();
            Console.WriteLine();

            // round 5 = 1 000 000
            var Rfc1000MD5 = PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds1000, saltLengthMD5);
            var Rfc1000SHA1 = PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds1000, saltLengthSHA1);
            var Rfc1000SHA256 = PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds1000, saltLengthSHA256);
            var Rfc1000SHA384 = PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds1000, saltLengthSHA384);
            var Rfc1000SHA512 = PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds1000, saltLengthSHA512);

            var time1000MD5 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds1000, saltLengthMD5);
            });
            var time1000SHA1 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds1000, saltLengthSHA1);
            });
            var time1000SHA256 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds1000, saltLengthSHA256);
            });
            var time1000SHA384 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds1000, saltLengthSHA384);
            });
            var time1000SHA512 = MeasureRunTime(() => {
                PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds1000, saltLengthSHA512);
            });

            Console.WriteLine(" Round 1 000 000");
            Console.WriteLine(" 1 000 000 MD5: " + Convert.ToBase64String(Rfc1000MD5));
            Console.WriteLine(" 1 000 000 SHA1: " + Convert.ToBase64String(Rfc1000SHA1));
            Console.WriteLine(" 1 000 000 SHA256: " + Convert.ToBase64String(Rfc1000SHA256));
            Console.WriteLine(" 1 000 000 SHA384: " + Convert.ToBase64String(Rfc1000SHA384));
            Console.WriteLine(" 1 000 000 SHA512: " + Convert.ToBase64String(Rfc1000SHA512));
            Console.WriteLine();
            Console.WriteLine(" Iterations <" + numOfRounds1000 + ">, Method <MD5>,    Elapsed Time: " + time1000MD5);
            Console.WriteLine(" Iterations <" + numOfRounds1000 + ">  Metod <SHA1>,    Elapsed Time: " + time1000SHA1);
            Console.WriteLine(" Iterations <" + numOfRounds1000 + ">, Method <SHA256>, Elapsed Time: " + time1000SHA256);
            Console.WriteLine(" Iterations <" + numOfRounds1000 + ">, Method <SHA384>, Elapsed Time: " + time1000SHA384);
            Console.WriteLine(" Iterations <" + numOfRounds1000 + ">, Method <SHA512>, Elapsed Time: " + time1000SHA512);
            Console.WriteLine();
            Console.WriteLine();
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