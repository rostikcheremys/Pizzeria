﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pizzeria
{
    public partial class Order
    {
        private readonly double _basePrice;
        private readonly Cart _cartPage;

        public Order(PizzaInfo info, Cart cartPage)
        {
            InitializeComponent();
            PizzaInfo pizzaInfo = info;
            _cartPage = cartPage;
            
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
        
        private double GetCurrentPrice()
        {
            double.TryParse(PizzaPrice.Text.Replace("Price: $", ""), out var currentPrice);
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

        public void SetSelectedToppings(List<string?> toppings)
        {
            foreach (CheckBox cb in ToppingStackPanel.Children)
            {
                cb.IsChecked = toppings.Contains(cb.Content.ToString());
            }
        }
        
        public void SetCurrentPrice(double price)
        {
            PizzaPrice.Text = $"Price: ${price}";
        }
        
        public void SetSelectedQuantity(int quantity)
        {
            PizzaQuantity.Text = quantity.ToString();
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
            Pizza pizzaPage = new Pizza();
            OrderPage.Navigate(pizzaPage);
        }
        
        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            string? productName = PizzaName.Content.ToString();
            int quantity = int.Parse(PizzaQuantity.Text);
            double price = GetCurrentPrice();
    
            CartItemInfo cartItem = new CartItemInfo(productName, quantity, price);
            _cartPage.AddToCart(cartItem);
  
            ResetFields();
        }
        
        private void ResetFields()
        {
            PizzaQuantity.Text = "1";
            
            SizeSmall.IsChecked = true;
            SizeMedium.IsChecked = false;
            SizeLarge.IsChecked = false;

            foreach (CheckBox cb in ToppingStackPanel.Children)
            {
                cb.IsChecked = false;
            }
            
            UpdatePriceDisplay();
        }
        
        private void OrderNowButton_Click(object sender, RoutedEventArgs e)
        {
            double currentOrderPrice = GetCurrentPrice();
            PizzaInfo pizzaInfo = GetCurrentPizzaInfo(); 
            Delivery deliveryPage = new Delivery(currentOrderPrice, pizzaInfo, this, _cartPage); 
    
            OrderPage.Navigate(deliveryPage);
        }
        
        private PizzaInfo GetCurrentPizzaInfo()
        {
            PizzaInfo pizzaInfo = new PizzaInfo(
                PizzaName.Content.ToString(),          
                ((BitmapImage)PizzaImage.Source).UriSource.ToString(),
                PizzaIngredients.Text,                    
                _basePrice                               
            );

            return pizzaInfo;
        }
    }
    public class CartItemInfo(string? product, int quantity, double price)
    {
        public string? Product { get; set; } = product;
        public int Quantity { get; set; } = quantity;
        public double Price { get; set; } = price;
    }
}
