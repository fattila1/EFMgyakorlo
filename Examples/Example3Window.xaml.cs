using System.Windows;

namespace EFM.Examples
{
    public partial class Example3Window : Window
    {
        public Example3Window()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
