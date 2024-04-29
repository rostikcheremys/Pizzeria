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
        protected new string Name { get; set; }
        protected string Description { get; set; }
        protected double Price { get; set; }
        
        public Menu()
        {
            Name = "";
            Description = ""; 
            Price = 0.0;
            
            InitializeComponent();
            
            Pizza pizzaPage = new Pizza();
            MenuPage.Navigate(pizzaPage); 
        }

        protected Menu(string name, string description, double price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        private void PizzaButton_Click(object sender, RoutedEventArgs e)
        {
                      

        }

        private void DrinkButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }







        /*
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

               Delivery deliveryPage = new Delivery();
               MenuFrame.Navigate(deliveryPage);
           }
           */
    }
}
