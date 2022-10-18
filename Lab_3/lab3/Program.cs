using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Intrinsics.Arm;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            // довжина ключів у байтах;
            int amountSHA1 = 20;       // 20 байтів
            int amountSHA256 = 32;     // 32 байтів
            int amountSHA512 = 64;     // 64 байтів

            // ініціалізація паролю та логіну
            string username;
            string password;
            string usernameCheck;
            string passwordCheck;

            // ініціалізація довжини солі
            byte[] keySHA1, keySHA256, keySHA512, passwordInArray;


            Console.Write("Enter your username:  ");
            username = Console.ReadLine();                          // отримання логіну
            Console.Write("Enter your password:  ");
            password = Console.ReadLine();                          // отримання паролю
            Console.WriteLine();
            Console.WriteLine();
            passwordInArray = Encoding.Unicode.GetBytes(password);  // кладемо пароль у множину

            // створюємо ключі
            keySHA1 = cryptoKey(amountSHA1);
            keySHA256 = cryptoKey(amountSHA256);
            keySHA512 = cryptoKey(amountSHA512);

            // Хешування
            var HmacSHA1 = ComputeHmacSHA1(passwordInArray, keySHA1);
            var HmacSHA256 = ComputeHmacSHA256(passwordInArray, keySHA256);
            var HmacSHA512 = ComputeHmacSHA512(passwordInArray, keySHA512);

            Console.WriteLine("SHA1:    " + Convert.ToBase64String(HmacSHA1));                                                                      
            Console.WriteLine("SHA256:  " + Convert.ToBase64String(HmacSHA256));
            Console.WriteLine("SHA512:  " + Convert.ToBase64String(HmacSHA512));
            Console.WriteLine();


            // Аутентифікація користувача
            Console.Write("Enter your username ->  ");
            usernameCheck = Console.ReadLine();               // отримання логіну
            Console.Write("Enter your password ->  ");
            passwordCheck = Console.ReadLine();               // отримання паролю
            Console.WriteLine();
            Console.WriteLine();

            var Hmac2SHA1 = ComputeHmacSHA1(passwordInArray, keySHA1);
            var Hmac2SHA256 = ComputeHmacSHA256(passwordInArray, keySHA256);
            var Hmac2SHA512 = ComputeHmacSHA512(passwordInArray, keySHA512);


            Console.WriteLine("SHA1: " + Convert.ToBase64String(Hmac2SHA1));
            Console.WriteLine("SHA256: " + Convert.ToBase64String(Hmac2SHA256));
            Console.WriteLine("SHA512:  " + Convert.ToBase64String(Hmac2SHA512));
            Console.WriteLine();


            // перевірка
            Console.Write("HMAC SHA1 check:   ");
            CheckHash(HmacSHA1, Hmac2SHA1);
            Console.Write("HMAC SHA256 check: ");
            CheckHash(HmacSHA256, Hmac2SHA256);
            Console.Write("HMAC SHA512 check: ");
            CheckHash(HmacSHA512, Hmac2SHA512);
            Console.WriteLine("");
            Console.WriteLine("");

            static int CheckHash(byte[] hash1, byte[] hash2)
            {
                if (Convert.ToBase64String(hash1) == Convert.ToBase64String(hash2))
                {
                    Console.WriteLine("Hashes of message are accurate");
                }
                else
                {
                    Console.WriteLine("Hashes aren`t accurate. Message is corrupted");
                }
                return 1;
            }


            static byte[] cryptoKey(int amount)
            {
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    var Key = new byte[amount];
                    rng.GetBytes(Key);
                    return Key;
                }
            }


            static byte[] ComputeHmacSHA1(byte[] toBeHashed, byte[] key)
            {
                using (var hmac = new HMACSHA1(key))
                {
                    return hmac.ComputeHash(toBeHashed);
                }
            }

            static byte[] ComputeHmacSHA256(byte[] toBeHashed, byte[] key)
            {
                using (var hmac = new HMACSHA256(key))
                {
                    return hmac.ComputeHash(toBeHashed);
                }
            }

            static byte[] ComputeHmacSHA512(byte[] toBeHashed, byte[] key)
            {
                using (var hmac = new HMACSHA512(key))
                {
                    return hmac.ComputeHash(toBeHashed);
                }
            }


        }
    }
}