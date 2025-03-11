using System.Windows;

namespace EFM.Examples
{
    public partial class Example7Window : Window
    {
        public Example7Window()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
