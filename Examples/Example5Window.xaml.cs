using System.Windows;

namespace EFM.Examples
{
    public partial class Example5Window : Window
    {
        public Example5Window()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
