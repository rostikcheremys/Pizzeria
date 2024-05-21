namespace Pizzeria
{
    public class CartItem(string product, string size, List<string> toppings, int quantity, double price)
    {
        public string Product { get; set; } = product;
        public string Size { get; set; } = size;
        public List<string> Toppings { get; set; } = toppings;
        public int Quantity { get; set; } = quantity;
        public double Price { get; set; } = price;
    }
}

