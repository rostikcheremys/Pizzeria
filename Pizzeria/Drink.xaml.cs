using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pizzeria
{
    public partial class Drink : Page
    {
        private readonly Cart _cartPage;
        public Drink(Cart cartPage)
        {
            _cartPage = cartPage;
            InitializeComponent();
           
            List<DrinkInfo> drinkDetails = DrinkData.Drinks;
            
            Image[] drinkImages = [CocaColaImage, SpriteImage, FantaImage, PepsiImage, MineralWaterImage, CoronaExtraImage];
            Label[] drinkNames = [CocaColaName, SpriteName, FantaName, PepsiName, MineralWaterName, CoronaExtraName];
            TextBlock[] drinkPrices = [CocaColaPrice, SpritePrice, FantaPrice, PepsiPrice, MineralWaterPrice, CoronaExtraPrice];
            TextBlock[] drinkIngredients = [CocaColaIngredients, SpriteIngredients, FantaIngredients, PepsiIngredients, MineralWaterIngredients, CoronaExtraIngredients];

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
            
            Pizza pizzaPage = new Pizza(_cartPage); 
            DrinkPage.Navigate(pizzaPage);
        }

        private void DrinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (DrinkPage.Content is Drink)
            {
                return;
            }
            
            Drink drinkPage = new Drink(_cartPage);
            DrinkPage.Navigate(drinkPage); 
        }
        
        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            Cart cartPage = new Cart(_cartPage);
            DrinkPage.Navigate(cartPage); 
        }
       
        private void OrderPizza_Click(object sender, RoutedEventArgs e)
        { 
            Button? clickedButton = sender as Button;
            
            if (clickedButton == CocaColaOrder)
            {
                NavigateToOrderPage(DrinkData.Drinks[0]);
            }
            else if (clickedButton == SpriteOrder)
            {
                NavigateToOrderPage(DrinkData.Drinks[1]);
            }
            else if (clickedButton == FantaOrder)
            {
                NavigateToOrderPage(DrinkData.Drinks[2]);
            }
            else if (clickedButton == PepsiOrder)
            {
                NavigateToOrderPage(DrinkData.Drinks[3]);
            }
            else if (clickedButton == MineralWaterOrder)
            {
                NavigateToOrderPage(DrinkData.Drinks[4]);
            }
            else if (clickedButton == CoronaExtraOrder)
            {
                NavigateToOrderPage(DrinkData.Drinks[5]);
            }
        
        }

        private void NavigateToOrderPage(DrinkInfo drinkInfo)
        {
            //Order orderPage = new Order(drinkInfo);
            //DrinkPage.Navigate(orderPage);
        }
    }
}

