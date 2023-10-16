using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Алгоритм Хавман");
            int n =  int.Parse(Console.ReadLine());
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = new Random().Next(1,99);
            }
            Console.WriteLine($"{Huffman(arr)}");
            
        }

        public static int Huffman(int [] f)
        {
            int[] H = new int[f.Length];
            int n = f.Length;
            for (int i = 1; i < n; i++)
            {
                Insert(H, i, f[i]);
            }
            int k =0, j =0;
            for (int i = 0+1; i < 2*n-1; i++)
            {
                k = i;
                j = i + (2 * n - 1);
                k = Math.Min(i,f[i]);
                j = Math.Min(i,f[i]);

            }
            
            f[0] = f[k] + f[j];
            Insert(H, 0, f[0]);
            return f[0];
        }

        private static void Insert(int[] h, int i, int v)
        {
            h[i] = v;
        }
    }
}
