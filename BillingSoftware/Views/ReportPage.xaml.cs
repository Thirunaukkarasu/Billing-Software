using BillingSoftware.ViewModels;
using System.Windows.Controls;

namespace BillingSoftware.Views;

public partial class ReportPage : Page
{
    public ReportPage(ReportViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
