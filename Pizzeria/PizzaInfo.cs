namespace Pizzeria
{
    public class PizzaInfo(string? name, string imagePath, string ingredients,  double price) : Menu(name, price, imagePath)
    {
        public string Ingredients { get; set; } = ingredients;
    }
}

