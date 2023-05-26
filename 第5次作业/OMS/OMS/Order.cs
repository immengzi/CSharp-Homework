namespace OMS
{
    [Serializable]
    public class Order
    {
        // 订单号
        public string? OrderNumber { get; set; }
        // 客户名
        public string? CustomerName { get; set; }
        // 订单日期
        public DateTime OrderDate { get; set; }
        // 订单明细
        public List<OrderDetails> OrderItems { get; set; }
        // 订单总金额
        public decimal TotalAmount
        {
            get
            {
                decimal total = 0;
                foreach (OrderDetails item in OrderItems)
                {
                    total += item.Quantity * item.UnitPrice;
                }
                return total;
            }
        }

        public override string ToString()
        {
            string orderInfo = $"订单号: {OrderNumber}\n";
            orderInfo += $"客户名: {CustomerName}\n";
            orderInfo += "订单明细:\n";
            foreach (OrderDetails item in OrderItems)
            {
                orderInfo += $"{item}\n";
            }
            orderInfo += $"总金额: {TotalAmount:C}";

            return orderInfo;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Order otherOrder = (Order)obj;
            // 根据订单的唯一标识进行比较，例如订单号
            return OrderNumber == otherOrder.OrderNumber;
        }

        public override int GetHashCode()
        {
            return OrderNumber.GetHashCode();
        }
    }

    public class OrderDetails
    {
        // 商品名
        public string? ProductName { get; set; }
        // 商品数量
        public int Quantity { get; set; }
        // 商品单价
        public decimal UnitPrice { get; set; }
        public override string ToString()
        {
            return $"{ProductName} - 数量: {Quantity}, 单价: {UnitPrice:C}";
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            OrderDetails otherDetails = (OrderDetails)obj;
            // 根据订单明细的唯一标识进行比较，例如商品名
            return ProductName == otherDetails.ProductName;
        }

        public override int GetHashCode()
        {
            return ProductName.GetHashCode();
        }
    }
}
