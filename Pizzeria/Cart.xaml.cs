using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;

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