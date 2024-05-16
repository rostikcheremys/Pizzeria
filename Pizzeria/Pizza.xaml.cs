using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pizzeria
{
    public partial class Pizza
    {
        private Cart _cartPage;
        public Pizza()
        {
            InitializeComponent();
            _cartPage = new Cart();

            List<PizzaInfo> pizzaDetails = PizzaData.Pizza;
            
            Image[] pizzaImages = [PepperoniImage, SicilianImage, HawaiianImage, FourCheeseImage, ProsciuttoEFunghiImage, ChickenWithMushroomsImage];
            Label[] pizzaNames = [PepperoniName, SicilianName, HawaiianName, FourCheeseName, ProsciuttoEFunghiName, ChickenWithMushroomsName];
            TextBlock[] pizzaPrices = [PepperoniPrice, SicilianPrice, HawaiianPrice, FourCheesePrice, ProsciuttoEFunghiPrice, ChickenWithMushroomsPrice];
            TextBlock[] pizzaIngredients = [PepperoniIngredients, SicilianIngredients, HawaiianIngredients, FourCheeseIngredients, ProsciuttoEFunghiIngredients, ChickenWithMushroomsIngredients];

            for (int i = 0; i < pizzaDetails.Count; i++)
            {
                pizzaImages[i].Source = new BitmapImage(new Uri(pizzaDetails[i].ImagePath, UriKind.Relative));
                pizzaNames[i].Content = pizzaDetails[i].Name;
                pizzaPrices[i].Text = $"${pizzaDetails[i].Price}";
                pizzaIngredients[i].Text = pizzaDetails[i].Ingredients;
            }
        }
        
        private void PizzaButton_Click(object sender, RoutedEventArgs e)
        {
            if (PizzaPage.Content is Pizza)
            {
                return;
            }
            
            Pizza pizzaPage = new Pizza(); 
            PizzaPage.Navigate(pizzaPage);
        }

        private void DrinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (PizzaPage.Content is Drink)
            {
                return;
            }
            
            Drink drinkPage = new Drink();
            PizzaPage.Navigate(drinkPage); 
        }
        
        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            Cart cartPage = new Cart();
            PizzaPage.Navigate(cartPage); 
        }
        
        private void OrderPizza_Click(object sender, RoutedEventArgs e)
        {
            Button? clickedButton = sender as Button;

            if (clickedButton == PepperoniOrder)
            {
                NavigateToOrderPage(PizzaData.Pizza[0]);
            }
            else if (clickedButton == SicilianOrder)
            {
                NavigateToOrderPage(PizzaData.Pizza[1]);
            }
            else if (clickedButton == HawaiianOrder)
            {
                NavigateToOrderPage(PizzaData.Pizza[2]);
            }
            else if (clickedButton == FourCheeseOrder)
            {
                NavigateToOrderPage(PizzaData.Pizza[3]);
            }
            else if (clickedButton == ProsciuttoEFunghiOrder)
            {
                NavigateToOrderPage(PizzaData.Pizza[4]);
            }
            else if (clickedButton == ChickenWithMushroomsOrder)
            {
                NavigateToOrderPage(PizzaData.Pizza[5]);
            }
        }

        private void NavigateToOrderPage(PizzaInfo pizzaInfo)
        {
            Order orderPage = new Order(pizzaInfo, _cartPage);
            PizzaPage.Navigate(orderPage);
        }
    }
}
