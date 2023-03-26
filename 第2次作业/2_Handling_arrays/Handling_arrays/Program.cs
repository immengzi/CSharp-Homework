using System;

class Program
{
    static void Main()
    {
        int[] arr = { 1, 2, 3, 4, 5, 6 };

        int max = arr[0];
        int min = arr[0];
        int sum = 0;

        foreach (int n in arr)
        {
            if (n > max)
                max = n;

            if (n < min)
                min = n;

            sum += n;
        }

        double average = (double)sum / arr.Length;

        Console.WriteLine("最大值为：" + max);
        Console.WriteLine("最小值为：" + min);
        Console.WriteLine("平均值为：" + average);
        Console.WriteLine("所有数组元素的和为：" + sum);
    }
}