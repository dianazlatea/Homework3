using System;

namespace BusinessApp
{
    public class Product
    {
        private int productId;
        private string productName;
        private double price;

        public int ProductId { get => productId; set => productId = value; }
        public string ProductName { get => productName; set => productName = value; }
        public double Price { get => price; set => price = value; }

        public Product(int productId, string productName, double price)
        {
            this.productId = productId;
            this.productName = productName;
            this.price = price;
        }

        public override string ToString()
        {
            return $"Product ID: {productId}, Name: {productName}, Price: {price:C}";
        }
    }
}
