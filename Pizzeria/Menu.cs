namespace Pizzeria
{
    public abstract class Menu(string? name, string imagePath, double price)
    {
        public string? Name { get; set; } = name;
        public string ImagePath { get; set; } = imagePath;
        public double Price { get; set; } = price;

        public abstract string GetIngredients();
    }
}