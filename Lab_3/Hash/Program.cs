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
            Console.Write("Enter message:  ");
            string message = Console.ReadLine();
            Console.WriteLine("");

            byte[] messageInArray = Encoding.Unicode.GetBytes(message);

            var md5ForStr = ComputeHashMd5(messageInArray);
            var SHA1ForStr = ComputeHashSHA1(Encoding.Unicode.GetBytes(message));
            var SHA256ForStr = ComputeHashSHA256(messageInArray);
            var SHA384ForStr = ComputeHashSHA384(messageInArray);
            var SHA512ForStr = ComputeHashSHA512(messageInArray);

            Console.WriteLine($"Hash MD5:{Convert.ToBase64String(md5ForStr)}");
            Console.WriteLine($"Hash SHA1:{Convert.ToBase64String(SHA1ForStr)}");
            Console.WriteLine($"Hash SHA256:{Convert.ToBase64String(SHA256ForStr)}");
            Console.WriteLine($"Hash SHA384:{Convert.ToBase64String(SHA384ForStr)}");
            Console.WriteLine($"Hash SHA512:{Convert.ToBase64String(SHA512ForStr)}");
            Console.WriteLine("");




            static byte[] ComputeHashMd5(byte[] messageInArray)
            {
                using (var md5 = MD5.Create())
                {
                    return md5.ComputeHash(messageInArray);
                }
            }
            static byte[] ComputeHashSHA1(byte[] messageInArray)
            {
                using (var sha1 = SHA1.Create())
                {
                    return sha1.ComputeHash(messageInArray);
                }
            }
            static byte[] ComputeHashSHA256(byte[] messageInArray)
            {
                using (var sha256 = SHA256.Create())
                {
                    return sha256.ComputeHash(messageInArray);
                }
            }
            static byte[] ComputeHashSHA384(byte[] messageInArray)
            {
                using (var sha384 = SHA384.Create())
                {
                    return sha384.ComputeHash(messageInArray);
                }
            }
            static byte[] ComputeHashSHA512(byte[] messageInArray)
            {
                using (var sha512 = SHA512.Create())
                {
                    return sha512.ComputeHash(messageInArray);
                }
            }
        }
    }
}