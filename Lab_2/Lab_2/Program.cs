using System;
using System.Text;
using System.Security.Cryptography;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Options:");
            Console.WriteLine("1 - encode file");
            Console.WriteLine("2 - decode file");
            Console.WriteLine("");
            Console.Write("Your option:  ");

            string option = Console.ReadLine();
            string path = @"C:\C#\Lab_2\Lab_2\";

            if (option == "1")                                                       // encryption
            {
                byte[] data = File.ReadAllBytes(path + "file.txt").ToArray();        

                int length = data.Length;                                           
                byte key = Convert.ToByte(length);                                   

                byte[] encrLength = new byte[length];                                

                for (int i = 0; i < length; i++)                                     
                {
                    encrLength[i] = (byte)(data[i] ^ length);
                }
                File.WriteAllBytes(path + "file.dat", encrLength);
            }
            else if (option == "2")                                                  // decription
            {
                byte[] data = File.ReadAllBytes(path + "file.dat").ToArray();

                int length = data.Length;
                byte key = Convert.ToByte(length);

                byte[] encrLength = new byte[length];
                for (int i = 0; i < length; i++)
                {
                    encrLength[i] = (byte)(data[i] ^ length);
                }
                File.WriteAllBytes(path + "file.txt", encrLength);

            }
        }
    }
}

