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
            
            UpdateTotalPrice();
        }
        
        public void AddToCart(CartItem item)
        {
            bool existingPizza = false;
            
            foreach (var cartItem in _cartItems)
            {
                if (cartItem.Product == item.Product)
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

            TotalPrice.Text = $"Total Price: ${totalPrice}";
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Pizza pizzaPage = new Pizza(this);
            CartPage.Navigate(pizzaPage);
        }
        
        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
        
        }
    }
}