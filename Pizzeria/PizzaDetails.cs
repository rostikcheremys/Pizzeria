namespace Pizzeria;

public class PizzaDetails(string name, string imagePath, string ingredients, double price)
{
    public string Name { get; set; } = name;
    public string ImagePath { get; set; } = imagePath;
    public string Ingredients { get; set; } = ingredients;
    public double Price { get; set; } = price;
}