using BillingSoftware.Models;
using System.Windows.Controls;

namespace BillingSoftware.PrintTemplates
{
    /// <summary>
    /// Interaction logic for SampleTemplate.xaml
    /// </summary>
    public partial class PrintSummary : UserControl
    {
        public PrintSummary()
        {
            InitializeComponent();

            for (var i = 1; i <= 100; i++)
            {
                ItemsControl2.Items.Add(new ReportDataModel(i, $"Part of list xxxxxxxxx 2","", "Part HSN", 10*i, 10000*i, 1000, 100000*i ));
            }
        }
    }
}
