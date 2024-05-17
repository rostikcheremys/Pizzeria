using System.Windows;

namespace Pizzeria
{
    public partial class CustomMessageBox 
    {
        public CustomMessageBox(string message, string yesButtonText = "YES", string noButtonText = "NO")
        {
            InitializeComponent();
            
            MessageText.Text = message;
            YesButton.Content = yesButtonText;
            NoButton.Content = noButtonText;
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

        public static bool? Show(string message, string yesButtonText = "YES", string noButtonText = "NO")
        {
            CustomMessageBox msgBox = new CustomMessageBox(message, yesButtonText, noButtonText);
            return msgBox.ShowDialog();
        }
    }
}