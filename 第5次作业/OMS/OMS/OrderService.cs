using System.Xml.Serialization;

namespace OMS
{
    internal class OrderService
    {
        private List<Order> orders;
        public OrderService()
        {
            orders = new List<Order>();
            
        }
        // 排序比较器
        public delegate int OrderComparer(Order order1, Order order2);
        public void SortOrders(OrderComparer comparer)
        {
            orders.Sort((order1, order2) => comparer(order1, order2));
        }
        public int CompareByOrderNumber(Order order1, Order order2)
        {
            return order1.OrderNumber.CompareTo(order2.OrderNumber);
        }

        public int CompareByTotalAmount(Order order1, Order order2)
        {
            return order2.TotalAmount.CompareTo(order1.TotalAmount);
        }

        public void AddOrder()
        {
            Console.WriteLine("请输入订单号:");
            string? orderNumber = Console.ReadLine();
            Console.WriteLine("请输入客户名:");
            string? customerName = Console.ReadLine();
            DateTime orderDate = DateTime.Now;
            Console.WriteLine("请输入订单明细~");
            List<OrderDetails> orderItems = new List<OrderDetails>();
            while (true)
            {
                Console.WriteLine("请输入商品名:");
                string productName = Console.ReadLine();
                Console.WriteLine("请输入商品数量:");
                int quantity = int.Parse(Console.ReadLine());
                Console.WriteLine("请输入商品单价:");
                decimal unitPrice = decimal.Parse(Console.ReadLine());
                OrderDetails orderDetail = new OrderDetails()
                {
                    ProductName = productName,
                    Quantity = quantity,
                    UnitPrice = unitPrice
                };

                // 检查是否已存在相同的订单明细
                if (orderItems.Contains(orderDetail))
                {
                    Console.WriteLine("【提示】订单明细已存在，无法重复添加。\n");
                }
                else
                {
                    orderItems.Add(orderDetail);
                }

                Console.WriteLine("是否继续添加订单明细？(y/n)");
                if (Console.ReadLine() == "n")
                    break;
            }

            // 创建订单对象
            Order order = new Order()
            {
                OrderNumber = orderNumber,
                CustomerName = customerName,
                OrderDate = orderDate,
                OrderItems = orderItems
            };

            // 检查是否已存在相同的订单
            if (orders.Contains(order))
            {
                Console.WriteLine("【提示】订单已存在，无法重复添加。");
            }
            else
            {
                orders.Add(order);
                Console.WriteLine("订单已成功添加。\n");
                SortOrders(CompareByOrderNumber);
            }
        }

        public void DeleteOrder()
        {
            Console.WriteLine("请输入要删除的订单号：");
            string orderNumber = Console.ReadLine();
            Order orderToDelete = orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
            if (orderToDelete != null)
            {
                orders.Remove(orderToDelete);
                Console.WriteLine($"订单 {orderNumber} 已成功删除。\n");
                SortOrders(CompareByOrderNumber);
            }
            else
            {
                Console.WriteLine($"【提示】找不到订单号为 {orderNumber} 的订单。\n");
            }
        }

        public void ModifyOrder()
        {
            Console.WriteLine("请输入要修改的订单号：");
            string orderNumber = Console.ReadLine();
            Order orderToModify = orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
            if (orderToModify != null)
            {
                Console.WriteLine("请输入新的客户名：");
                string newCustomerName = Console.ReadLine();
                Console.WriteLine("订单日期将更新为：");
                DateTime newOrderDate = DateTime.Now;

                // 修改订单信息
                orderToModify.CustomerName = newCustomerName;
                orderToModify.OrderDate = newOrderDate;

                Console.WriteLine($"订单 {orderNumber} 已成功修改。\n");
                SortOrders(CompareByOrderNumber);
            }
            else
            {
                Console.WriteLine($"【提示】找不到订单号为 {orderNumber} 的订单。\n");
            }
        }

        public void QueryOrder()
        {
            Console.WriteLine("\n" +
                              "1.按照订单号查询\n" +
                              "2.按商品名查询\n" +
                              "3.按客户名查询\n" +
                              "4.按订单总金额查询");
            Console.Write("请选择查询条件：");
            int op = int.Parse(Console.ReadLine());

            switch (op)
            {
                case 1:
                    Console.WriteLine("请输入订单号：");
                    string orderNumberQuery = Console.ReadLine();
                    var orderByOrderNumber = orders.Where(o => o.OrderNumber.Contains(orderNumberQuery));
                    DisplayQueryResult(orderByOrderNumber);
                    break;
                case 2:
                    Console.WriteLine("请输入商品名：");
                    string productNameQuery = Console.ReadLine();
                    var orderByProductName = orders.Where(o => o.OrderItems.Any(oi => oi.ProductName.Contains(productNameQuery)));
                    DisplayQueryResult(orderByProductName);
                    break;
                case 3:
                    Console.WriteLine("请输入客户名：");
                    string customerNameQuery = Console.ReadLine();
                    var orderByCustomerName = orders.Where(o => o.CustomerName.Contains(customerNameQuery));
                    DisplayQueryResult(orderByCustomerName);
                    break;
                case 4:
                    Console.WriteLine("请输入订单总金额范围（最小金额和最大金额）：");
                    decimal minAmount = decimal.Parse(Console.ReadLine());
                    decimal maxAmount = decimal.Parse(Console.ReadLine());
                    var orderByTotalAmount = orders.Where(o => o.TotalAmount >= minAmount && o.TotalAmount <= maxAmount);
                    DisplayQueryResult(orderByTotalAmount);
                    break;
                default:
                    Console.WriteLine("无效的查询条件。\n");
                    break;
            }
        }

        private void DisplayQueryResult(IEnumerable<Order> queryResult)
        {
            if (queryResult.Any())
            {
                Console.WriteLine("查询结果：");
                queryResult = queryResult.OrderByDescending(order => order.TotalAmount);
                foreach (var order in queryResult)
                {
                    Console.WriteLine(order.ToString());
                }
            }
            else
            {
                Console.WriteLine("未找到匹配的订单。\n");
            }
        }

        public void ShowAllOrders()
        {
            var sortOrders = orders.OrderByDescending(order => order.TotalAmount);
            foreach (var order in sortOrders)
            {
                Console.WriteLine(order.ToString());
                Console.WriteLine();
            }
        }

        public void Export()
        {
            try
            {
                using (FileStream fileStream = new FileStream("order.xml", FileMode.Create))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
                    xmlSerializer.Serialize(fileStream,orders);
                }
                Console.WriteLine("已经将订单导出为XML文件。\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"导出订单为XML文件时出现错误：{ex.Message}\n");
                throw;
            }
        }

        public void Import()
        {
            try
            {
                using (FileStream fileStream = new FileStream("order.xml", FileMode.Open))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
                    orders = (List<Order>)xmlSerializer.Deserialize(fileStream);
                }
                Console.WriteLine("已经从XML文件导入订单。\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"从XML文件导入订单时出现错误：{ex.Message}\n");
                throw;
            }
        }
    }
}