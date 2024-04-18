using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pizzeria
{
    public partial class Menu 
    {
        
        protected Menu(string name, string description)
        {
            Name = name;
            Description = description;
            InitializeComponent();
        }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        
        public Menu(string name, string description, double price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> selectedItems = new List<string>();

            if (MargheritaCheckbox.IsChecked == true)
                selectedItems.Add("Margherita");
            if (PepperoniCheckbox.IsChecked == true)
                selectedItems.Add("Pepperoni");
            if (VegetarianCheckbox.IsChecked == true)
                selectedItems.Add("Vegetarian");
            if (HawaiianCheckbox.IsChecked == true)
                selectedItems.Add("Hawaiian");

            Order newOrder = new Order(selectedItems);
            newOrder.DisplayOrder();
            MenuPage.Content = new Delivery();
        }

    }
    public class PizzaMenuItem : Menu
    {
        public string Toppings { get; set; }

        public PizzaMenuItem(string name, string description, double price, string toppings) : base(name, description, price)
        {
            Toppings = toppings;
        }

        public virtual string GetDetails()
        {
            return $"Pizza: {Name}\nDescription: {Description}\nToppings: {Toppings}\nPrice: {Price}";
        }
    }

    public class DrinkMenuItem : Menu
    {
        public string Size { get; set; }

        public DrinkMenuItem(string name, string description, double price, string size) : base(name, description, price)
        {
            Size = size;
        }

        public virtual string GetDetails()
        {
            return $"Drink: {Name}\nDescription: {Description}\nSize: {Size}\nPrice: {Price}";
        }
    }
}
