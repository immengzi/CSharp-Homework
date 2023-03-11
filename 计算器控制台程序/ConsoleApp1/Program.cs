using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("请输入第一个数字：");
            double num1 = Convert.ToDouble(Console.ReadLine());

            Console.Write("请输入第二个数字：");
            double num2 = Convert.ToDouble(Console.ReadLine());

            Console.Write("请输入运算符（+、-、*、/）：");
            char op = Convert.ToChar(Console.ReadLine());

            double result = 0;
            switch (op)
            {
                case '+':
                    result = num1 + num2;
                    break;
                case '-':
                    result = num1 - num2;
                    break;
                case '*':
                    result = num1 * num2;
                    break;
                case '/':
                    if (num2 == 0)
                    {
                        Console.WriteLine("错误：除数不能为0！");
                        return;
                    }
                    result = num1 / num2;
                    break;
                default:
                    Console.WriteLine("错误：无效的运算符！");
                    return;
            }

            Console.WriteLine("计算结果为：" + result);
        }
    }
}
