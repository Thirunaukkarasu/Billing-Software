using BillingSoftware.ViewModels;
using System.Windows.Controls;

namespace BillingSoftware.Views;

public partial class PurchasePage : Page
{
    public PurchasePage(PurchaseViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
 
}
