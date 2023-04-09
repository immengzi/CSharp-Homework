using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManage
{
    [Serializable]
    public class OrderItem
    {
        public OrderItem(string coname, int idd, double pprice, int num)
        {

            commodity_name = coname;
            id = idd;
            per_price = pprice;
            number = num;
        }
        public OrderItem()
        {
        }
        public string commodity_name { get; set; }
        public int id { get; set; }
        public double per_price { get; set; }
        public int number { get; set; }
        public override string ToString()
        {
            return ($"商品名：{commodity_name}\t单价：{per_price}\t数量：{number}\r\n");
        }
        public override bool Equals(object obj)
        {
            OrderItem m = obj as OrderItem;
            return m != null && m.commodity_name == commodity_name
              && m.per_price == per_price && m.number == number;
        }
        public override int GetHashCode()
        {
            return id;
        }
    }
}
