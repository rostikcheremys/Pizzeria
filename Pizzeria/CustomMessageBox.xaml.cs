using System.Windows;

namespace Pizzeria
{
    public partial class CustomMessageBox 
    {
        public CustomMessageBox(string message)
        {
            InitializeComponent();
            MessageText.Text = message;
            MinWidth = MaxWidth = 450;
            MinHeight = MaxHeight = 200;
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

        public static bool Show(string message)
        {
            CustomMessageBox messageBox = new CustomMessageBox(message);
            return (bool)messageBox.ShowDialog()!;
        }
        
        public static void InfoShow(string message)
        {
            CustomMessageBox messageBox = new CustomMessageBox(message)
            {
                NoButton =
                {
                    Visibility = Visibility.Collapsed
                },
                YesButton =
                {
                    Content = "OK"
                }
            };

            messageBox.ShowDialog();
        }
    }
}