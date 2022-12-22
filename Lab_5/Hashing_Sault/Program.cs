using System.Security.Cryptography;
using System.Text;

class Salted_Hash
{
    static void Main()
    {
        Console.WriteLine("Enter your password:");
        string password = Console.ReadLine();
        byte[] salt = SaltedHash.GenerateSalt();

        Console.WriteLine("\n" + " - Password: " + password + "\n");
        Console.WriteLine(" - Salt: " + Convert.ToBase64String(salt) + "\n");
        var hashedPassword = SaltedHash.HashingviaSalt(Encoding.UTF8.GetBytes(password), salt);
        Console.WriteLine(" - Hashed Password: " + Convert.ToBase64String(hashedPassword) + "\n");
        Console.ReadLine();
    }
}

public class SaltedHash
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

    private static byte[] Combine(byte[] pas, byte[] salt)
    {
        var unit = new byte[pas.Length + salt.Length];
        Buffer.BlockCopy(pas, 0, unit, 0, pas.Length);
        Buffer.BlockCopy(salt, 0, unit, pas.Length, salt.Length);

        return unit;
    }

    public static byte[] HashingviaSalt(byte[] toBeHashed, byte[] salting)
    {
        using (var sha512 = SHA512.Create())
        {
            return sha512.ComputeHash(Combine(toBeHashed, salting));
        }
    }
}
