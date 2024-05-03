using System.Windows;
using System.Windows.Controls;

namespace Pizzeria;

public partial class Pizza : Page
{
    public Pizza()
    {
        InitializeComponent();
    }
    
    private void OrderPepperoni_Click(object sender, RoutedEventArgs e) 
    {
        Order orderPage = new Order(
            "Pepperoni",
            "images/pizza/pepperoni.png",
            "Pepperoni, Chili pepper, Cheese, Tomato Sauce"
        );
        PizzaPage.Navigate(orderPage);
    }

    private void OrderSicilian_Click(object sender, RoutedEventArgs e)
    {
        Order orderPage = new Order(
            "Sicilian",
            "images/pizza/sicilian.png", 
            "Pepperoni, Olives, Tomato, Pepper, Cheese"
        );
        PizzaPage.Navigate(orderPage);
    }

    private void OrderHawaiian_Click(object sender, RoutedEventArgs e)
    {
        Order orderPage = new Order(
            "Hawaiian",
            "images/pizza/hawaiian.png",
            "Mozzarella, Chicken, Pineapple, Tomato Sauce"
        );
        PizzaPage.Navigate(orderPage);
    }

    private void OrderFourCheese_Click(object sender, RoutedEventArgs e)
    {
        Order orderPage = new Order(
            "Four Cheese",
            "images/pizza/four-cheese.png",
            "Mozzarella, Parmesan, Feta, Gorgonzola"
        );
        PizzaPage.Navigate(orderPage);
    }

    private void OrderProsciuttoEFunghi_Click(object sender, RoutedEventArgs e)
    {
        Order orderPage = new Order(
            "Prosciutto e Funghi",
            "images/pizza/prosciutto-e-funghi.png",
            "Ham, Mushrooms, Cheese, Tomato Sauce"
        );
        PizzaPage.Navigate(orderPage);
    }

    private void OrderChickenWithMushrooms_Click(object sender, RoutedEventArgs e)
    {
        Order orderPage = new Order(
            "Chicken with Mushrooms",
            "images/pizza/chicken-with-mushrooms.png",
            "Chicken, Mushrooms, Tomato, Cheese"
        );
        PizzaPage.Navigate(orderPage);
    }
}