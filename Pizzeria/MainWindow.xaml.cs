using System.Windows;
using System.Globalization;

namespace Pizzeria;

public partial class MainWindow
{
    private readonly Cart _cartPage = new ();
    private readonly Order _orderPage;
    public MainWindow()
    {
        InitializeComponent();
        
        MinWidth = MaxWidth = 1200;
        MinHeight = MaxHeight = 900;
        
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
    }
    
    private void MenuButton_Click(object sender, RoutedEventArgs e)
    {
        Pizza pizzaPage = new Pizza(_cartPage, _orderPage);
        MainPage.Navigate(pizzaPage);
    }
}


