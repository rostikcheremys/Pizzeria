namespace Pizzeria
{
    public class PizzaInfo(string name, string imagePath, string ingredients, double price) : Menu(name, imagePath, price)
    {
        public string Ingredients { get; } = ingredients;

        public override string GetIngredients()
        {
            return Ingredients;
        }
    }
}

