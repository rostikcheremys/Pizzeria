namespace Pizzeria
{
    public class PizzaInfo(string productName, string imagePath, string ingredients, double price) : Menu(productName, imagePath, price)
    {
        public string Ingredients { get; } = ingredients;

        public override string GetDescription()
        {
            return Ingredients;
        }
    }
}

