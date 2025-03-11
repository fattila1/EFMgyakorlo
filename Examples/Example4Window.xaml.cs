using System.Windows;

namespace EFM.Examples
{
    public partial class Example4Window : Window
    {
        public Example4Window()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
