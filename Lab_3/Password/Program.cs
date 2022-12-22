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
            String correctHash = "po1MVkAE7IjUUwu61XxgNg==";
            String attemptHash = "";

            int first = 0; int second = 0; int third = 0; int fourth = 0; int fifth = 0; int sixth = 0; int seventh = 0; int eighth = 0; int cracks = 0;

            string[] array = new string[10];
            array[0] = "0"; array[1] = "1"; array[2] = "2"; array[3] = "3"; array[4] = "4"; array[5] = "5"; array[6] = "6"; array[7] = "7"; array[8] = "8"; array[9] = "9";

            while (!attemptHash.Equals(correctHash))
            {
                if (first == array.Length)
                {
                    second++;
                    first = 0;
                }
                if (second == array.Length)
                {
                    third++;
                    second = 0;
                }
                if (third == array.Length)
                {
                    fourth++;
                    third = 0;
                }
                if (fourth == array.Length)
                {
                    fifth++;
                    fourth = 0;
                }
                if (fifth == array.Length)
                {
                    sixth++;
                    fifth = 0;
                }
                if (sixth == array.Length)
                {
                    seventh++;
                    sixth = 0;
                }
                if (seventh == array.Length)
                {
                    eighth++;
                    seventh = 0;
                }
                if (eighth == array.Length)
                {
                    break; //кінець
                }

                string attempt = array[eighth] + array[seventh] + array[sixth] + array[fifth]
                    + array[fourth] + array[third] + array[second] + array[first];

                byte[] attemptInArray = Encoding.Unicode.GetBytes(attempt);
                attemptInArray = ComputeHashMd5(attemptInArray);
                attemptHash = Convert.ToBase64String(attemptInArray);

                Console.WriteLine(attempt + "    -    " + attemptHash);
                first++;
                cracks++;
            }
            Console.WriteLine("Attempts to crack: " + cracks);

            // пароль -  20192020


            static byte[] ComputeHashMd5(byte[] messageInArray)
            {
                using (var md5 = MD5.Create())
                {
                    return md5.ComputeHash(messageInArray);
                }
            }
        }
    }
}