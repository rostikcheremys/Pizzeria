namespace Pizzeria
{
    public class Menu
    {
        public string? Name { get; set; }
        public string ImagePath { get; set; }
        public double Price { get; set; }

        protected Menu(string? name, double price, string imagePath)
        {
            Name = name;
            ImagePath = imagePath;
            Price = price;
        }
    }
}