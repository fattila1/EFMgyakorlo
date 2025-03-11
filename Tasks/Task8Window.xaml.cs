using System.Windows;

namespace EFM.Tasks
{
    public partial class Task8Window : Window
    {
        public Task8Window()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputTextBox.Text, out int input))
            {
                ResultTextBlock.Text = $"{input} x 8 = {input * 8}";
            }
            else
            {
                ResultTextBlock.Text = "Invalid input. Please enter a number.";
            }
        }
    }
}
