namespace Pizzeria
{
    public class DrinkInfo(string? name, string imagePath, string description, double price) : Menu(name, imagePath, price)
    {
        public string Description { get; } = description;
        
        public override string GetDescription()
        {
            return Description;
        }
    }
}

