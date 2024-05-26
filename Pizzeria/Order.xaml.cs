using System.Windows;
using Pizzeria.PizzeriaInfo;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pizzeria
{
    public partial class Order
    {
        private readonly MenuInfo _menuInfo;
        private readonly Cart _cartPage;
        private readonly double _price;
        private readonly string _isProductPage;

        public Order(MenuInfo menuInfo, Cart cartPage, string isProductPage)
        {
            InitializeComponent();

            _menuInfo = menuInfo;
            _cartPage = cartPage;
            _isProductPage = isProductPage;

            ProductName.Content = menuInfo.ProductName;
            ProductImage.Source = new BitmapImage(new Uri(menuInfo.ImagePath, UriKind.Relative));
            ProductDescription.Text = menuInfo.GetDescription();
            _price = menuInfo.Price;
            
            SizeSmall.Checked += Size_Checked;
            SizeMedium.Checked += Size_Checked;
            SizeLarge.Checked += Size_Checked;
            
            if (isProductPage == "Pizza")
            {
                foreach (CheckBox cb in ToppingStackPanel.Children)
                {
                    cb.Checked += ToppingChanged;
                    cb.Unchecked += ToppingChanged;
                }
            }
            else
            {
                SizeSmall.Content = "Small 250ml";
                SizeMedium.Content = "Medium 400ml";
                SizeLarge.Content = "Large 500ml";
                LabelAddToppings.Visibility = Visibility.Collapsed;
                ToppingStackPanel.Children.Clear();
            }
            
            UpdatePriceDisplay();
        }
        
        private void Size_Checked(object sender, RoutedEventArgs e)
        {
            UpdatePriceDisplay();
        }

        private void ToppingChanged(object sender, RoutedEventArgs e)
        {
            UpdatePriceDisplay();
        }
        
        private void UpdatePriceDisplay()
        {
            int quantity = GetCurrentQuantity();
            double total = (GetPrice() + GetToppingsPrice()) * quantity;

            ProductPrice.Text = $"Price: ${total:F2}";
        }

        private double GetPrice()
        {
            double multiplier = 1.0;
            
            if (SizeMedium.IsChecked == true)  multiplier = 1.5;
           
            else if (SizeLarge.IsChecked == true)  multiplier = 2.0;
            
            return _price * multiplier;
        }

        public int GetCurrentQuantity()
        {
            return int.Parse(Quantity.Text);
        }
        
        public void SetSelectedQuantity(int quantity)
        {
            Quantity.Text = quantity.ToString();
        }
       
        private double GetCurrentPrice()
        {
            double.TryParse(ProductPrice.Text.Replace("Price: $", ""), out double currentPrice);
            return currentPrice;
        }
        
        public void SetCurrentPrice(double price)
        {
            ProductPrice.Text = $"Price: ${price:F2}";
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

        public string GetSelectedSize()
        {
            if (SizeSmall.IsChecked == true)  return "Small";
           
            if (SizeMedium.IsChecked == true)  return "Medium";
            
            if (SizeLarge.IsChecked == true)  return "Large";
            
            return null!;
        }
        
        public void SetSelectedSize(string size)
        {
            if (size == "Small") SizeSmall.IsChecked = true;
            
            else if (size == "Medium") SizeMedium.IsChecked = true;
            
            else if (size == "Large")  SizeLarge.IsChecked = true;
        }

        public List<string> GetSelectedToppings()
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

        public void SetSelectedToppings(List<string> toppings)
        {
            foreach (CheckBox cb in ToppingStackPanel.Children)
            {
                cb.IsChecked = toppings.Contains(cb.Content.ToString());
            }
        }
        
        private PizzaInfo GetCurrentPizzaInfo()
        {
            PizzaInfo pizzaInfo = new PizzaInfo(
                ProductName.Content.ToString()!,          
                ((BitmapImage)ProductImage.Source).UriSource.ToString(),
                ProductDescription.Text,                    
                _price                               
            );

            return pizzaInfo;
        }

        private DrinkInfo GetCurrentDrinkInfo()
        {
            DrinkInfo drinkInfo = new DrinkInfo(
                ProductName.Content.ToString()!,          
                ((BitmapImage)ProductImage.Source).UriSource.ToString(),
                ProductDescription.Text,                    
                _price                               
            );

            return drinkInfo;
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
            if (_isProductPage == "Pizza")
            {
                Pizza pizzaPage = new Pizza(_cartPage, this);
                OrderPage.Navigate(pizzaPage);
            }
            else
            {   
                Drink drinkPage = new Drink(_cartPage, this);
                OrderPage.Navigate(drinkPage);
            }
        }
        
        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            string product = ProductName.Content.ToString()!;
            string size = GetSelectedSize();
            int quantity = int.Parse(Quantity.Text);
            double price = GetCurrentPrice();

            CartInfo cartInfo = CreateCartInfo(product, size, quantity, price);

            bool? addToCart = CustomMessageBox.Show($"Add {quantity} x {product} to cart for ${price:F2}?");

            if (addToCart == true)
            {
                _cartPage.AddToCart(cartInfo);
            }
            else
            {
                return;
            }

            ResetFields();
        }

        private CartInfo CreateCartInfo(string product, string size, int quantity, double price)
        {
            if (_isProductPage == "Pizza")
            {
                List<string> toppings = GetSelectedToppings();
                return new CartInfo(product, size, toppings, quantity, price);
            }

            return new CartInfo(product, size, quantity, price);
        }

        private void OrderNowButton_Click(object sender, RoutedEventArgs e)
        {
            double currentPrice = GetCurrentPrice();

            if (_isProductPage == "Pizza")
            {
                PizzaInfo currentPizzaInfo = GetCurrentPizzaInfo();
                Delivery deliveryPage = new Delivery(currentPrice, _isProductPage, currentPizzaInfo, this, _cartPage);
                OrderPage.Navigate(deliveryPage);
            }
            else
            {
                DrinkInfo currentDrinkInfo = GetCurrentDrinkInfo();
                Delivery deliveryPage = new Delivery(currentPrice, _isProductPage, currentDrinkInfo, this, _cartPage);
                OrderPage.Navigate(deliveryPage);
            }
        }
    }
}
