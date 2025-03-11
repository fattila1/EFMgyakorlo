using System.Windows;

namespace EFM.Examples
{
    public partial class Example8Window : Window
    {
        public Example8Window()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
