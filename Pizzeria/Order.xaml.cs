using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pizzeria
{
    public partial class Order : Page
    {
        private double pizzaPrice;

        public Order(string name, string imagePath, string ingredients, double price)
        {
            InitializeComponent();
            
            PizzaName.Content = name;
            PizzaImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            PizzaIngredients.Text = ingredients;
            pizzaPrice = price;

            UpdatePriceDisplay();
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

        private void UpdatePriceDisplay()
        {
            int quantity = GetCurrentQuantity();
            double total = pizzaPrice * quantity;

            PizzaPrice.Text = $"Total Price: ${total}";
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            int quantity = GetCurrentQuantity();
            MessageBox.Show($"Added {quantity} to Cart");
        }

        private void OrderNow_Click(object sender, RoutedEventArgs e)
        {
            int quantity = GetCurrentQuantity();
            MessageBox.Show($"Order placed for {quantity}");
        }
    }
}