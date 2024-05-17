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

public partial class MainWindow 
{
    public MainWindow()
    {
        InitializeComponent();
        
        MinWidth = MaxWidth = 1200;
        MinHeight = MaxHeight = 900;
    }
    
    private void MenuButton_Click(object sender, RoutedEventArgs e)
    {
        Pizza pizzaPage = new Pizza();
        MainPage.Navigate(pizzaPage);
    }
}


