using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Pizzeria
{
    public partial class Cart
    {
        private readonly ObservableCollection<CartItem> _cartItems = new();
        
        public Cart()
        {
            InitializeComponent();
            CardListView.ItemsSource = _cartItems;
            //CartItem car = new CartItem("car", 1, 100);
            //AddToCart(car);
        }
        
        public void AddToCart(CartItem item)
        {
            _cartItems.Add(item);
        }
        
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Pizza pizzaPage = new Pizza();
            CartPage.Navigate(pizzaPage);
        }
    }
}