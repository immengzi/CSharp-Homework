using System;
using System.Collections.Generic;

namespace PrimeFactors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入一个正整数：");
            int num = Convert.ToInt32(Console.ReadLine());
            List<int> primeFactors = GetPrimeFactors(num);
            Console.WriteLine( num + "的素数因子为：");
            foreach (int factor in primeFactors)
            {
                Console.Write(factor + " ");
            }
        }

        static List<int> GetPrimeFactors(int num)
        {
            List<int> primeFactors = new List<int>();
            for (int i = 2; i <= num; i++)
            {
                while (num % i == 0)
                {
                    primeFactors.Add(i);
                    num /= i;
                }
            }
            return primeFactors;
        }
    }
}