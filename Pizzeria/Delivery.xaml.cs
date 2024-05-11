using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Pizzeria;

public partial class Delivery
{
    private string FullName { get; set; }
    private string PhoneNumber { get; set; }
    private string ChoiceDelivery { get; set; }
    private string CookingTime { get; set; }
    private string DeliveryTime { get; set; }
    private string Price { get; set; }

    public Delivery()
    {
        InitializeComponent();
    }

    public Delivery(string fullName, string phoneNumber, string choiceDelivery, string cookingTime, string deliveryTime, string price)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        ChoiceDelivery = choiceDelivery;
        CookingTime = cookingTime;
        DeliveryTime = deliveryTime;
        Price = price;
    }
    

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        Pizza pizzaPage = new Pizza(); 
        DeliveryPage.Navigate(pizzaPage);
    }
    
    private void DeliverButton_Click(object sender, RoutedEventArgs e)
    {
    }
}