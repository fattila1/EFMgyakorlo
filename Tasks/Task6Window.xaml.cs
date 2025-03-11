using System.Windows;

namespace EFM.Tasks
{
    public partial class Task6Window : Window
    {
        public Task6Window()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputTextBox.Text, out int input))
            {
                ResultTextBlock.Text = $"{input} x 6 = {input * 6}";
            }
            else
            {
                ResultTextBlock.Text = "Invalid input. Please enter a number.";
            }
        }
    }
}