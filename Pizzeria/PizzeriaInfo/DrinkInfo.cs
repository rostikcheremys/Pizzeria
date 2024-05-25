namespace Pizzeria.PizzeriaInfo
{
    public class DrinkInfo(string? productName, string imagePath, string description, double price) : Menu(productName, imagePath, price)
    {
        public string Description { get; } = description;
        
        public override string GetDescription()
        {
            return Description;
        }
    }
}

