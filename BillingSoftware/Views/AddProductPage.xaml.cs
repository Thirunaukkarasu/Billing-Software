using System.Windows.Controls;

using BillingSoftware.ViewModels;

namespace BillingSoftware.Views;

public partial class AddProductPage : Page
{
    public AddProductPage(AddProductViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
