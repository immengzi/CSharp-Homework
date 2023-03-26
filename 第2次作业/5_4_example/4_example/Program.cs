using System;

// 定义形状接口
public interface IShape
{
    double Area { get; }  // 计算面积
    bool IsValid();      // 判断形状是否合法
}

// 长方形类
public class Rectangle : IShape
{
    public double Width { get; set; }   // 宽度
    public double Height { get; set; }  // 高度

    // 实现计算面积方法
    public double Area
    {
        get { return Width * Height; }
    }

    // 实现判断形状是否合法方法
    public bool IsValid()
    {
        return Width > 0 && Height > 0;
    }
}

// 正方形类，继承自长方形类
public class Square : Rectangle
{
    public double Side
    {
        get { return Width; }
        set { Width = value; Height = value; }
    }
}

// 三角形类
public class Triangle : IShape
{
    public double SideA { get; set; }  // 边长 A
    public double SideB { get; set; }  // 边长 B
    public double SideC { get; set; }  // 边长 C

    // 实现计算面积方法
    public double Area
    {
        get
        {
            // 使用海伦公式计算三角形面积
            double p = (SideA + SideB + SideC) / 2;
            return Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC));
        }
    }

    // 实现判断形状是否合法方法
    public bool IsValid()
    {
        return SideA > 0 && SideB > 0 && SideC > 0 &&
               SideA + SideB > SideC && SideA + SideC > SideB && SideB + SideC > SideA;
    }
}

// 简单工厂类
public static class ShapeFactory
{
    // 随机创建一个形状对象
    public static IShape CreateShape()
    {
        Random rand = new Random();
        switch (rand.Next(3))
        {
            case 0: return new Rectangle { Width = rand.Next(1, 10), Height = rand.Next(1, 10) };
            case 1: return new Square { Side = rand.Next(1, 10) };
            case 2: return new Triangle { SideA = rand.Next(1, 10), SideB = rand.Next(1, 10), SideC = rand.Next(1, 10) };
            default: return null;
        }
    }
}

// 测试程序
class Program
{
    static void Main(string[] args)
    {
        double sumArea = 0;
        for (int i = 0; i < 10; i++)
        {
            IShape shape = ShapeFactory.CreateShape();
            if (shape != null && shape.IsValid())
            {
                sumArea += shape.Area;
            }
        }

        Console.WriteLine("10个随机形状对象的面积之和：" + sumArea);
    }
}
