namespace Pizzeria
{
    public abstract class Menu(string? productName, string imagePath, double price)
    {
        public string? ProductName { get; } = productName;
        public string ImagePath { get; } = imagePath;
        public double Price { get; } = price;

        public abstract string GetDescription();
    }
}