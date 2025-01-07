
using System.Collections.Generic;
using System.Linq;

namespace BusinessApp
{
    public class Order
    {
        private int orderId;
        private string orderDate;
        private List<Product> productList;

        public int OrderId { get => orderId; set => orderId = value; }
        public string OrderDate { get => orderDate; set => orderDate = value; }
        public List<Product> ProductList { get => productList; set => productList = value; }

        public Order(int orderId, string orderDate, List<Product> productList)
        {
            this.orderId = orderId;
            this.orderDate = orderDate;
            this.productList = productList;
        }

        public override string ToString()
        {
            string products = string.Join(", ", productList.Select(p => p.ProductName));
            return $"Order ID: {orderId}, Date: {orderDate}, Products: {products}";
        }
    }
}

