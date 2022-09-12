using System.Security.Cryptography;


public class Random_Crypto
{
    public static byte[] GenerateRandomNumber()
    {
        using (var randomNumberGenerator = RandomNumberGenerator.Create())
        {
            int bit = 13; //bit value -> to see the difference
            var randomNumber = new byte[bit];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
    public static void Main()
    {
        int c = 1;
        for (int i = 0; i < 13; i++)
        {
            var rnd = GenerateRandomNumber();
            Console.WriteLine(c + ".");
            Console.WriteLine(Convert.ToBase64String(rnd));
            c++;
        }
        Console.ReadLine();
    }
}
