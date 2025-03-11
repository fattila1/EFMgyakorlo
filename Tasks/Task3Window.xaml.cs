// Az egyes Task ablakok kódjai a Tasks mappában. Minden ablakhoz tartozik egy logikai számítás.

using System.Windows;

namespace EFM.Tasks
{
    
    public partial class Task3Window : Window
    {
        public Task3Window()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputTextBox.Text, out int input))
            {
                ResultTextBlock.Text = $"{input} x 3 = {input * 3}";
            }
            else
            {
                ResultTextBlock.Text = "Invalid input. Please enter a number.";
            }
        }
    }

}