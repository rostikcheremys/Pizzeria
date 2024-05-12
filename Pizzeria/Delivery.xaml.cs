using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Pizzeria;

public partial class Delivery
{
    private new string Name { get; set; }
    private string PhoneNumber { get; set; }
    private string ChoiceDelivery { get; set; }
    private string Data { get; set; }
    private string Time { get; set; }
    
    
    private readonly PizzaInfo _pizzaInfo;
    private Order _orderPage; 

    public Delivery(double currentOrderPrice, PizzaInfo pizzaInfo, Order orderPage)
    {
        InitializeComponent();
        InitializeTimeComboBox();
        Price.Text = $"Price: ${currentOrderPrice}";

        _pizzaInfo = pizzaInfo; 
        _orderPage = orderPage;
    }

    private PizzaInfo GetPizzaInfo()
    {
        return _pizzaInfo;
    }

    private string GetCurrentSize()
    {
        if (_orderPage.SizeSmall.IsChecked == true)
        {
            return "Small";
        }
        if (_orderPage.SizeMedium.IsChecked == true)
        {
            return "Medium";
        }
        if (_orderPage.SizeLarge.IsChecked == true)
        {
            return "Large";
        }
        return null!; 
    }

    private List<string?> GetCurrentToppings()
    {
        List<string?> toppings = new List<string?>();

        foreach (CheckBox cb in _orderPage.ToppingStackPanel.Children)
        {
            if (cb.IsChecked == true)
            {
                toppings.Add(cb.Content.ToString());
            }
        }

        return toppings;
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        string selectedSize = GetCurrentSize();
        List<string?> selectedToppings = GetCurrentToppings();
        
        Order orderPage = new Order(GetPizzaInfo());
        orderPage.SetSelectedSize(selectedSize);
        orderPage.SetSelectedToppings(selectedToppings);
        
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