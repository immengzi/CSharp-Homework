using System;

namespace GenericListDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericList<int> list = new GenericList<int>();

            // 从控制台输入元素并添加到链表中
            Console.WriteLine("请输入整数元素，每两个元素之间有一个空格，直到换行/回车为止：");
            string input = Console.ReadLine();
            string[] inputArr = input.Split(' ');
            foreach (string str in inputArr)
            {
                if (str != "")
                {
                    int num = int.Parse(str);
                    list.Add(num);
                }
            }

            // 使用ForEach方法打印链表元素
            Console.WriteLine("链表元素：");
            list.ForEach(x => Console.Write(x + " "));
            Console.WriteLine();

            // 使用ForEach方法求最大值
            int max = 0;
            list.ForEach(x => { if (x > max) max = x; });
            Console.WriteLine("最大值：" + max);

            // 使用ForEach方法求最小值
            int min = int.MaxValue;
            list.ForEach(x => { if (x < min) min = x; });
            Console.WriteLine("最小值：" + min);

            // 使用ForEach方法求和
            int sum = 0;
            list.ForEach(x => sum += x);
            Console.WriteLine("总和：" + sum);
        }
    }

    class GenericList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public GenericList()
        {
            tail = head = null;
        }

        public Node<T> Head
        {
            get => head;
        }

        public void Add(T t)
        {
            Node<T> n = new Node<T>(t);

            if (tail == null)
            {
                head = tail = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
        }

        public void ForEach(Action<T> action)
        {
            Node<T> current = head;
            while (current != null)
            {
                action(current.Data);
                current = current.Next;
            }
        }
    }

    class Node<T>
    {
        private T data;
        private Node<T> next;

        public Node(T t)
        {
            data = t;
            next = null;
        }

        public T Data
        {
            get => data;
            set => data = value;
        }

        public Node<T> Next
        {
            get => next;
            set => next = value;
        }
    }
}