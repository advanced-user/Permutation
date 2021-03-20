using System;
using System.Diagnostics;

namespace Permutation
{
    class Program
    {
        static void Main(string[] args)
        {
            int k = 5;
            int n = 5;
            int []arr = new int[n];
                
            for(int i = 0; i < n; i++)
            {
                arr[i] = i + 1;
            }

            var generator = new PermutationGenerator(arr);
            Stopwatch time = new Stopwatch();


            time.Start();
            generator.GeneratePlacements(k);

            for (int i = 0; i < generator.Set.Length; i++)
            {
                Console.Write(generator.Set[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("n = " + generator.Set.Length);
            Console.WriteLine("k = " + k);
            Console.WriteLine("Количество размещений: " + generator.Count);

            time.Stop();
            Console.WriteLine("Минут: " + time.Elapsed.Minutes);
            Console.WriteLine("Секунд: " + time.Elapsed.Seconds);
            Console.WriteLine("Миллисекунд: " + time.Elapsed.Milliseconds);


            time.Start();
            generator.GenerateCombinations(k);
            Console.WriteLine();

            for (int i = 0; i < generator.Set.Length; i++)
            {
                Console.Write(generator.Set[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("n = " + generator.Set.Length);
            Console.WriteLine("k = " + k);
            Console.WriteLine("Количество сочетаний: " + generator.Count);

            time.Stop();
            Console.WriteLine("Минут: " + time.Elapsed.Minutes);
            Console.WriteLine("Секунд: " + time.Elapsed.Seconds);
            Console.WriteLine("Миллисекунд: " + time.Elapsed.Milliseconds);
        }
    }
}