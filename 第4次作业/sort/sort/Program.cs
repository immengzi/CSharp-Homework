using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Random rnd = new Random();
        int[] nums = Enumerable.Range(0, 100).Select(i => rnd.Next(0, 1001)).ToArray();

        Console.WriteLine("Generated Numbers:");
        for (int i = 0; i < nums.Length; i++)
        {
            Console.Write(nums[i] + " ");
            if ((i + 1) % 10 == 0)
            {
                Console.WriteLine(); // 每行输出10个数
            }
        }

        var sortedNums = nums.OrderByDescending(n => n).ToArray();

        int sum = sortedNums.Sum();
        double avg = sortedNums.Average();

        Console.WriteLine("\nSum: {0}", sum);
        Console.WriteLine("Average: {0}", avg);
    }
}