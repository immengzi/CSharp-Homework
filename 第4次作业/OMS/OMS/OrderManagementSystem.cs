namespace OMS;

class OrderManagementSystem
{
    private OrderService orderService;
    public OrderManagementSystem()
    {
        orderService = new OrderService();
        while (true)
        {
            Console.WriteLine("【订单管理系统】\n" +
                              "1.添加订单\n" +
                              "2.删除订单\n" +
                              "3.修改订单\n" +
                              "4.查询订单\n" +
                              "5.查看全部订单"
            );
            Console.Write("请输入操作序号：");
            string input = Console.ReadLine();
            Console.WriteLine("");
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("【提示】输入有误，请重新输入。\n");
                continue;
            }

            if (!int.TryParse(input, out int op))
            {
                Console.WriteLine("【提示】输入有误，请重新输入。\n");
                continue;
            }

            switch (op)
            {
                case 1: orderService.AddOrder(); break;
                case 2: orderService.DeleteOrder(); break;
                case 3: orderService.ModifyOrder(); break;
                case 4: orderService.QueryOrder(); break;
                case 5: orderService.ShowAllOrders(); break;
                default: Console.WriteLine("【提示】输入有误，请重新输入。\n"); break;
            }
        }
    }

}