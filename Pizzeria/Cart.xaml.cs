using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Pizzeria
{
    public partial class Cart 
    {
        private readonly ObservableCollection<CartItem> _cartItems = new();
        public ObservableCollection<CartItem> CartItems => _cartItems;

        public Cart()
        {
            InitializeComponent();
            DataContext = this;
            CardListView.ItemsSource = _cartItems;
        }

        public void AddToCart(CartItem item)
        {
            _cartItems.Add(item);
            MessageBox.Show($"Cart now has {_cartItems.Count} items.");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Pizza pizzaPage = new Pizza(this);
            CartPage.Navigate(pizzaPage);
        }
    }
}