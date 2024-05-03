using System.Windows;
using System.Windows.Controls;

namespace Pizzeria
{
    public partial class Order : Page
    {
        public Order(string name, string imagePath, string ingredients)
        {
            InitializeComponent();
            
            PizzaName.Content = name;
            PizzaImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(imagePath, UriKind.Relative));
            PizzaIngredients.Text = ingredients;
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Added to Cart");
        }

        private void OrderNow_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Order placed!");
        }
    }
}
