using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pizzeria
{
    public partial class Order : Page
    {
        private readonly double _basePrice;
        
        public Order(PizzaInfo info)
        {
            InitializeComponent();
            
            PizzaInfo pizzaInfo = info;

            PizzaName.Content = pizzaInfo.Name;
            PizzaImage.Source = new BitmapImage(new Uri(pizzaInfo.ImagePath, UriKind.Relative));
            PizzaIngredients.Text = pizzaInfo.Ingredients;
            _basePrice = pizzaInfo.Price;
            
            SizeSmall.Checked += PizzaSize_Checked;
            SizeMedium.Checked += PizzaSize_Checked;
            SizeLarge.Checked += PizzaSize_Checked;

            foreach (CheckBox cb in ToppingStackPanel.Children)
            {
                cb.Checked += ToppingChanged;
                cb.Unchecked += ToppingChanged;
            }

            UpdatePriceDisplay();
        }

        private void PizzaSize_Checked(object sender, RoutedEventArgs e)
        {
            UpdatePriceDisplay();
        }

        private void ToppingChanged(object sender, RoutedEventArgs e)
        {
            UpdatePriceDisplay();
        }

        private double GetPrice()
        {
            double multiplier = 1.0;
            
            if (SizeMedium.IsChecked == true)
            {
                multiplier = 1.5;
            }
            else if (SizeLarge.IsChecked == true)
            {
                multiplier = 2.0;
            }
            
            return _basePrice * multiplier;
        }

        private double GetToppingsPrice()
        {
            double toppingPrice = 1; 
            double totalToppingsPrice = 0;

            foreach (CheckBox cb in ToppingStackPanel.Children)
            {
                if (cb.IsChecked == true)
                {
                    totalToppingsPrice += toppingPrice;
                }
            }

            return totalToppingsPrice;
        }

        private void UpdatePriceDisplay()
        {
            int quantity = GetCurrentQuantity();
            double total = (GetPrice() + GetToppingsPrice()) * quantity;

            PizzaPrice.Text = $"Price: ${total}";
        }

        private int GetCurrentQuantity()
        {
            return int.Parse(PizzaQuantity.Text);
        }

        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            int currentQuantity = GetCurrentQuantity();
            currentQuantity++;
            PizzaQuantity.Text = currentQuantity.ToString();

            UpdatePriceDisplay();
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            int currentQuantity = GetCurrentQuantity();
            
            if (currentQuantity > 1)
            {
                currentQuantity--;
                PizzaQuantity.Text = currentQuantity.ToString();
                UpdatePriceDisplay();
            }
        }
            
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Pizza menuPage = new Pizza();
            OrderPage.Navigate(menuPage);
        }
        
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            int quantity = GetCurrentQuantity();
            MessageBox.Show($"Add {quantity} to Cart");
        }

        private void OrderNow_Click(object sender, RoutedEventArgs e)
        {
            Delivery deliveryPage = new Delivery(); 
            OrderPage.Navigate(deliveryPage);
        }
    }
}
