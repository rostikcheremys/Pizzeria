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
        MessageBox.Show("OrderPepperoni_Click");
        Order orderPage = new Order();
        PizzaPage.Navigate(orderPage);
    }
    
    private void OrderSicilian_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("OrderSicilian_Click");
    }
    
    private void OrderHawaiian_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("OrderHawaiian_Click");
    }
    
    private void OrderFourCheese_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("OrderFourCheese_Click");
    }
    
    private void OrderProsciuttoEFunghi_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("OrderProsciuttoEFunghi_Click");
    }
    
    private void OrderChickenWithMushrooms_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("OrderChickenWithMushrooms_Click");
    }
}