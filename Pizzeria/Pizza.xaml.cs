using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pizzeria
{
    public static class PizzaData
    {
        public static List<PizzaDetails> AllPizzas = new List<PizzaDetails>
        {
            new PizzaDetails("Pepperoni", "images/pizza/pepperoni.png", "Pepperoni, Chili pepper, Cheese, Tomato Sauce", 15),
            new PizzaDetails("Sicilian", "images/pizza/sicilian.png", "Pepperoni, Olives, Tomato, Pepper, Cheese", 12),
            new PizzaDetails("Hawaiian", "images/pizza/hawaiian.png", "Mozzarella, Chicken, Pineapple, Tomato Sauce", 11),
            new PizzaDetails("Four Cheese", "images/pizza/four-cheese.png", "Mozzarella, Parmesan, Feta, Gorgonzola", 15),
            new PizzaDetails("Prosciutto e Funghi", "images/pizza/prosciutto-e-funghi.png", "Ham, Mushrooms, Cheese, Tomato Sauce", 13),
            new PizzaDetails("Chicken with Mushrooms", "images/pizza/сhicken-with-mushrooms.png", "Chicken, Mushrooms, Tomato, Cheese", 12)
        };
    }
    public partial class Pizza : Page
    {
        public Pizza()
        {
            InitializeComponent();

            List<PizzaDetails> pizzaDetails = PizzaData.AllPizzas;
            
            Image[] pizzaImages = { PizzaImage1, PizzaImage2, PizzaImage3, PizzaImage4, PizzaImage5, PizzaImage6 };
            Label[] pizzaNames = { PizzaName1, PizzaName2, PizzaName3, PizzaName4, PizzaName5, PizzaName6 };
            TextBlock[] pizzaPrices = { PizzaPrice1, PizzaPrice2, PizzaPrice3, PizzaPrice4, PizzaPrice5, PizzaPrice6 };
            TextBlock[] pizzaIngredients = { PizzaIngredients1, PizzaIngredients2, PizzaIngredients3, PizzaIngredients4, PizzaIngredients5, PizzaIngredients6 };

            for (int i = 0; i < pizzaDetails.Count; i++)
            {
                pizzaImages[i].Source = new BitmapImage(new Uri(pizzaDetails[i].ImagePath, UriKind.Relative));
                pizzaNames[i].Content = pizzaDetails[i].Name;
                pizzaPrices[i].Text = $"${pizzaDetails[i].Price}";
                pizzaIngredients[i].Text = pizzaDetails[i].Ingredients;
            }
        }
        
        private void OrderPizza_Click(object sender, RoutedEventArgs e)
        {
            Button? clickedButton = sender as Button;

            if (clickedButton == OrderPizzaButton1)
            {
                NavigateToOrderPage(PizzaData.AllPizzas[0]);
            }
            else if (clickedButton == OrderPizzaButton2)
            {
                NavigateToOrderPage(PizzaData.AllPizzas[1]);
            }
            else if (clickedButton == OrderPizzaButton3)
            {
                NavigateToOrderPage(PizzaData.AllPizzas[2]);
            }
            else if (clickedButton == OrderPizzaButton4)
            {
                NavigateToOrderPage(PizzaData.AllPizzas[3]);
            }
            else if (clickedButton == OrderPizzaButton5)
            {
                NavigateToOrderPage(PizzaData.AllPizzas[4]);
            }
            else if (clickedButton == OrderPizzaButton6)
            {
                NavigateToOrderPage(PizzaData.AllPizzas[5]);
            }
        }

        private void NavigateToOrderPage(PizzaDetails pizzaDetails)
        {
            Order orderPage = new Order(pizzaDetails);
            PizzaPage.Navigate(orderPage);
        }
    }
}
