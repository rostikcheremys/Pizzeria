namespace Pizzeria
{
    public class DrinkInfo(string? name, string imagePath, string description,  double price) : Menu(name, price, imagePath)
    {
        public string Description { get; set; } = description;
    }
}

