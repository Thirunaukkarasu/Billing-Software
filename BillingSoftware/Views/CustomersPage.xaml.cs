using BillingSoftware.ViewModels;
using System.Windows.Controls;

namespace BillingSoftware.Views;

public partial class CustomersPage : Page
{
    public CustomersPage(CustomersViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
