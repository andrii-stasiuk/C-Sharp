using System;
using System.Linq;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            int length;
            int[] sequence; 
            Random rnd = new Random();
            int shuffleRate = 0;

            Console.Write("Enter the length of the array: ");
            length = int.Parse(Console.ReadLine());
            Console.WriteLine();

            // Generating an array of numbers in ascending order
            sequence = Enumerable.Range(1, length).ToArray();
            
            Console.Write("Your generated array is: \t\t");
            foreach (var item in sequence)
                Console.Write(item + " ");
            Console.WriteLine();

            // Shuffling an array in the first way (LINQ)
            sequence = sequence.OrderBy(x => rnd.Next()).ToArray();

            Console.Write("Shuffled the first way: \t\t");
            foreach (var item in sequence)
                Console.Write(item + " ");
            Console.WriteLine();

            // Shuffle rate calculation for the first case
            for (int i = 0; i < length; i++)
                if (sequence[i] == i + 1)
                    shuffleRate++;
            Console.Write("Shuffle rate is " + (100 - ((float)shuffleRate / length) * 100) + "%, ");
            Console.WriteLine(shuffleRate + " identical\n");

            // Returning the array to its original state (there are two ways)
            Array.Sort(sequence);
            // sequence = sequence.OrderBy(x => x).ToArray();

            Console.Write("Returned to the original: \t\t");
            foreach (var item in sequence)
                Console.Write(item + " ");
            Console.WriteLine();

            // Shuffling an array in the second way
            for (int i = length - 1; i >= 0; i--)
            {
                int r = rnd.Next(i);
                int tmp = sequence[i];
                sequence[i] = sequence[r];
                sequence[r] = tmp;
            }

            Console.Write("Shuffled the second way: \t\t");
            foreach (var item in sequence)
                Console.Write(item + " ");
            Console.WriteLine();

            // Shuffle rate calculation for the second case
            shuffleRate = 0;
            for (int i = 0; i < length; i++)
                if (sequence[i] == i + 1)
                    shuffleRate++;
            Console.Write("Shuffle rate is " + (100 - ((float)shuffleRate / length) * 100) + "%, ");
            Console.WriteLine(shuffleRate + " identical");

            // P.S. For small arrays the first method shows worse results than the second
        }
    }
}
