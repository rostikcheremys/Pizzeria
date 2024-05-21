using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace Pizzeria
{
    public partial class Cart 
    {
        private readonly ObservableCollection<CartItem> _cartItems = new();
       
        private ObservableCollection<CartItem> CartItems => _cartItems;

        private readonly Cart _previousCartPage = null!;
        
        public Cart()
        {
            InitializeComponent();
        }
        
        public Cart(Cart previousCartPage)
        {
            InitializeComponent();
            
            DataContext = this;
            _previousCartPage = previousCartPage;
            CardListView.ItemsSource = _cartItems;
           
            foreach (var item in previousCartPage.CartItems)
            {
                _cartItems.Add(item);
            }
            
            UpdateTotalPrice();
        }
        
        public void AddToCart(CartItem item)
        {
            bool existingPizza = false;
    
            foreach (var cartItem in _cartItems)
            {
                if (cartItem.Product == item.Product && cartItem.Size == item.Size && cartItem.Toppings.SequenceEqual(item.Toppings))
                {
                    cartItem.Quantity += item.Quantity;
                    cartItem.Price += item.Price;
            
                    existingPizza = true;
                    break;
                }
            }
    
            if (!existingPizza)
            {
                _cartItems.Add(item);
            }
        }
        
        private void UpdateTotalPrice()
        {
            double totalPrice = 0;

            foreach (CartItem cartItem in _cartItems)
            {
                totalPrice += cartItem.Price;
            }

            Price.Text = $"Total Price: ${totalPrice:F2}";
        }
        
        private double GetTotalPrice()
        {
            double currentPrice = 0;

            foreach (CartItem cartItem in _cartItems)
            {
                currentPrice += cartItem.Price;
            }

            return currentPrice;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Pizza pizzaPage = new Pizza(this);
            CartPage.Navigate(pizzaPage);
        }
        
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { Tag: CartItem cartItem })
            {
                _cartItems.Remove(cartItem);
                UpdateTotalPrice();
            }
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            double currentPrice = GetTotalPrice();
            
            Delivery deliveryPage = new Delivery(currentPrice, _previousCartPage); 
    
            CartPage.Navigate(deliveryPage);
        }
    }
}