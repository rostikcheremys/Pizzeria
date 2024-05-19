using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Pizzeria
{
    public partial class Delivery
    {
        private new string Name { get; set; }
        private string PhoneNumber { get; set; }
        private string ChoiceDelivery { get; set; }
        private string Data { get; set; }
        private string Time { get; set; }

        private readonly PizzaInfo _pizzaInfo;
        private readonly Order _orderPage;
        private readonly Cart _cartPage;

        public Delivery(double currentOrderPrice, PizzaInfo pizzaInfo, Order orderPage, Cart cartPage)
        {
            InitializeComponent();
            InitializeTimeComboBox();
            Price.Text = $"Price: ${currentOrderPrice}";

            _pizzaInfo = pizzaInfo;
            _orderPage = orderPage;
            _cartPage = cartPage;
            
            
            DeliveryComboBox.SelectionChanged += DeliveryComboBox_SelectionChanged;
        }

        private PizzaInfo GetPizzaInfo()
        {
            return _pizzaInfo;
        }

        private double GetCurrentPrice()
        {
            double.TryParse(_orderPage.PizzaPrice.Text.Replace("Price: $", ""), out var currentPrice);
            return currentPrice;
        }

        private int GetCurrentQuantity()
        {
            return int.Parse(_orderPage.PizzaQuantity.Text);
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
            double currentPrice = GetCurrentPrice();
            int currentQuantity = GetCurrentQuantity();
            string selectedSize = GetCurrentSize();
            List<string?> selectedToppings = GetCurrentToppings();

            Order orderPage = new Order(GetPizzaInfo(), _cartPage);

            orderPage.SetCurrentPrice(currentPrice);
            orderPage.SetSelectedQuantity(currentQuantity);
            orderPage.SetSelectedSize(selectedSize);
            orderPage.SetSelectedToppings(selectedToppings);

            DeliveryPage.Navigate(orderPage);
        }


        private void InitializeTimeComboBox()
        {
            HourComboBox.Items.Clear();
            MinuteComboBox.Items.Clear();
            
            DateTime now = DateTime.Now;

            int currentHour = DatePicker.SelectedDate.HasValue && DatePicker.SelectedDate.Value.Date == now.Date ? now.Hour : 0;

            for (int hour = currentHour; hour < 24; hour++)
            {
                HourComboBox.Items.Add(hour.ToString("D2"));
            }

            for (int minute = 0; minute < 60; minute++)
            {
                MinuteComboBox.Items.Add(minute.ToString("D2"));
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
        private void DeliveryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DeliveryComboBox.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)DeliveryComboBox.SelectedItem;
                
                string? deliveryOption = selectedItem.Content.ToString();

                if (deliveryOption == "On-site delivery")
                {
                    CityLabel.Visibility = Visibility.Collapsed;
                    CityTextBox.IsEnabled = false;
                    CityTextBox.Visibility = Visibility.Collapsed;
                    AddressLabel.Visibility = Visibility.Collapsed;
                    AddressTextBox.IsEnabled = false;
                    AddressTextBox.Visibility = Visibility.Collapsed;
                }
                else if (deliveryOption == "Delivery to the address")
                {
                    CityLabel.Visibility = Visibility.Visible;
                    CityTextBox.IsEnabled = true;
                    CityTextBox.Visibility = Visibility.Visible;
                    AddressLabel.Visibility = Visibility.Visible;
                    AddressTextBox.IsEnabled = true;
                    AddressTextBox.Visibility = Visibility.Visible;
                }
            }
        }
        
        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            Func<bool>[] validations =
            [
                () => ValidateName(NameTextBox, "Please enter your name!"),
                () => ValidatePhone(PhoneTextBox, "Please enter your phone number!"),
                () => ValidateDelivery(DeliveryComboBox, "Please select a delivery option!"),
                () => ValidateCity(CityTextBox, "Please enter your city!"),
                () => ValidateAddress(AddressTextBox, "Please enter your delivery address!"),
                () => ValidateDate(DatePicker.SelectedDate, "Please select a delivery date!"),
                () => ValidateTime(HourComboBox.Text, MinuteComboBox.Text)
            ];

            if (validations.All(validation => validation()))
            {
                MessageBox.Show("Order placed successfully!");
            }
        }
        
        private bool ValidateName(TextBox textBox, string errorMessage)
        {
            string name = textBox.Text;
            
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(errorMessage);
                return false;
            }
            
            if (System.Text.RegularExpressions.Regex.IsMatch(name, @"\d"))
            {
                MessageBox.Show("Name should not contain any digits!");
                return false;
            }

            return true;
        }
        
        private bool ValidatePhone(TextBox textBox, string errorMessage)
        {
            string phone = textBox.Text;
            
            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show(errorMessage);
                return false;
            }
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\d+$"))
            {
                MessageBox.Show("Phone number should contain only digits!");
                return false;
            }
            
            return true;
        }
        
        private bool ValidateDelivery(ComboBox comboBox, string errorMessage)
        {
            if (comboBox.SelectedItem == null)
            {
                MessageBox.Show(errorMessage);
                return false;
            }
            return true;
        }
        
        private bool ValidateCity(TextBox textBox, string errorMessage)
        {
            string city = textBox.Text;
            
            if (string.IsNullOrEmpty(city))
            {
                MessageBox.Show(errorMessage);
                return false;
            }
            
            if (System.Text.RegularExpressions.Regex.IsMatch(city, @"\d"))
            {
                MessageBox.Show("City should not contain any digits!");
                return false;
            }

            return true;
        }

        private bool ValidateAddress(TextBox textBox, string errorMessage)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show(errorMessage);
                return false;
            }
            return true;
        }
        
        private bool ValidateDate(DateTime? date, string errorMessage)
        {
            if (!date.HasValue || date < DateTime.Today)
            {
                MessageBox.Show(errorMessage);
                return false;
            }
            return true;
        }

        private bool ValidateTime(string hour, string minute)
        {
            if (string.IsNullOrEmpty(hour) || string.IsNullOrEmpty(minute))
            {
                MessageBox.Show("Please select a delivery time.");
                return false;
            }

            DateTime selectedDateTime = DatePicker.SelectedDate!.Value.Date.AddHours(int.Parse(hour)).AddMinutes(int.Parse(minute));

            if (selectedDateTime < DateTime.Now)
            {
                MessageBox.Show("The selected delivery date and time cannot be in the past.");
                return false;
            }

            return true;
        }
    }
}
