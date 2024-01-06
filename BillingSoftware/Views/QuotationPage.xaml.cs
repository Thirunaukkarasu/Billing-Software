using BillingSoftware.ViewModels;
using System.Windows.Controls;

namespace BillingSoftware.Views;

public partial class QuotationPage : Page
{
    public QuotationPage(QuotationViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
