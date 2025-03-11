using System.Windows;

namespace EFM.Tasks
{
    public partial class Task7Window : Window
    {
        public Task7Window()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputTextBox.Text, out int input))
            {
                ResultTextBlock.Text = $"{input} x 7 = {input * 7}";
            }
            else
            {
                ResultTextBlock.Text = "Invalid input. Please enter a number.";
            }
        }
    }

}