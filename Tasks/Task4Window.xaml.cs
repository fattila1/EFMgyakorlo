using System.Windows;

namespace EFM.Tasks
{
    public partial class Task4Window : Window
    {
        public Task4Window()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputTextBox.Text, out int input))
            {
                ResultTextBlock.Text = $"{input} x 4 = {input * 4}";
            }
            else
            {
                ResultTextBlock.Text = "Invalid input. Please enter a number.";
            }
        }
    }

}