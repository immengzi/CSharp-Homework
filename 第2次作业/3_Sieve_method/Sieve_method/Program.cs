using System;

namespace PrimeSieve
{
    class Program
    {
        static void Main(string[] args)
        {
            const int N = 100;
            bool[] isPrime = new bool[N + 1];
            for (int i = 2; i <= N; i++)
                isPrime[i] = true;

            for (int i = 2; i <= N; i++)
            {
                if (isPrime[i])
                {
                    for (int j = 2 * i; j <= N; j += i)
                        isPrime[j] = false;
                }
            }

            Console.WriteLine("2~100之间的素数为:");
            for (int i = 2; i <= N; i++)
                if (isPrime[i]) Console.Write(i + " ");
        }
    }
}