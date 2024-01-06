using BillingSoftware.ViewModels;
using System.Windows.Controls;

namespace BillingSoftware.Views;

public partial class SalesPage : Page
{
    public SalesPage(SalesViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
