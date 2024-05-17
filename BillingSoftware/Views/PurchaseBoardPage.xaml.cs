using System.Windows.Controls;

using BillingSoftware.ViewModels;

namespace BillingSoftware.Views;

public partial class PurchaseBoardPage : Page
{
    public PurchaseBoardPage(PurchaseBoardViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
