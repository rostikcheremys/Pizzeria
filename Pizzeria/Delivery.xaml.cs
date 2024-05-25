using System.IO;
using System.Windows;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows.Markup;
using Formatting = System.Xml.Formatting;

namespace Pizzeria
{
    public partial class Delivery
    {
        private readonly PizzaInfo _pizzaInfo;
        private readonly DrinkInfo _drinkInfo;
        private readonly Order _orderPage;
        private readonly Cart _cartPage;
        private readonly string _previousPage;
        private readonly string _isProductPage;
      
        public Delivery(double currentOrderPrice, string isProductPage, Order orderPage, Cart cartPage)
        {
            InitializeComponent();
            InitializeTimeComboBox();

            Price.Text = $"Price: ${currentOrderPrice:F2}";
            _cartPage = cartPage;
            _previousPage = "Cart";
            _isProductPage = isProductPage;
            _orderPage = orderPage; 

            DatePicker.SelectedDate = DateTime.Today;
            DatePicker.Language = XmlLanguage.GetLanguage("en-GB");
            DeliveryComboBox.SelectionChanged += DeliveryComboBox_SelectionChanged;
        }

        private void SaveOrderData(string filePath)
        {
            List<DeliveryInfo> orderDetailsList;
            
            if (_orderPage == null)
            { 
                MessageBox.Show("Order page is not initialized!");
                return;
            }

            if (File.Exists(filePath))
            {
                try
                {
                    string existingJson = File.ReadAllText(filePath);
                    orderDetailsList = JsonConvert.DeserializeObject<List<DeliveryInfo>>(existingJson) ?? new List<DeliveryInfo>();
                }
                catch (JsonSerializationException)
                {
                    orderDetailsList = new List<DeliveryInfo>();
                }
            }
            else
            {
                orderDetailsList = new List<DeliveryInfo>();
            }

            if (_previousPage == "Cart")
            {
                foreach (var cartItem in _cartPage.CartItems)
                {
                    DeliveryInfo orderDetails = new DeliveryInfo
                    {
                        Name = NameTextBox.Text,
                        Phone = PhoneTextBox.Text,
                        DeliveryOption = ((ComboBoxItem)DeliveryComboBox.SelectedItem)?.Content.ToString(),
                        DeliveryDate = DatePicker.SelectedDate ?? DateTime.Today,
                        DeliveryTime = $"{HourComboBox.Text}:{MinuteComboBox.Text}",
                        OrderInfo = 
                        [
                            new PizzaDetails
                            {
                                ProductName = cartItem.Product,
                                ProductSize = cartItem.Size,
                                ProductToppings= cartItem.Toppings.ToList(),
                                ProductQuantity = cartItem.Quantity,
                                ProductPrice = cartItem.Price
                            }
                        ]
                    };
                    orderDetailsList.Add(orderDetails);
                }
            }
            else
            {
                DeliveryInfo orderDetails = new DeliveryInfo
                {
                    Name = NameTextBox.Text,
                    Phone = PhoneTextBox.Text,
                    DeliveryOption = ((ComboBoxItem)DeliveryComboBox.SelectedItem)?.Content.ToString(),
                    City = CityTextBox.Text,
                    Address = AddressTextBox.Text,
                    DeliveryDate = DatePicker.SelectedDate ?? DateTime.Today,
                    DeliveryTime = $"{HourComboBox.Text}:{MinuteComboBox.Text}",
                };
                
                if (_isProductPage == "Pizza")
                {
                    PizzaDetails pizzaDetails = new PizzaDetails
                    {
                        ProductName = GetPizzaInfo()?.ProductName,
                        ProductSize = GetCurrentSize(),
                        ProductToppings = GetCurrentToppings().Where(t => !string.IsNullOrEmpty(t)).ToList(),
                        ProductQuantity = GetCurrentQuantity(),
                        ProductPrice = GetCurrentPrice()
                    };
                    orderDetails.OrderInfo.Add(pizzaDetails);
                }
                else
                {
                    DrinkDetails drinkDetails = new DrinkDetails
                    {
                        ProductName = GetDrinkInfo()?.ProductName,
                        ProductSize = GetCurrentSize(),
                        ProductQuantity = GetCurrentQuantity(),
                        ProductPrice = GetCurrentPrice()
                    };
                    orderDetails.OrderInfo.Add(drinkDetails);
                }
            
                orderDetailsList.Add(orderDetails);
            }
            
            string updatedJson = JsonConvert.SerializeObject(orderDetailsList, Newtonsoft.Json.Formatting.Indented);
            
            File.WriteAllText(filePath, updatedJson);
        }
        
        public Delivery(double currentOrderPrice, string isProductPage, PizzaInfo pizzaInfo, Order orderPage, Cart cartPage) : this(currentOrderPrice, isProductPage, orderPage, cartPage)
        {
            _pizzaInfo = pizzaInfo;
            _orderPage = orderPage;
            _previousPage = "Order";
            _isProductPage = "Pizza";
        }

        public Delivery(double currentOrderPrice,  string isProductPage, DrinkInfo drinkInfo, Order orderPage, Cart cartPage) : this(currentOrderPrice, isProductPage, orderPage, cartPage)
        {
            _drinkInfo = drinkInfo;
            _orderPage = orderPage;
            _previousPage = "Order";
            _isProductPage = "Drink";
        }

        private PizzaInfo GetPizzaInfo()
        {
            return _pizzaInfo;
        }
        
        private DrinkInfo GetDrinkInfo()
        {
            return _drinkInfo;
        }
        
        private double GetCurrentPrice()
        {
            double.TryParse(_orderPage.ProductPrice.Text.Replace("Price: $", ""), out double currentPrice);
            return currentPrice;
        }

        private int GetCurrentQuantity()
        {
            return int.Parse(_orderPage.Quantity.Text);
        }

        private string GetCurrentSize()
        {
            if (_orderPage.SizeSmall.IsChecked == true) return "Small";

            if (_orderPage.SizeMedium.IsChecked == true) return "Medium";

            if (_orderPage.SizeLarge.IsChecked == true) return "Large";

            return null!;
        }

        private List<string?> GetCurrentToppings()
        {
            List<string?> toppings = new List<string?>();

            foreach (CheckBox checkBox in _orderPage.ToppingStackPanel.Children)
            {
                if (checkBox.IsChecked == true)
                {
                    toppings.Add(checkBox.Content.ToString());
                }
            }

            return toppings;
        }
        
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (_previousPage == "Cart")
            {
                Cart cartPage = new Cart(_cartPage, _isProductPage, _orderPage);
                DeliveryPage.Navigate(cartPage);
            }
            else if (_previousPage == "Order")
            {
                if (_isProductPage == "Pizza")
                {
                    Order orderPage = new Order(GetPizzaInfo(), _cartPage, _isProductPage);

                    double currentPrice = GetCurrentPrice();
                    int currentQuantity = GetCurrentQuantity();
                    string selectedSize = GetCurrentSize();
                    List<string?> selectedToppings = GetCurrentToppings();

                    orderPage.SetCurrentPrice(currentPrice);
                    orderPage.SetSelectedQuantity(currentQuantity);
                    orderPage.SetSelectedSize(selectedSize);
                    orderPage.SetSelectedToppings(selectedToppings);

                    DeliveryPage.Navigate(orderPage);
                }
                else
                {
                    Order orderPage = new Order(GetDrinkInfo(), _cartPage, _isProductPage);

                    double currentPrice = GetCurrentPrice();
                    int currentQuantity = GetCurrentQuantity();
                    string selectedSize = GetCurrentSize();

                    orderPage.SetCurrentPrice(currentPrice);
                    orderPage.SetSelectedQuantity(currentQuantity);
                    orderPage.SetSelectedSize(selectedSize);

                    DeliveryPage.Navigate(orderPage);
                }
            }
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
        
        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            Func<bool>[] validations =
            [
                () => ValidateName(NameTextBox, "Please enter your name!"),
                () => ValidatePhone(PhoneTextBox, "Please enter your phone number!"),
                () => ValidateDelivery(DeliveryComboBox, "Please select a delivery option!"),
                () => ValidateCity(CityTextBox, "Please enter your city!", DeliveryComboBox.SelectedItem?.ToString()),
                () => ValidateAddress(AddressTextBox, "Please enter your delivery address!", DeliveryComboBox.SelectedItem?.ToString()),
                () => ValidateDate(DatePicker.SelectedDate, "Please select a delivery date!"),
                () => ValidateTime(HourComboBox.Text, MinuteComboBox.Text)
            ];

            bool checkValidations = validations.All(validation => validation());

            if (checkValidations)
            {
                bool confirmOrder = CustomMessageBox.Show("Do you want to confirm your order?");

                if (confirmOrder)
                {
                    CustomMessageBox.InfoShow("Order accepted. Wait for a call to confirm the delivery time.");
                    
                    SaveOrderData("orderDetails.json");
                    
                    InitializeTimeComboBox();
                    ClearFields();
                    
                    if (_isProductPage == "Pizza")
                    {
                        Pizza pizzaPage = new Pizza(_cartPage, _orderPage);
                        DeliveryPage.Navigate(pizzaPage);
                    }
                    else
                    {   
                        Drink drinkPage = new Drink(_cartPage, _orderPage);
                        DeliveryPage.Navigate(drinkPage);
                    }
                }
            }
        }
        
        private bool ValidateName(TextBox textBox, string errorMessage)
        {
            string name = textBox.Text;
            
            if (string.IsNullOrEmpty(name))
            {
                CustomMessageBox.InfoShow(errorMessage);
                return false;
            }
            
            if (System.Text.RegularExpressions.Regex.IsMatch(name, @"\d"))
            {
                CustomMessageBox.InfoShow("Name should not contain any digits!");
                return false;
            }

            return true;
        }
        
        private bool ValidatePhone(TextBox textBox, string errorMessage)
        {
            string phone = textBox.Text;
            
            if (string.IsNullOrEmpty(phone))
            {
                CustomMessageBox.InfoShow(errorMessage);
                return false;
            }
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\d+$"))
            {
                CustomMessageBox.InfoShow("Phone number should contain only digits!");
                return false;
            }
            
            return true;
        }
        
        private bool ValidateDelivery(ComboBox comboBox, string errorMessage)
        {
            if (comboBox.SelectedItem == null)
            {
                CustomMessageBox.InfoShow(errorMessage);
                return false;
            }
            return true;
        }
        
        private bool ValidateCity(TextBox textBox, string errorMessage, string? deliveryOption)
        {
            if (deliveryOption == "System.Windows.Controls.ComboBoxItem: On-site delivery")
            {
                return true;
            }

            string city = textBox.Text;

            if (string.IsNullOrEmpty(city))
            {
                CustomMessageBox.InfoShow(errorMessage);
                return false;
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(city, @"\d"))
            {
                CustomMessageBox.InfoShow("City should not contain any digits!");
                return false;
            }

            return true;
        }

        private bool ValidateAddress(TextBox textBox, string errorMessage, string? deliveryOption)
        {
            if (deliveryOption == "System.Windows.Controls.ComboBoxItem: On-site delivery")
            {
                return true;
            }

            if (string.IsNullOrEmpty(textBox.Text))
            {
                CustomMessageBox.InfoShow(errorMessage);
                return false;
            }
    
            return true;
        }
        
        private bool ValidateDate(DateTime? date, string errorMessage)
        {
            if (!date.HasValue || date < DateTime.Today)
            {
                CustomMessageBox.InfoShow(errorMessage);
                return false;
            }
            return true;
        }

        private bool ValidateTime(string hour, string minute)
        {
            if (string.IsNullOrEmpty(hour) || string.IsNullOrEmpty(minute))
            {
                CustomMessageBox.InfoShow("Please select a delivery time!");
                return false;
            }

            DateTime selectedDateTime = DatePicker.SelectedDate!.Value.Date.AddHours(int.Parse(hour)).AddMinutes(int.Parse(minute));

            if (selectedDateTime < DateTime.Now)
            {
                CustomMessageBox.InfoShow("The selected delivery date and time cannot be in the past!");
                return false;
            }

            return true;
        }
        
        private void ClearFields()
        {
            NameTextBox.Text = string.Empty;
            PhoneTextBox.Text = string.Empty;
            DeliveryComboBox.SelectedIndex = -1;
            CityTextBox.Text = string.Empty;
            AddressTextBox.Text = string.Empty;
            DatePicker.SelectedDate = DateTime.Today;
            _cartPage.ClearCart();
        }
    }
}
