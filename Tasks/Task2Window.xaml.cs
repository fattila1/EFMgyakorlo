using EFM.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace EFM.Tasks
{
    public partial class Task2Window : Window
    {
        private List<Task2Adat> _adatok;

        public Task2Window()
        {
            InitializeComponent();
            BetoltAdatok();
        }

        private void BetoltAdatok()
        {
            _adatok = DatabaseHelper.GetTask2Data();
            Task2DataGrid.ItemsSource = _adatok;
        }

        private void MegoldasButton_Click(object sender, RoutedEventArgs e)
        {
            var solutionWindow = new Solution2Window(_adatok);
            solutionWindow.ShowDialog();
        }


        private void EllenorzesButton_Click(object sender, RoutedEventArgs e)
        {
            decimal helyesCx = KiszamoltCx();
            decimal helyesCy = KiszamoltCy();

            decimal.TryParse(CxInput.Text, out decimal bekuldottCx);
            decimal.TryParse(CyInput.Text, out decimal bekuldottCy);

            CxInput.Background = bekuldottCx == helyesCx ? Brushes.LightGreen : Brushes.LightCoral;
            CyInput.Background = bekuldottCy == helyesCy ? Brushes.LightGreen : Brushes.LightCoral;
        }

        private decimal KiszamoltCx() => _adatok.Sum(a => a.X * a.Suly) / _adatok.Sum(a => a.Suly);
        private decimal KiszamoltCy() => _adatok.Sum(a => a.Y * a.Suly) / _adatok.Sum(a => a.Suly);
    }

   
}
