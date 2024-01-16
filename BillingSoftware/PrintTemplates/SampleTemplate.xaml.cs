using BillingSoftware.Models;
using System.Windows.Controls;

namespace BillingSoftware.PrintTemplates
{
    /// <summary>
    /// Interaction logic for SampleTemplate.xaml
    /// </summary>
    public partial class SampleTemplate : UserControl
    {
        public SampleTemplate()
        {
            InitializeComponent();

            for (var i = 1; i <= 30; i++)
            {
                ItemsControl1.Items.Add(new ReportDataModel(i, $"Col {i}", "Part of list 1"));
            }

            for (var i = 1; i <= 100; i++)
            {
                ItemsControl2.Items.Add(new ReportDataModel(i, $"Row {i}", "Part of list 2"));
            }
        }
    }
}
