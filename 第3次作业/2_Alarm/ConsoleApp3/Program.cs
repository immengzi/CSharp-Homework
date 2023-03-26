using System;

namespace ClockApp
{
    class Clock
    {
        public event EventHandler Tick;
        public event EventHandler Alarm;

        public void Start(DateTime alarmTime)
        {
            while (true)
            {
                // 等待一秒钟
                System.Threading.Thread.Sleep(1000);

                // 触发 Tick 事件
                OnTick();

                // 在闹钟时间触发 Alarm 事件
                if (DateTime.Now >= alarmTime)
                {
                    OnAlarm();
                    alarmTime = alarmTime.AddDays(1); // 将闹钟时间设置为明天，以便继续触发 Tick 事件
                }
            }
        }

        protected virtual void OnTick()
        {
            Tick?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnAlarm()
        {
            Alarm?.Invoke(this, EventArgs.Empty);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("请输入闹钟时间:时(0-23):分(0-59):秒(0-59）：");
            DateTime alarmTime = DateTime.Parse(Console.ReadLine());

            var clock = new Clock(); //创建一个Clock实例

            clock.Tick += (sender, e) =>
            {
                Console.WriteLine("Tick...");
            };

            clock.Alarm += (sender, e) =>
            {
                Console.WriteLine("Alarm!!!");
            };

            clock.Start(alarmTime);

            Console.ReadLine();
        }
    }
}