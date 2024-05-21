namespace Pizzeria
{
    public class CartItem(string product, string size, List<string> toppings, int quantity, double price)
    {
        public string Product { get; } = product;
        public string Size { get; } = size;
        public List<string> Toppings { get; } = toppings;
        public int Quantity { get; set; } = quantity;
        public double Price { get; set; } = price;
    }
}

