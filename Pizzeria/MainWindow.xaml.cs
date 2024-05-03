using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pizzeria;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void MenuButton_Click(object sender, RoutedEventArgs e)
    {
        Menu menuPage = new Menu();
        MainPage.Navigate(menuPage);
    }
}

public class PizzaMenu
{
    public List<string> MenuItems { get; } = ["Margherita", "Pepperoni", "Vegetarian", "Hawaiian"];
}

