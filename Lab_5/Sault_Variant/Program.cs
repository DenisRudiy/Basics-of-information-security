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
            byte[] saltMD5, saltSHA1, saltSHA256, saltSHA384, saltSHA512, passwordInArray;

            // 10 значень із кроком 50'000; перше значення = номер варіанта * 10'000;
            int numOfRounds0 = 220000;
            int numOfRounds1 = 270000;
            int numOfRounds2 = 320000;
            int numOfRounds3 = 370000;
            int numOfRounds4 = 420000;
            int numOfRounds5 = 470000;
            int numOfRounds6 = 520000;
            int numOfRounds7 = 570000;
            int numOfRounds8 = 620000;
            int numOfRounds9 = 670000;

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
                    Console.WriteLine("Wait a bit");
                    Console.WriteLine();
                    // Для хешування-MD5 використовуємо різні числа
                    var Rfc0MD5 = PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds0, saltLengthMD5);
                    var Rfc1MD5 = PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds1, saltLengthMD5);
                    var Rfc2MD5 = PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds2, saltLengthMD5);
                    var Rfc3MD5 = PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds3, saltLengthMD5);
                    var Rfc4MD5 = PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds4, saltLengthMD5);
                    var Rfc5MD5 = PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds5, saltLengthMD5);
                    var Rfc6MD5 = PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds6, saltLengthMD5);
                    var Rfc7MD5 = PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds7, saltLengthMD5);
                    var Rfc8MD5 = PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds8, saltLengthMD5);
                    var Rfc9MD5 = PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds9, saltLengthMD5);

                    // шукаємо час для кожного числа
                    var time0MD5 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds0, saltLengthMD5);
                    });
                    var time1MD5 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds1, saltLengthMD5);
                    });
                    var time2MD5 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds2, saltLengthMD5);
                    });
                    var time3MD5 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds3, saltLengthMD5);
                    });
                    var time4MD5 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds4, saltLengthMD5);
                    });
                    var time5MD5 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds5, saltLengthMD5);
                    });
                    var time6MD5 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds6, saltLengthMD5);
                    });
                    var time7MD5 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds7, saltLengthMD5);
                    });
                    var time8MD5 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds8, saltLengthMD5);
                    });
                    var time9MD5 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltMD5, numOfRounds9, saltLengthMD5);
                    });

                    Console.WriteLine(" " + numOfRounds0 + " MD5: " + Convert.ToBase64String(Rfc0MD5));
                    Console.WriteLine(" " + numOfRounds1 + " MD5: " + Convert.ToBase64String(Rfc1MD5));
                    Console.WriteLine(" " + numOfRounds2 + " MD5: " + Convert.ToBase64String(Rfc2MD5));
                    Console.WriteLine(" " + numOfRounds3 + " MD5: " + Convert.ToBase64String(Rfc3MD5));
                    Console.WriteLine(" " + numOfRounds4 + " MD5: " + Convert.ToBase64String(Rfc4MD5));
                    Console.WriteLine(" " + numOfRounds5 + " MD5: " + Convert.ToBase64String(Rfc5MD5));
                    Console.WriteLine(" " + numOfRounds6 + " MD5: " + Convert.ToBase64String(Rfc6MD5));
                    Console.WriteLine(" " + numOfRounds7 + " MD5: " + Convert.ToBase64String(Rfc7MD5));
                    Console.WriteLine(" " + numOfRounds8 + " MD5: " + Convert.ToBase64String(Rfc8MD5));
                    Console.WriteLine(" " + numOfRounds9 + " MD5: " + Convert.ToBase64String(Rfc9MD5));
                    Console.WriteLine();
                    Console.WriteLine(" Iterations <" + numOfRounds0 + ">, Elapsed Time: " + time0MD5);
                    Console.WriteLine(" Iterations <" + numOfRounds1 + ">, Elapsed Time: " + time1MD5);
                    Console.WriteLine(" Iterations <" + numOfRounds2 + ">, Elapsed Time: " + time2MD5);
                    Console.WriteLine(" Iterations <" + numOfRounds3 + ">, Elapsed Time: " + time3MD5);
                    Console.WriteLine(" Iterations <" + numOfRounds4 + ">, Elapsed Time: " + time4MD5);
                    Console.WriteLine(" Iterations <" + numOfRounds5 + ">, Elapsed Time: " + time5MD5);
                    Console.WriteLine(" Iterations <" + numOfRounds6 + ">, Elapsed Time: " + time6MD5);
                    Console.WriteLine(" Iterations <" + numOfRounds7 + ">, Elapsed Time: " + time7MD5);
                    Console.WriteLine(" Iterations <" + numOfRounds8 + ">, Elapsed Time: " + time8MD5);
                    Console.WriteLine(" Iterations <" + numOfRounds9 + ">, Elapsed Time: " + time9MD5);
                    Console.WriteLine();
                    Console.WriteLine();

                }

                else if (variant == "2")
                {
                    Console.WriteLine("Wait a bit");
                    Console.WriteLine();
                    // Для хешування-SHA1 використовуємо різні числа
                    var Rfc0SHA1 = PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds0, saltLengthSHA1);
                    var Rfc1SHA1 = PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds1, saltLengthSHA1);
                    var Rfc2SHA1 = PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds2, saltLengthSHA1);
                    var Rfc3SHA1 = PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds3, saltLengthSHA1);
                    var Rfc4SHA1 = PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds4, saltLengthSHA1);
                    var Rfc5SHA1 = PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds5, saltLengthSHA1);
                    var Rfc6SHA1 = PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds6, saltLengthSHA1);
                    var Rfc7SHA1 = PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds7, saltLengthSHA1);
                    var Rfc8SHA1 = PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds8, saltLengthSHA1);
                    var Rfc9SHA1 = PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds9, saltLengthSHA1);

                    // шукаємо час для кожного числа
                    var time0SHA1 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds0, saltLengthSHA1);
                    });
                    var time1SHA1 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds1, saltLengthSHA1);
                    });
                    var time2SHA1 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds2, saltLengthSHA1);
                    });
                    var time3SHA1 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds3, saltLengthSHA1);
                    });
                    var time4SHA1 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds4, saltLengthSHA1);
                    });
                    var time5SHA1 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds5, saltLengthSHA1);
                    });
                    var time6SHA1 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds6, saltLengthSHA1);
                    });
                    var time7SHA1 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds7, saltLengthSHA1);
                    });
                    var time8SHA1 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds8, saltLengthSHA1);
                    });
                    var time9SHA1 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA1, numOfRounds9, saltLengthSHA1);
                    });

                    Console.WriteLine(" " + numOfRounds0 + " SHA1: " + Convert.ToBase64String(Rfc0SHA1));
                    Console.WriteLine(" " + numOfRounds1 + " SHA1: " + Convert.ToBase64String(Rfc1SHA1));
                    Console.WriteLine(" " + numOfRounds2 + " SHA1: " + Convert.ToBase64String(Rfc2SHA1));
                    Console.WriteLine(" " + numOfRounds3 + " SHA1: " + Convert.ToBase64String(Rfc3SHA1));
                    Console.WriteLine(" " + numOfRounds4 + " SHA1: " + Convert.ToBase64String(Rfc4SHA1));
                    Console.WriteLine(" " + numOfRounds5 + " SHA1: " + Convert.ToBase64String(Rfc5SHA1));
                    Console.WriteLine(" " + numOfRounds6 + " SHA1: " + Convert.ToBase64String(Rfc6SHA1));
                    Console.WriteLine(" " + numOfRounds7 + " SHA1: " + Convert.ToBase64String(Rfc7SHA1));
                    Console.WriteLine(" " + numOfRounds8 + " SHA1: " + Convert.ToBase64String(Rfc8SHA1));
                    Console.WriteLine(" " + numOfRounds9 + " SHA1: " + Convert.ToBase64String(Rfc9SHA1));
                    Console.WriteLine();
                    Console.WriteLine(" Iterations <" + numOfRounds0 + ">, Elapsed Time: " + time0SHA1);
                    Console.WriteLine(" Iterations <" + numOfRounds1 + ">, Elapsed Time: " + time1SHA1);
                    Console.WriteLine(" Iterations <" + numOfRounds2 + ">, Elapsed Time: " + time2SHA1);
                    Console.WriteLine(" Iterations <" + numOfRounds3 + ">, Elapsed Time: " + time3SHA1);
                    Console.WriteLine(" Iterations <" + numOfRounds4 + ">, Elapsed Time: " + time4SHA1);
                    Console.WriteLine(" Iterations <" + numOfRounds5 + ">, Elapsed Time: " + time5SHA1);
                    Console.WriteLine(" Iterations <" + numOfRounds6 + ">, Elapsed Time: " + time6SHA1);
                    Console.WriteLine(" Iterations <" + numOfRounds7 + ">, Elapsed Time: " + time7SHA1);
                    Console.WriteLine(" Iterations <" + numOfRounds8 + ">, Elapsed Time: " + time8SHA1);
                    Console.WriteLine(" Iterations <" + numOfRounds9 + ">, Elapsed Time: " + time9SHA1);
                    Console.WriteLine();
                }

                else if (variant == "3")
                {
                    Console.WriteLine("Wait a bit");
                    Console.WriteLine();
                    // Для хешування-SHA256 використовуємо різні числа
                    var Rfc0SHA256 = PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds0, saltLengthSHA256);
                    var Rfc1SHA256 = PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds1, saltLengthSHA256);
                    var Rfc2SHA256 = PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds2, saltLengthSHA256);
                    var Rfc3SHA256 = PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds3, saltLengthSHA256);
                    var Rfc4SHA256 = PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds4, saltLengthSHA256);
                    var Rfc5SHA256 = PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds5, saltLengthSHA256);
                    var Rfc6SHA256 = PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds6, saltLengthSHA256);
                    var Rfc7SHA256 = PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds7, saltLengthSHA256);
                    var Rfc8SHA256 = PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds8, saltLengthSHA256);
                    var Rfc9SHA256 = PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds9, saltLengthSHA256);

                    // шукаємо час для кожного числа
                    var time0SHA256 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds0, saltLengthSHA256);
                    });
                    var time1SHA256 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds1, saltLengthSHA256);
                    });
                    var time2SHA256 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds2, saltLengthSHA256);
                    });
                    var time3SHA256 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds3, saltLengthSHA256);
                    });
                    var time4SHA256 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds4, saltLengthSHA256);
                    });
                    var time5SHA256 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds5, saltLengthSHA256);
                    });
                    var time6SHA256 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds6, saltLengthSHA256);
                    });
                    var time7SHA256 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds7, saltLengthSHA256);
                    });
                    var time8SHA256 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds8, saltLengthSHA256);
                    });
                    var time9SHA256 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA256, numOfRounds9, saltLengthSHA256);
                    });

                    
                    Console.WriteLine(" " + numOfRounds0 + " SHA256: " + Convert.ToBase64String(Rfc0SHA256));
                    Console.WriteLine(" " + numOfRounds1 + " SHA256: " + Convert.ToBase64String(Rfc1SHA256));
                    Console.WriteLine(" " + numOfRounds2 + " SHA256: " + Convert.ToBase64String(Rfc2SHA256));
                    Console.WriteLine(" " + numOfRounds3 + " SHA256: " + Convert.ToBase64String(Rfc3SHA256));
                    Console.WriteLine(" " + numOfRounds4 + " SHA256: " + Convert.ToBase64String(Rfc4SHA256));
                    Console.WriteLine(" " + numOfRounds5 + " SHA256: " + Convert.ToBase64String(Rfc5SHA256));
                    Console.WriteLine(" " + numOfRounds6 + " SHA256: " + Convert.ToBase64String(Rfc6SHA256));
                    Console.WriteLine(" " + numOfRounds7 + " SHA256: " + Convert.ToBase64String(Rfc7SHA256));
                    Console.WriteLine(" " + numOfRounds8 + " SHA256: " + Convert.ToBase64String(Rfc8SHA256));
                    Console.WriteLine(" " + numOfRounds9 + " SHA256: " + Convert.ToBase64String(Rfc9SHA256));
                    Console.WriteLine();
                    Console.WriteLine(" Iterations <" + numOfRounds0 + ">, Elapsed Time: " + time0SHA256);
                    Console.WriteLine(" Iterations <" + numOfRounds1 + ">, Elapsed Time: " + time1SHA256);
                    Console.WriteLine(" Iterations <" + numOfRounds2 + ">, Elapsed Time: " + time2SHA256);
                    Console.WriteLine(" Iterations <" + numOfRounds3 + ">, Elapsed Time: " + time3SHA256);
                    Console.WriteLine(" Iterations <" + numOfRounds4 + ">, Elapsed Time: " + time4SHA256);
                    Console.WriteLine(" Iterations <" + numOfRounds5 + ">, Elapsed Time: " + time5SHA256);
                    Console.WriteLine(" Iterations <" + numOfRounds6 + ">, Elapsed Time: " + time6SHA256);
                    Console.WriteLine(" Iterations <" + numOfRounds7 + ">, Elapsed Time: " + time7SHA256);
                    Console.WriteLine(" Iterations <" + numOfRounds8 + ">, Elapsed Time: " + time8SHA256);
                    Console.WriteLine(" Iterations <" + numOfRounds9 + ">, Elapsed Time: " + time9SHA256);
                    Console.WriteLine();
                    Console.WriteLine();

                }

                else if (variant == "4")
                {
                    Console.WriteLine("Wait a bit");
                    Console.WriteLine();
                    // Для хешування-SHA384 використовуємо різні числа
                    var Rfc0SHA384 = PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds0, saltLengthSHA384);
                    var Rfc1SHA384 = PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds1, saltLengthSHA384);
                    var Rfc2SHA384 = PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds2, saltLengthSHA384);
                    var Rfc3SHA384 = PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds3, saltLengthSHA384);
                    var Rfc4SHA384 = PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds4, saltLengthSHA384);
                    var Rfc5SHA384 = PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds5, saltLengthSHA384);
                    var Rfc6SHA384 = PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds6, saltLengthSHA384);
                    var Rfc7SHA384 = PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds7, saltLengthSHA384);
                    var Rfc8SHA384 = PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds8, saltLengthSHA384);
                    var Rfc9SHA384 = PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds9, saltLengthSHA384);

                    // шукаємо час для кожного числа
                    var time0SHA384 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds0, saltLengthSHA384);
                    });
                    var time1SHA384 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds1, saltLengthSHA384);
                    });
                    var time2SHA384 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds2, saltLengthSHA384);
                    });
                    var time3SHA384 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds3, saltLengthSHA384);
                    });
                    var time4SHA384 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds4, saltLengthSHA384);
                    });
                    var time5SHA384 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds5, saltLengthSHA384);
                    });
                    var time6SHA384 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds6, saltLengthSHA384);
                    });
                    var time7SHA384 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds7, saltLengthSHA384);
                    });
                    var time8SHA384 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds8, saltLengthSHA384);
                    });
                    var time9SHA384 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA384, numOfRounds9, saltLengthSHA384);
                    });

                    
                    Console.WriteLine(" " + numOfRounds0 + " SHA384: " + Convert.ToBase64String(Rfc0SHA384));
                    Console.WriteLine(" " + numOfRounds1 + " SHA384: " + Convert.ToBase64String(Rfc1SHA384));
                    Console.WriteLine(" " + numOfRounds2 + " SHA384: " + Convert.ToBase64String(Rfc2SHA384));
                    Console.WriteLine(" " + numOfRounds3 + " SHA384: " + Convert.ToBase64String(Rfc3SHA384));
                    Console.WriteLine(" " + numOfRounds4 + " SHA384: " + Convert.ToBase64String(Rfc4SHA384));
                    Console.WriteLine(" " + numOfRounds5 + " SHA384: " + Convert.ToBase64String(Rfc5SHA384));
                    Console.WriteLine(" " + numOfRounds6 + " SHA384: " + Convert.ToBase64String(Rfc6SHA384));
                    Console.WriteLine(" " + numOfRounds7 + " SHA384: " + Convert.ToBase64String(Rfc7SHA384));
                    Console.WriteLine(" " + numOfRounds8 + " SHA384: " + Convert.ToBase64String(Rfc8SHA384));
                    Console.WriteLine(" " + numOfRounds9 + " SHA384: " + Convert.ToBase64String(Rfc9SHA384));
                    Console.WriteLine();
                    Console.WriteLine(" Iterations <" + numOfRounds0 + ">, Elapsed Time: " + time0SHA384);
                    Console.WriteLine(" Iterations <" + numOfRounds1 + ">, Elapsed Time: " + time1SHA384);
                    Console.WriteLine(" Iterations <" + numOfRounds2 + ">, Elapsed Time: " + time2SHA384);
                    Console.WriteLine(" Iterations <" + numOfRounds3 + ">, Elapsed Time: " + time3SHA384);
                    Console.WriteLine(" Iterations <" + numOfRounds4 + ">, Elapsed Time: " + time4SHA384);
                    Console.WriteLine(" Iterations <" + numOfRounds5 + ">, Elapsed Time: " + time5SHA384);
                    Console.WriteLine(" Iterations <" + numOfRounds6 + ">, Elapsed Time: " + time6SHA384);
                    Console.WriteLine(" Iterations <" + numOfRounds7 + ">, Elapsed Time: " + time7SHA384);
                    Console.WriteLine(" Iterations <" + numOfRounds8 + ">, Elapsed Time: " + time8SHA384);
                    Console.WriteLine(" Iterations <" + numOfRounds9 + ">, Elapsed Time: " + time9SHA384);
                    Console.WriteLine();
                    Console.WriteLine();

                }

                else if (variant == "5")
                {
                    Console.WriteLine("Wait a bit");
                    Console.WriteLine();
                    // Для хешування-SHA512 використовуємо різні числа
                    var Rfc0SHA512 = PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds0, saltLengthSHA512);
                    var Rfc1SHA512 = PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds1, saltLengthSHA512);
                    var Rfc2SHA512 = PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds2, saltLengthSHA512);
                    var Rfc3SHA512 = PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds3, saltLengthSHA512);
                    var Rfc4SHA512 = PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds4, saltLengthSHA512);
                    var Rfc5SHA512 = PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds5, saltLengthSHA512);
                    var Rfc6SHA512 = PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds6, saltLengthSHA512);
                    var Rfc7SHA512 = PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds7, saltLengthSHA512);
                    var Rfc8SHA512 = PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds8, saltLengthSHA512);
                    var Rfc9SHA512 = PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds9, saltLengthSHA512);

                    // шукаємо час для кожного числа
                    var time0SHA512 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds0, saltLengthSHA512);
                    });
                    var time1SHA512 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds1, saltLengthSHA512);
                    });
                    var time2SHA512 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds2, saltLengthSHA512);
                    });
                    var time3SHA512 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds3, saltLengthSHA512);
                    });
                    var time4SHA512 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds4, saltLengthSHA512);
                    });
                    var time5SHA512 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds5, saltLengthSHA512);
                    });
                    var time6SHA512 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds6, saltLengthSHA512);
                    });
                    var time7SHA512 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds7, saltLengthSHA512);
                    });
                    var time8SHA512 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds8, saltLengthSHA512);
                    });
                    var time9SHA512 = MeasureRunTime(() => {
                        PBKDF2.HashPassword(passwordInArray, saltSHA512, numOfRounds9, saltLengthSHA512);
                    });

                    Console.WriteLine(" " + numOfRounds0 + " SHA512: " + Convert.ToBase64String(Rfc0SHA512));
                    Console.WriteLine(" " + numOfRounds1 + " SHA512: " + Convert.ToBase64String(Rfc1SHA512));
                    Console.WriteLine(" " + numOfRounds2 + " SHA512: " + Convert.ToBase64String(Rfc2SHA512));
                    Console.WriteLine(" " + numOfRounds3 + " SHA512: " + Convert.ToBase64String(Rfc3SHA512));
                    Console.WriteLine(" " + numOfRounds4 + " SHA512: " + Convert.ToBase64String(Rfc4SHA512));
                    Console.WriteLine(" " + numOfRounds5 + " SHA512: " + Convert.ToBase64String(Rfc5SHA512));
                    Console.WriteLine(" " + numOfRounds6 + " SHA512: " + Convert.ToBase64String(Rfc6SHA512));
                    Console.WriteLine(" " + numOfRounds7 + " SHA512: " + Convert.ToBase64String(Rfc7SHA512));
                    Console.WriteLine(" " + numOfRounds8 + " SHA512: " + Convert.ToBase64String(Rfc8SHA512));
                    Console.WriteLine(" " + numOfRounds9 + " SHA512: " + Convert.ToBase64String(Rfc9SHA512));
                    Console.WriteLine();
                    Console.WriteLine(" Iterations <" + numOfRounds0 + ">, Elapsed Time: " + time0SHA512);
                    Console.WriteLine(" Iterations <" + numOfRounds1 + ">, Elapsed Time: " + time1SHA512);
                    Console.WriteLine(" Iterations <" + numOfRounds2 + ">, Elapsed Time: " + time2SHA512);
                    Console.WriteLine(" Iterations <" + numOfRounds3 + ">, Elapsed Time: " + time3SHA512);
                    Console.WriteLine(" Iterations <" + numOfRounds4 + ">, Elapsed Time: " + time4SHA512);
                    Console.WriteLine(" Iterations <" + numOfRounds5 + ">, Elapsed Time: " + time5SHA512);
                    Console.WriteLine(" Iterations <" + numOfRounds6 + ">, Elapsed Time: " + time6SHA512);
                    Console.WriteLine(" Iterations <" + numOfRounds7 + ">, Elapsed Time: " + time7SHA512);
                    Console.WriteLine(" Iterations <" + numOfRounds8 + ">, Elapsed Time: " + time8SHA512);
                    Console.WriteLine(" Iterations <" + numOfRounds9 + ">, Elapsed Time: " + time9SHA512);
                    Console.WriteLine();
                    Console.WriteLine();

                }


            } while (variant != "0");

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