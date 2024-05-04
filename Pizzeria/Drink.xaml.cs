using System.Windows;
using System.Windows.Controls;

namespace Pizzeria
{
    public partial class Drink : Page
    {
        public Drink()
        {
            InitializeComponent();
        }
    
        private void PizzaButton_Click(object sender, RoutedEventArgs e)
        {
            if (DrinkPage.Content is Pizza)
            {
                return;
            }
            
            Pizza pizzaPage = new Pizza(); 
            DrinkPage.Navigate(pizzaPage);
        }

        private void DrinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (DrinkPage.Content is Drink)
            {
                return;
            }
            
            Drink drinkPage = new Drink();
            DrinkPage.Navigate(drinkPage); 
        }
    }
}

