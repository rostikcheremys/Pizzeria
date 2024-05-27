namespace Pizzeria.PizzeriaInfo
{
    public abstract class MenuInfo(string? productName, string imagePath, double price)
    {
        public string? ProductName { get; } = productName;
        public string ImagePath { get; } = imagePath;
        public double Price { get; } = price;
        public abstract string GetDescription();
    }
}
