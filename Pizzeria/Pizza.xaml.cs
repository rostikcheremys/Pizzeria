using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pizzeria
{
    public partial class Pizza
    {
        private readonly Cart _cartPage;
        
        public Pizza(Cart cartPage)
        {
            _cartPage = cartPage;
            InitializeComponent();
           
            List<PizzaInfo> pizzaDetails = PizzaData.Pizzas;
            
            Image[] pizzaImages = [PepperoniImage, SicilianImage, HawaiianImage, FourCheeseImage, ProsciuttoEFunghiImage, ChickenWithMushroomsImage];
            Label[] pizzaNames = [PepperoniName, SicilianName, HawaiianName, FourCheeseName, ProsciuttoEFunghiName, ChickenWithMushroomsName];
            TextBlock[] pizzaPrices = [PepperoniPrice, SicilianPrice, HawaiianPrice, FourCheesePrice, ProsciuttoEFunghiPrice, ChickenWithMushroomsPrice];
            TextBlock[] pizzaIngredients = [PepperoniIngredients, SicilianIngredients, HawaiianIngredients, FourCheeseIngredients, ProsciuttoEFunghiIngredients, ChickenWithMushroomsIngredients];

            for (int i = 0; i < pizzaDetails.Count; i++)
            {
                pizzaImages[i].Source = new BitmapImage(new Uri(pizzaDetails[i].ImagePath, UriKind.Relative));
                pizzaNames[i].Content = pizzaDetails[i].Name;
                pizzaPrices[i].Text = $"${pizzaDetails[i].Price:F2}";
                pizzaIngredients[i].Text = pizzaDetails[i].Ingredients;
            }
        }
        
        private void PizzaButton_Click(object sender, RoutedEventArgs e)
        {
            if (PizzaPage.Content is Pizza) return;
            
            Pizza pizzaPage = new Pizza(_cartPage); 
            PizzaPage.Navigate(pizzaPage);
        }

        private void DrinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (PizzaPage.Content is Drink) return;
            
            Drink drinkPage = new Drink(_cartPage);
            PizzaPage.Navigate(drinkPage); 
        }
        
        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            Cart cartPage = new Cart(_cartPage);
            PizzaPage.Navigate(cartPage); 
        }
        
        private void OrderPizza_Click(object sender, RoutedEventArgs e)
        {
            Button? clickedButton = sender as Button;

            if (clickedButton == PepperoniOrder)
            {
                NavigateToOrderPage(PizzaData.Pizzas[0]);
            }
            else if (clickedButton == SicilianOrder)
            {
                NavigateToOrderPage(PizzaData.Pizzas[1]);
            }
            else if (clickedButton == HawaiianOrder)
            {
                NavigateToOrderPage(PizzaData.Pizzas[2]);
            }
            else if (clickedButton == FourCheeseOrder)
            {
                NavigateToOrderPage(PizzaData.Pizzas[3]);
            }
            else if (clickedButton == ProsciuttoEFunghiOrder)
            {
                NavigateToOrderPage(PizzaData.Pizzas[4]);
            }
            else if (clickedButton == ChickenWithMushroomsOrder)
            {
                NavigateToOrderPage(PizzaData.Pizzas[5]);
            }
        }

        private void NavigateToOrderPage(PizzaInfo pizzaInfo)
        {
            Order orderPage = new Order(pizzaInfo, _cartPage);
            PizzaPage.Navigate(orderPage);
        }
    }
}
