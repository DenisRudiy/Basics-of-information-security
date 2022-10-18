using System;
public class Program_RND
{
    public static void Main()
    {
        int seed;
        seed = Convert.ToInt32(Console.ReadLine());
        Random rnd = new Random(seed);

        for (int i = 0; i < 10; i++)
        {
            Console.Write(rnd.Next(1, 20));
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
