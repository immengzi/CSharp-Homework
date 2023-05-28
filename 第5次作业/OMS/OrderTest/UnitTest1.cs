using NUnit.Framework;
using OMS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OrderTest
{
    public class Tests
    {
        private OrderService orderService;
        private StringWriter consoleOutput;

        [SetUp]
        public void Setup()
        {
            orderService = new OrderService();
            consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
        }

        [TearDown]
        public void Cleanup()
        {
            consoleOutput.Dispose();
        }

        // 编写第一个测试类，以增加订单为例
        [Test]
        public void AddOrder_WhenValidInput_OrderAdded()
        {
            // 设置控制台输入
            string input = "123\nJohn Doe\nProduct A\n2\n10\nn\n";
            using (StringReader reader = new StringReader(input))
            {
                Console.SetIn(reader);

                // 设置控制台输出重定向
                StringBuilder outputBuilder = new StringBuilder();
                using (StringWriter writer = new StringWriter(outputBuilder))
                {
                    Console.SetOut(writer);

                    // 调用被测试的方法
                    orderService.AddOrder();

                    // 断言测试结果
                    Assert.AreEqual(1, orderService.orders.Count); // 确保订单数量正确
                    Assert.AreEqual("123", orderService.orders[0].OrderNumber); // 确保订单号正确
                    Assert.AreEqual("John Doe", orderService.orders[0].CustomerName); // 确保客户名正确
                    // 检查订单明细
                    Assert.AreEqual(1, orderService.orders[0].OrderItems.Count); // 确保订单明细数量正确;
                    Assert.AreEqual("Product A", orderService.orders[0].OrderItems[0].ProductName); // 确保商品名正确
                    Assert.AreEqual(2, orderService.orders[0].OrderItems[0].Quantity); // 确保商品数量正确
                    Assert.AreEqual(10, orderService.orders[0].OrderItems[0].UnitPrice); // 确保商品单价正确

                    Assert.AreEqual(20, orderService.orders[0].TotalAmount); // 确保订单总价正确

                    // 检查控制台输出
                    string expectedOutput = "请输入订单号:\r\n" +
                                            "请输入客户名:\r\n" +
                                            "请输入订单明细~\r\n" +
                                            "请输入商品名:\r\n" +
                                            "请输入商品数量:\r\n" +
                                            "请输入商品单价:\r\n" +
                                            "是否继续添加订单明细？(y/n)\r\n" +
                                            "订单已成功添加。\n\r\n";
                    Assert.AreEqual(expectedOutput, outputBuilder.ToString());
                }
            }
        }
    }
}