using System.Windows;
using System.Windows.Controls;

namespace Pizzeria;

public partial class Delivery
{
    public Delivery()
    {
        InitializeComponent();
    }
    
    private string FullName { get; set; }
    private string PhoneNumber { get; set; }
    private string ChoiceDelivery { get; set; }
    private string CookingTime { get; set; }
    private string DeliveryTime { get; set; }
    private string Price { get; set; }

    public Delivery(string fullName, string phoneNumber, string choiceDelivery, string cookingTime, string deliveryTime, string price)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        ChoiceDelivery = choiceDelivery;
        CookingTime = cookingTime;
        DeliveryTime = deliveryTime;
        Price = price;
    }

    public void Deliver()
    {
        MessageBox.Show($"Delivering to: {FullName}\nContact number: {PhoneNumber}\nDelivery option: {ChoiceDelivery}\nCooking time: {CookingTime}\nEstimated delivery time: {DeliveryTime}\nTotal price: {Price}");
    }
    
    private void DeliverButton_Click(object sender, RoutedEventArgs e)
    {
        Delivery delivery = new Delivery(NameTextBox.Text, PhoneTextBox.Text, DeliveryOptionComboBox.Text, CookingTimeTextBox.Text, DeliveryTimeTextBox.Text, PriceTextBox.Text);
        delivery.Deliver();
    }
}