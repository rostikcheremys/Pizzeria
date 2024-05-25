using System.Windows;
using Pizzeria.PizzeriaInfo;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace Pizzeria
{
    public partial class Cart 
    {
        private readonly ObservableCollection<CartInfo> _cartInfo = new();

        public ObservableCollection<CartInfo> CartInfo => _cartInfo;
        private readonly Cart _previousCartPage = null!;
        private readonly Order _orderPage;
        private readonly string _isProductPage;
        
        
        public Cart()
        {
            InitializeComponent();
        }
        
        public Cart(Cart previousCartPage, string isProductPage, Order orderPage)
        {
            InitializeComponent();
    
            DataContext = this;
            _previousCartPage = previousCartPage;
            _isProductPage = isProductPage;
            _orderPage = orderPage;
            CardListView.ItemsSource = _cartInfo;
   
            foreach (CartInfo cartInfo in previousCartPage._cartInfo)
            {
                _cartInfo.Add(cartInfo);
            }
    
            UpdateTotalPrice();
        }

        
        public void AddToCart(CartInfo cartInfo)
        {
            bool existingItem = false;

            foreach (CartInfo cartItem in _cartInfo)
            {
                if (IsSameItem(cartItem, cartInfo))
                {
                    cartItem.Quantity += cartInfo.Quantity;
                    cartItem.Price += cartInfo.Price;

                    existingItem = true;
                    break;
                }
            }

            if (!existingItem)
            {
                _cartInfo.Add(cartInfo);
            }
        }

        private bool IsSameItem(CartInfo existingInfo, CartInfo newInfo)
        {
            if (_isProductPage == "Pizza")
            {
                return existingInfo.Product == newInfo.Product && existingInfo.Size == newInfo.Size && existingInfo.Toppings.SequenceEqual(newInfo.Toppings);
            }
            
            return existingInfo.Product == newInfo.Product && existingInfo.Size == newInfo.Size;
        }

        public void UpdateTotalPrice()
        {
            double totalPrice = 0;

            foreach (CartInfo cartInfo in _cartInfo)
            {
                totalPrice += cartInfo.Price;
            }

            Price.Text = $"Total Price: ${totalPrice:F2}";
        }
        
        private double GetTotalPrice()
        {
            double currentPrice = 0;

            foreach (CartInfo cartInfo in _cartInfo)
            {
                currentPrice += cartInfo.Price;
            }

            return currentPrice;
        }
        
        public void ClearCart()
        {
            _cartInfo.Clear();
            UpdateTotalPrice();
        }
        
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isProductPage == "Pizza")
            {
                Pizza pizzaPage = new Pizza(this, _orderPage);
                CartPage.Navigate(pizzaPage);
            }
            else
            {
                Drink drinkPage = new Drink(this, _orderPage);
                CartPage.Navigate(drinkPage);
            }
           
        }
        
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { Tag: CartInfo cartInfo })
            {
                _cartInfo.Remove(cartInfo);
                UpdateTotalPrice();
            }
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            double currentPrice = GetTotalPrice();

            if (currentPrice == 0)
            {
                CustomMessageBox.InfoShow("Order cannot be $0!");
            }
            else
            {
                Delivery deliveryPage = new Delivery(currentPrice, _isProductPage, _orderPage, _previousCartPage); 
                CartPage.Navigate(deliveryPage);
            }
        }
    }
}