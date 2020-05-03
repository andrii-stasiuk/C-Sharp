using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int k = 20; // how much lines
            for (int i = 1; i <= k; i++) {
                for (int j = i; j < k; j++)
                    Console.Write(" ");
                for (int j = 1; j < i*2; j++)
                    Console.Write("*");
                Console.WriteLine();
            }
        }
    }
}
