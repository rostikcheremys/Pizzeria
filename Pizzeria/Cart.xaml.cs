﻿using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace Pizzeria
{
    public partial class Cart 
    {
        private readonly ObservableCollection<CartItem> _cartItems = new();

        public ObservableCollection<CartItem> CartItems => _cartItems;
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
            CardListView.ItemsSource = _cartItems;
   
            foreach (var item in previousCartPage.CartItems)
            {
                _cartItems.Add(item);
            }
    
            UpdateTotalPrice();
        }

        
        public void AddToCart(CartItem item)
        {
            bool existingItem = false;

            foreach (var cartItem in _cartItems)
            {
                if (IsSameItem(cartItem, item))
                {
                    cartItem.Quantity += item.Quantity;
                    cartItem.Price += item.Price;

                    existingItem = true;
                    break;
                }
            }

            if (!existingItem)
            {
                _cartItems.Add(item);
            }
        }

        private bool IsSameItem(CartItem existingItem, CartItem newItem)
        {
            if (_isProductPage == "Pizza")
            {
                return existingItem.Product == newItem.Product && existingItem.Size == newItem.Size && existingItem.Toppings.SequenceEqual(newItem.Toppings);
            }
            
            return existingItem.Product == newItem.Product && existingItem.Size == newItem.Size;
        }

        public void UpdateTotalPrice()
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
        
        public void ClearCart()
        {
            _cartItems.Clear();
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
            if (sender is Button { Tag: CartItem cartItem })
            {
                _cartItems.Remove(cartItem);
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