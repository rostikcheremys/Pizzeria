namespace Pizzeria.PizzeriaInfo
{
    public class PizzaInfo(string productName, string imagePath, string ingredients, double price) : MenuInfo(productName, imagePath, price)
    {
        public string Ingredients { get; } = ingredients;

        public override string GetDescription()
        {
            return Ingredients;
        }
    }
}

