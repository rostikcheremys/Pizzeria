namespace Pizzeria
{
    public class CartItem(string? product, int quantity, double price)
    {
        public string? Product { get; set; } = product;
        public int Quantity { get; set; } = quantity;
        public double Price { get; set; } = price;
    }
}

