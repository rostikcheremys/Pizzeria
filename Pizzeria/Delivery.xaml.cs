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
    
    private readonly PizzaInfo _pizzaInfo;

    public Delivery(double currentOrderPrice, PizzaInfo pizzaInfo)
    {
        InitializeComponent();
        InitializeTimeComboBox();
    
        Price.Text = $"Price: ${currentOrderPrice}";

        _pizzaInfo = pizzaInfo; 
    }

    private PizzaInfo GetPizzaInfo()
    {
        return _pizzaInfo;
    }

    public Delivery(string fullName, string phoneNumber, string choiceDelivery, string cookingTime, string deliveryTime, double price)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        ChoiceDelivery = choiceDelivery;
        CookingTime = cookingTime;
        DeliveryTime = deliveryTime;
        //Price = price;
    }
    
    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        PizzaInfo currentOrderPrice = GetPizzaInfo();
        Order orderPage = new Order(currentOrderPrice); 
        DeliveryPage.Navigate(orderPage);
    }
    private void OrderButton_Click(object sender, RoutedEventArgs e)
    {
    }
    
    private void InitializeTimeComboBox()
    {
        // Очищаємо вміст ComboBox
        HourComboBox.Items.Clear();
        MinuteComboBox.Items.Clear();

        // Перевіряємо, чи обрана дата є сьогоднішньою
        if (DatePicker.SelectedDate.HasValue && DatePicker.SelectedDate.Value.Date == DateTime.Today)
        {
            int currentHour = DateTime.Now.Hour;
            int currentMinute = DateTime.Now.Minute;

            // Додаємо доступні години в HourComboBox
            for (int i = currentHour; i < 24; i++)
            {
                HourComboBox.Items.Add(i);
            }

            if (HourComboBox.Items.Count > 0 && currentHour < Convert.ToInt32(HourComboBox.Items[0]))
            {
                for (int i = currentMinute; i < 60; i++)
                {
                    MinuteComboBox.Items.Add(i.ToString("D2"));
                }
            }
            else if (HourComboBox.Items.Count > 0 && currentHour == Convert.ToInt32(HourComboBox.Items[0]))
            {
                for (int i = currentMinute; i < 60; i++)
                {
                    MinuteComboBox.Items.Add(i.ToString("D2"));
                }
            }
            else
            {
                for (int i = 0; i < 60; i++)
                {
                    MinuteComboBox.Items.Add(i.ToString("D2"));
                }
            }
        }
        else
        {
            for (int i = 0; i < 24; i++)
            {
                HourComboBox.Items.Add(i);
            }
            for (int i = 0; i < 60; i++)
            {
                MinuteComboBox.Items.Add(i.ToString("D2"));
            }
        }
    }
    
    private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DatePicker.SelectedDate.HasValue && DatePicker.SelectedDate.Value.Date < DateTime.Today)
        {
            DatePicker.SelectedDate = DateTime.Today;
        }
        
        InitializeTimeComboBox();
    }
}