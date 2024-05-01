using System.Windows;
using System.Windows.Controls;

namespace Pizzeria;

public partial class Pizza : Page
{
    public Pizza()
    {
        InitializeComponent();
    }

    private void Image_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("werw");
    }
    
}