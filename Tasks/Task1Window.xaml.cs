using EFM.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace EFM.Tasks
{
    public partial class Task1Window : Window
    {
        private string _username;

        public Task1Window(string username)
        {
            InitializeComponent();
            _username = username;
            BetoltAdatok();
        }

        private void EllenorzesButton_Click(object sender, RoutedEventArgs e)
        {
            decimal helyesPontszam1 = SzamoljPontszamot(1);
            decimal helyesPontszam2 = SzamoljPontszamot(2);
            decimal helyesPontszam3 = SzamoljPontszamot(3);

            decimal.TryParse(Pontszam1.Text, out decimal bekuldott1);
            decimal.TryParse(Pontszam2.Text, out decimal bekuldott2);
            decimal.TryParse(Pontszam3.Text, out decimal bekuldott3);
            int.TryParse(ValasztottTelephely.Text, out int bekuldottValasztas);

            bool mindenHelyes = true;

            Pontszam1.Background = bekuldott1 == helyesPontszam1 ? Brushes.LightGreen : Brushes.LightCoral;
            if (bekuldott1 != helyesPontszam1) mindenHelyes = false;

            Pontszam2.Background = bekuldott2 == helyesPontszam2 ? Brushes.LightGreen : Brushes.LightCoral;
            if (bekuldott2 != helyesPontszam2) mindenHelyes = false;

            Pontszam3.Background = bekuldott3 == helyesPontszam3 ? Brushes.LightGreen : Brushes.LightCoral;
            if (bekuldott3 != helyesPontszam3) mindenHelyes = false;

            int helyesValasztas = new[] { helyesPontszam1, helyesPontszam2, helyesPontszam3 }
                .Select((pont, index) => (pont, index + 1))
                .OrderByDescending(x => x.pont)
                .First().Item2;

            ValasztottTelephely.Background = bekuldottValasztas == helyesValasztas ? Brushes.LightGreen : Brushes.LightCoral;
            if (bekuldottValasztas != helyesValasztas) mindenHelyes = false;

            DatabaseHelper.UpdateUserStatistics(_username, mindenHelyes);
        }


        private List<Task1Record> _osszesRekord;
        private int _aktualisIndex = 0;

        public Task1Window()
        {
            InitializeComponent();
            BetoltAdatok();
        }

        private void BetoltAdatok()
        {
            _osszesRekord = DatabaseHelper.GetAllTask1Records();
            if (_osszesRekord.Any())
            {
                MegjelenitRekord(_osszesRekord[0]);
            }
            else
            {
                MessageBox.Show("Nincs elérhető adat a task1 táblában!");
            }
        }

        private List<TelephelyAdat> _normalizaltAdatok;

        private void MegjelenitRekord(Task1Record adat)
        {
            var eredetiAdatok = KonvertalListara(adat);
            DataGrid1.ItemsSource = eredetiAdatok;

            _normalizaltAdatok = Normalizal(eredetiAdatok);
        }

        private void MegoldasButton_Click(object sender, RoutedEventArgs e)
        {
            var solutionWindow = new Solution1Window(_normalizaltAdatok);
            solutionWindow.ShowDialog();
        }

     

        private decimal SzamoljPontszamot(int telephelyIndex)
        {
            return _normalizaltAdatok.Sum(adat =>
            {
                decimal ertek = telephelyIndex switch
                {
                    1 => adat.Telephely1,
                    2 => adat.Telephely2,
                    3 => adat.Telephely3,
                    _ => 0
                };
                return ertek * adat.Suly;
            });
        }

        private List<TelephelyAdat> KonvertalListara(Task1Record adat) => new()
        {
            new TelephelyAdat("A munkaerő elérhetősége", adat.MunkaeroElerhetoseg_Telephely1, adat.MunkaeroElerhetoseg_Telephely2, adat.MunkaeroElerhetoseg_Telephely3, adat.MunkaeroElerhetoseg_Suly),
            new TelephelyAdat("A munkaerő költségei", adat.MunkaeroKoltsegek_Telephely1, adat.MunkaeroKoltsegek_Telephely2, adat.MunkaeroKoltsegek_Telephely3, adat.MunkaeroKoltsegek_Suly),
            new TelephelyAdat("Az anyagbeszerzés költségei", adat.AnyagbeszerzesKoltsegek_Telephely1, adat.AnyagbeszerzesKoltsegek_Telephely2, adat.AnyagbeszerzesKoltsegek_Telephely3, adat.AnyagbeszerzesKoltsegek_Suly),
            new TelephelyAdat("Az építés költségei", adat.EpitesKoltsegek_Telephely1, adat.EpitesKoltsegek_Telephely2, adat.EpitesKoltsegek_Telephely3, adat.EpitesKoltsegek_Suly),
            new TelephelyAdat("A közlekedési infrastruktúra", adat.KozlekedesiInfrastruktura_Telephely1, adat.KozlekedesiInfrastruktura_Telephely2, adat.KozlekedesiInfrastruktura_Telephely3, adat.KozlekedesiInfrastruktura_Suly),
            new TelephelyAdat("Helyi adók", adat.HelyiAdok_Telephely1, adat.HelyiAdok_Telephely2, adat.HelyiAdok_Telephely3, adat.HelyiAdok_Suly),
            new TelephelyAdat("Igényelhető támogatás", adat.IgenyelhetoTamogatas_Telephely1, adat.IgenyelhetoTamogatas_Telephely2, adat.IgenyelhetoTamogatas_Telephely3, adat.IgenyelhetoTamogatas_Suly)
        };

        private List<TelephelyAdat> Normalizal(List<TelephelyAdat> adatok)
        {
            var normalizaltLista = new List<TelephelyAdat>();

            foreach (var adat in adatok)
            {
                bool kellNormalizalni = adat.Telephely1 > 10 || adat.Telephely2 > 10 || adat.Telephely3 > 10;

                if (!kellNormalizalni)
                {
                    // Ha nem kell normalizálni, marad az eredeti érték
                    normalizaltLista.Add(new TelephelyAdat(
                        adat.Szempont,
                        adat.Telephely1,
                        adat.Telephely2,
                        adat.Telephely3,
                        adat.Suly
                    ));
                }
                else
                {
                    // Ha kell normalizálni, akkor csak az adott soron belül kell max értéket számolni
                    decimal max = Math.Max(adat.Telephely1, Math.Max(adat.Telephely2, adat.Telephely3));

                    normalizaltLista.Add(new TelephelyAdat(
                        adat.Szempont,
                        Math.Round(adat.Telephely1 * 10 / max, 2),
                        Math.Round(adat.Telephely2 * 10 / max, 2),
                        Math.Round(adat.Telephely3 * 10 / max, 2),
                        adat.Suly
                    ));
                }
            }

            return normalizaltLista;
        }

        private void Elozo_Click(object sender, RoutedEventArgs e)
        {
            if (_aktualisIndex > 0)
            {
                _aktualisIndex--;
                MegjelenitRekord(_osszesRekord[_aktualisIndex]);
            }
        }

        private void Kovetkezo_Click(object sender, RoutedEventArgs e)
        {
            if (_aktualisIndex < _osszesRekord.Count - 1)
            {
                _aktualisIndex++;
                MegjelenitRekord(_osszesRekord[_aktualisIndex]);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class TelephelyAdat
    {
        public TelephelyAdat(string szempont, decimal telephely1, decimal telephely2, decimal telephely3, decimal suly)
        {
            Szempont = szempont;
            Telephely1 = telephely1;
            Telephely2 = telephely2;
            Telephely3 = telephely3;
            Suly = suly;
        }

        public string Szempont { get; set; }
        public decimal Telephely1 { get; set; }
        public decimal Telephely2 { get; set; }
        public decimal Telephely3 { get; set; }
        public decimal Suly { get; set; }
    }


}
