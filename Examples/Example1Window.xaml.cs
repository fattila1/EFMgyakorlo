using System.Collections.Generic;
using System.Windows;

namespace EFM.Examples
{
    public partial class Example1Window : Window
    {
        public Example1Window()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var adatok = new List<TelephelyAdat>
            {
                new TelephelyAdat { Szempont = "A telek mérete", Telephely1 = "2000 m2", Telephely2 = "2200 m2", Suly = "2" },
                new TelephelyAdat { Szempont = "A meglévő épületek alapterülete", Telephely1 = "400 m2", Telephely2 = "200 m2", Suly = "4" },
                new TelephelyAdat { Szempont = "A meglévő épületek állapota", Telephely1 = "közepes (6)", Telephely2 = "jó (8)", Suly = "4" },
                new TelephelyAdat { Szempont = "Távolság a belvárosi piactól", Telephely1 = "50 km", Telephely2 = "30 km", Suly = "2" },
                new TelephelyAdat { Szempont = "Az ingatlan ára", Telephely1 = "20 M Ft", Telephely2 = "25 M Ft", Suly = "5" },
                new TelephelyAdat { Szempont = "A telephely megközelíthetősége", Telephely1 = "kiváló (10)", Telephely2 = "jó (8)", Suly = "3" }
            };

            DataGrid1.ItemsSource = adatok;

            var adatok2 = new List<TelephelyAdat>
            {
                new TelephelyAdat { Szempont = "A telek mérete", Telephely1 = "9", Telephely2 = "10", Suly = "2" },
                new TelephelyAdat { Szempont = "A meglévő épületek alapterülete", Telephely1 = "10", Telephely2 = "5", Suly = "4" },
                new TelephelyAdat { Szempont = "A meglévő épületek állapota", Telephely1 = "6", Telephely2 = "8", Suly = "4" },
                new TelephelyAdat { Szempont = "Távolság a belvárosi piactól", Telephely1 = "6", Telephely2 = "10", Suly = "2" },
                new TelephelyAdat { Szempont = "Az ingatlan ára", Telephely1 = "10", Telephely2 = "8", Suly = "5" },
                new TelephelyAdat { Szempont = "A telephely megközelíthetősége", Telephely1 = "10", Telephely2 = "8", Suly = "3" }
            };

            DataGrid2.ItemsSource = adatok2;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class TelephelyAdat
    {
        public string Szempont { get; set; }
        public string Telephely1 { get; set; }
        public string Telephely2 { get; set; }
        public string Suly { get; set; }
    }
}


