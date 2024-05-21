using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pizzeria
{
    public partial class Order
    {
        private readonly double _basePrice;
        private readonly Cart _cartPage;
        private readonly Menu _menu;

        public Order(Menu menu, Cart cartPage, string navigationSource)
        {
            InitializeComponent();

            _menu = menu;
            _cartPage = cartPage;

            ProductName.Content = menu.Name;
            ProductImage.Source = new BitmapImage(new Uri(menu.ImagePath, UriKind.Relative));
            ProductDescription.Text = menu.GetDescription();
            _basePrice = menu.Price;
            
            SizeSmall.Checked += Size_Checked;
            SizeMedium.Checked += Size_Checked;
            SizeLarge.Checked += Size_Checked;
            
            if (navigationSource == "Drink")
            {
                SizeSmall.Content = "Small 250ml";
                SizeMedium.Content = "Medium 400ml";
                SizeLarge.Content = "Large 500ml";
                LabelAddToppings.Visibility = Visibility.Collapsed;
                ToppingStackPanel.Children.Clear();
            }
            else
            {
                foreach (CheckBox cb in ToppingStackPanel.Children)
                {
                    cb.Checked += ToppingChanged;
                    cb.Unchecked += ToppingChanged;
                }
            }
            
            UpdatePriceDisplay();
            DisplayIngredients();
        }
        
        private void DisplayIngredients()
        {
            Menu productMenu = _menu;
            string description = productMenu.GetDescription();
            string? productName = productMenu.Name;
            string[] descriptionList = description.Split(',');
    
            Console.WriteLine($"Description: {productName}");
            foreach (string ingredient in descriptionList)
            {
                Console.WriteLine("- " + ingredient.Trim());
            }

            Console.WriteLine();
        }

        private void Size_Checked(object sender, RoutedEventArgs e)
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

        private double GetCurrentPrice()
        {
            double.TryParse(ProductPrice.Text.Replace("Price: $", ""), out double currentPrice);
            return currentPrice;
        }
        
        public void SetSelectedSize(string size)
        {
            if (size == "Small")
            {
                SizeSmall.IsChecked = true;
            }
            else if (size == "Medium")
            {
                SizeMedium.IsChecked = true;
            }
            else if (size == "Large")
            {
                SizeLarge.IsChecked = true;
            }
        }
        
        private string GetSelectedSize()
        {
            if (SizeSmall.IsChecked == true)
            {
                return "Small";
            }
            if (SizeMedium.IsChecked == true)
            {
                return "Medium";
            }
            if (SizeLarge.IsChecked == true)
            {
                return "Large";
            }
            return "Unknown";
        }

        public void SetSelectedToppings(List<string?> toppings)
        {
            foreach (CheckBox cb in ToppingStackPanel.Children)
            {
                cb.IsChecked = toppings.Contains(cb.Content.ToString());
            }
        }
        
        public void SetCurrentPrice(double price)
        {
            ProductPrice.Text = $"Price: ${price:F2}";
        }
        
        public void SetSelectedQuantity(int quantity)
        {
            Quantity.Text = quantity.ToString();
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

        private List<string> GetSelectedToppings()
        {
            List<string> toppings = new List<string>();

            foreach (CheckBox cb in ToppingStackPanel.Children)
            {
                if (cb.IsChecked == true)
                {
                    toppings.Add(cb.Content.ToString()!);
                }
            }

            return toppings;
        }

        private void UpdatePriceDisplay()
        {
            int quantity = GetCurrentQuantity();
            double total = (GetPrice() + GetToppingsPrice()) * quantity;

            ProductPrice.Text = $"Price: ${total:F2}";
        }

        private int GetCurrentQuantity()
        {
            return int.Parse(Quantity.Text);
        }
        
        private PizzaInfo GetCurrentPizzaInfo()
        {
            PizzaInfo pizzaInfo = new PizzaInfo(
                ProductName.Content.ToString()!,          
                ((BitmapImage)ProductImage.Source).UriSource.ToString(),
                ProductDescription.Text,                    
                _basePrice                               
            );

            return pizzaInfo;
        }
        
        private void ResetFields()
        {
            Quantity.Text = "1";
            
            SizeSmall.IsChecked = true;
            SizeMedium.IsChecked = false;
            SizeLarge.IsChecked = false;

            foreach (CheckBox cb in ToppingStackPanel.Children)
            {
                cb.IsChecked = false;
            }
            
            UpdatePriceDisplay();
        }
        
        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            int currentQuantity = GetCurrentQuantity();
            currentQuantity++;
            Quantity.Text = currentQuantity.ToString();

            UpdatePriceDisplay();
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            int currentQuantity = GetCurrentQuantity();
            
            if (currentQuantity > 1)
            {
                currentQuantity--;
                Quantity.Text = currentQuantity.ToString();
                UpdatePriceDisplay();
            }
        }
        
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Pizza pizzaPage = new Pizza(_cartPage);
            OrderPage.Navigate(pizzaPage);
        }
        
        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            string product = ProductName.Content.ToString()!;
            string size = GetSelectedSize();
            List<string> toppings = GetSelectedToppings();
            int quantity = int.Parse(Quantity.Text);
            double price = GetCurrentPrice();

            CartItem cartItem = new CartItem(product, size, toppings, quantity, price);
            
            bool? addToCart = CustomMessageBox.Show($"Add {quantity} x {product} to cart for ${price:F2}?");
            
            if (addToCart == true)
            {
                _cartPage.AddToCart(cartItem);
            }
            
            ResetFields();
        }
        
        private void OrderNowButton_Click(object sender, RoutedEventArgs e)
        {
            double currentPrice = GetCurrentPrice();
            PizzaInfo currentPizzaInfo = GetCurrentPizzaInfo(); 
            Delivery deliveryPage = new Delivery(currentPrice, currentPizzaInfo, this, _cartPage); 
    
            OrderPage.Navigate(deliveryPage);
        }
    }
}
