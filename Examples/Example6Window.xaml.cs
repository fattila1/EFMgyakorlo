using System.Windows;

namespace EFM.Examples
{
    public partial class Example6Window : Window
    {
        public Example6Window()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
