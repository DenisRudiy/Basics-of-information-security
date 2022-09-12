using System;
public class Program_RND
{
    public static void Main()
    {
        Random rnd = new Random(1234);
        for (int ctr = 0; ctr < 10; ctr++)
        {
            Console.Write("{0,3} ", rnd.Next(-10, 11));
        }
        Console.WriteLine();
    }
}
