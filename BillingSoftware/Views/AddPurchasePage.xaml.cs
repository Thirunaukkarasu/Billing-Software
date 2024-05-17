using System.Windows.Controls; 
using BillingSoftware.ViewModels;
namespace BillingSoftware.Views;

public partial class AddPurchasePage : Page
{
    public AddPurchasePage(AddPurchaseViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
