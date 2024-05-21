namespace Pizzeria
{
    public abstract class Menu(string? name, string imagePath, double price)
    {
        public string? Name { get; } = name;
        public string ImagePath { get; } = imagePath;
        public double Price { get; } = price;

        public abstract string GetDescription();
    }
}