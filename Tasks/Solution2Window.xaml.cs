using EFM.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace EFM.Tasks
{
    public partial class Solution2Window : Window
    {
        public Solution2Window(List<Task2Adat> adatok)
        {
            InitializeComponent();
            SolutionDataGrid.ItemsSource = adatok;
            MegjelenitLevezetest(adatok);
        }

        private void MegjelenitLevezetest(List<Task2Adat> adatok)
        {
            var sumWeight = adatok.Sum(a => a.Suly);
            decimal cx = adatok.Sum(a => a.X * a.Suly) / sumWeight;
            decimal cy = adatok.Sum(a => a.Y * a.Suly) / sumWeight;

            var sb = new StringBuilder();
            sb.AppendLine("Behelyettesítve a formulába:");
            sb.AppendLine($"Cx = ({string.Join(" + ", adatok.Select(a => $"{a.X}*{a.Suly}"))}) / {sumWeight} = {cx:F2}");
            sb.AppendLine($"Cy = ({string.Join(" + ", adatok.Select(a => $"{a.Y}*{a.Suly}"))}) / {sumWeight} = {cy:F2}");
            sb.AppendLine($"Javasolt telephely koordinátái: [{cx:F2}, {cy:F2}]");

            LevezetesText.Text = sb.ToString();
        }
    }
}
