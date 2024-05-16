using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Pizzeria
{
    public partial class Cart
    {
        private readonly ObservableCollection<CartItemInfo> _cartItems = new();
        
        public Cart()
        {
            InitializeComponent();
            CardListView.ItemsSource = _cartItems;
        } 
        public void AddToCart(CartItemInfo item)
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