using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pizzeria
{
    public partial class Drink : Page
    {
        public Drink()
        {
            InitializeComponent();
           
            List<DrinkInfo> drinkDetails = DrinkData.Drinks;
            
            Image[] drinkImages = [PepperoniImage, SicilianImage, HawaiianImage, FourCheeseImage, ProsciuttoEFunghiImage, ChickenWithMushroomsImage];
            Label[] drinkNames = [PepperoniName, SicilianName, HawaiianName, FourCheeseName, ProsciuttoEFunghiName, ChickenWithMushroomsName];
            TextBlock[] drinkPrices = [PepperoniPrice, SicilianPrice, HawaiianPrice, FourCheesePrice, ProsciuttoEFunghiPrice, ChickenWithMushroomsPrice];
            TextBlock[] drinkIngredients = [PepperoniIngredients, SicilianIngredients, HawaiianIngredients, FourCheeseIngredients, ProsciuttoEFunghiIngredients, ChickenWithMushroomsIngredients];

            for (int i = 0; i < drinkDetails.Count; i++)
            {
                drinkImages[i].Source = new BitmapImage(new Uri(drinkDetails[i].ImagePath, UriKind.Relative));
                drinkNames[i].Content = drinkDetails[i].Name;
                drinkPrices[i].Text = $"${drinkDetails[i].Price}";
                drinkIngredients[i].Text = drinkDetails[i].Description;
            }
        }
        
        private void PizzaButton_Click(object sender, RoutedEventArgs e)
        {
            if (DrinkPage.Content is Pizza)
            {
                return;
            }
            
            Pizza pizzaPage = new Pizza(); 
            DrinkPage.Navigate(pizzaPage);
        }

        private void DrinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (DrinkPage.Content is Drink)
            {
                return;
            }
            
            Drink drinkPage = new Drink();
            DrinkPage.Navigate(drinkPage); 
        }
        
        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            Cart cartPage = new Cart();
            DrinkPage.Navigate(cartPage); 
        }
       
        private void OrderPizza_Click(object sender, RoutedEventArgs e)
        {
            /*Button? clickedButton = sender as Button;

           if (clickedButton == PepperoniOrder)
           {
               NavigateToOrderPage(DrinkData.Drinks[0]);
           }
           else if (clickedButton == SicilianOrder)
           {
               NavigateToOrderPage(DrinkData.Drinks[1]);
           }
           else if (clickedButton == HawaiianOrder)
           {
               NavigateToOrderPage(DrinkData.Drinks[2]);
           }
           else if (clickedButton == FourCheeseOrder)
           {
               NavigateToOrderPage(DrinkData.Drinks[3]);
           }
           else if (clickedButton == ProsciuttoEFunghiOrder)
           {
               NavigateToOrderPage(DrinkData.Drinks[4]);
           }
           else if (clickedButton == ChickenWithMushroomsOrder)
           {
               NavigateToOrderPage(DrinkData.Drinks[5]);
           }*/
       }

        private void NavigateToOrderPage(PizzaInfo drinkInfo)
        {
            //Order orderPage = new Order(drinkInfo);
            //DrinkPage.Navigate(orderPage);
        }
    }
}

