using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace EFM.Tasks
{
    public partial class Solution1Window : Window
    {
        private readonly List<TelephelyAdat> _normalizaltAdatok;

        public Solution1Window(List<TelephelyAdat> normalizaltAdatok)
        {
            InitializeComponent();
            _normalizaltAdatok = normalizaltAdatok;
            NormalizaltDataGrid.ItemsSource = _normalizaltAdatok;

            MegjelenitLevezetest();
        }

        private void MegjelenitLevezetest()
        {
            StringBuilder levezetes = new StringBuilder();
            levezetes.AppendLine("Behelyettesítve az additív modell formulájába/képletébe:");

            decimal[] pontszamok = new decimal[3];

            for (int telephely = 1; telephely <= 3; telephely++)
            {
                decimal osszpontszam = 0;

                levezetes.Append($"Az {telephely}. telephely összpontszáma: S{telephely} = ");

                for (int i = 0; i < _normalizaltAdatok.Count; i++)
                {
                    var adat = _normalizaltAdatok[i];
                    decimal ertek = telephely switch
                    {
                        1 => adat.Telephely1,
                        2 => adat.Telephely2,
                        3 => adat.Telephely3,
                        _ => 0
                    };

                    decimal sulyozottErtek = ertek * adat.Suly;
                    osszpontszam += sulyozottErtek;

                    levezetes.Append($"{adat.Suly}*{ertek}");
                    if (i < _normalizaltAdatok.Count - 1)
                        levezetes.Append(" + ");
                }

                levezetes.AppendLine($" = {osszpontszam}");
                pontszamok[telephely - 1] = osszpontszam;
            }

            int legjobbTelephely = pontszamok
                .Select((pontszam, index) => new { pontszam, index = index + 1 })
                .OrderByDescending(x => x.pontszam)
                .First().index;

            levezetes.AppendLine();
            levezetes.AppendLine($"Az additív modell alapján a(z) {legjobbTelephely}. telephely választását javasoljuk.");

            LevezetesTextBlock.Text = levezetes.ToString();
        }
    }
}
