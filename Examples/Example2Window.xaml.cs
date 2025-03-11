using System.Windows;

namespace EFM.Examples
{
    public partial class Example2Window : Window
    {
        public Example2Window()
        {
            InitializeComponent();
            BetoltMintafeladat();
        }

        private void BetoltMintafeladat()
        {
            var adatok = new List<ExampleTelephely>
            {
                new ExampleTelephely { Telephely = "A", X = 250, Y = 140, Suly = 1200 },
                new ExampleTelephely { Telephely = "B", X = 300, Y = 400, Suly = 1400 },
                new ExampleTelephely { Telephely = "C", X = 125, Y = 350, Suly = 2500 }
            };

            ExampleDataGrid.ItemsSource = adatok;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class ExampleTelephely
    {
        public string Telephely { get; set; } = string.Empty;
        public int X { get; set; }
        public int Y { get; set; }
        public int Suly { get; set; }
    }
}
