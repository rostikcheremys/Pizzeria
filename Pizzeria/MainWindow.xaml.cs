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
        PizzaMenu pizzaMenu = new PizzaMenu();
        DataContext = pizzaMenu;
    }

    private void MenuButton_Click(object sender, RoutedEventArgs e)
    {
        //MainWindowPage.Content = new Menu();
    }
}

public class PizzaMenu
{
    public List<string> MenuItems { get; }

    public PizzaMenu()
    {
        MenuItems = new List<string> { "Margherita", "Pepperoni", "Vegetarian", "Hawaiian" };
    }
}

public class Order
{
    private readonly List<string> _items;

    public Order(List<string> orderItems)
    {
        _items = orderItems;
    }

    public void DisplayOrder()
    {
        MessageBox.Show("Your order:\n" + string.Join("\n", _items));
    }
}
