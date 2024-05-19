using System.Windows;

namespace Pizzeria
{
    public partial class CustomMessageBox 
    {
        public CustomMessageBox(string message)
        {
            InitializeComponent();
            
            MessageText.Text = message;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public static bool? Show(string message)
        {
            CustomMessageBox messageBox = new CustomMessageBox(message);
            return messageBox.ShowDialog();
        }
    }
}