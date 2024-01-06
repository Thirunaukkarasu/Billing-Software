using BillingSoftware.ViewModels;
using System.Windows.Controls;

namespace BillingSoftware.Views;

public partial class DashboardPage : Page
{
    public DashboardPage(DashboardViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
