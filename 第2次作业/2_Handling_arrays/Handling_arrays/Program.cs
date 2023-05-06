using System;

class Program
{
    static void Main()
    {
        List<int> arr = new List<int>();
        Console.WriteLine("请输入整数（输入非整数或空行结束输入）：");

        while (true)
        {
            string input = Console.ReadLine();

            // 如果输入为空行，则结束输入
            if (string.IsNullOrEmpty(input))
            {
                if (arr.Count == 0)
                {
                    Console.WriteLine("您没有输入任何整数，请重新输入：");
                    continue;
                }
                else
                {
                    break;
                }
            }

            int num;
            // 如果输入不是整数，则提示用户重新输入
            if (!int.TryParse(input, out num))
            {
                Console.WriteLine("请输入整数：");
                continue;
            }

            arr.Add(num); // 将输入的整数添加到动态数组中
        }

        int max = int.MinValue; // 将最大值初始化为int类型的最小值
        int min = int.MaxValue; // 将最小值初始化为int类型的最大值
        int sum = 0;

        foreach (int num in arr)
        {
            if (num > max)
                max = num;

            if (num < min)
                min = num;

            sum += num;
        }

        double average = (double)sum / arr.Count;

        Console.WriteLine("最大值为：" + max);
        Console.WriteLine("最小值为：" + min);
        Console.WriteLine("平均值为：" + average);
        Console.WriteLine("所有数组元素的和为：" + sum);
    }
}