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

// 测试程序
class Program
{
    static void Main(string[] args)
    {
        Rectangle r = new Rectangle { Width = 3, Height = 4 };
        Console.WriteLine("长方形面积：" + r.Area);
        Console.WriteLine("长方形是否合法：" + r.IsValid());

        Square s = new Square { Side = 5 };
        Console.WriteLine("正方形面积：" + s.Area);
        Console.WriteLine("正方形是否合法：" + s.IsValid());

        Triangle t = new Triangle { SideA = 3, SideB = 4, SideC = 7 };
        Console.WriteLine("三角形面积：" + t.Area);
        Console.WriteLine("三角形是否合法：" + t.IsValid());
    }
}
