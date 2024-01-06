using BillingSoftware.ViewModels;
using System.Windows.Controls;

namespace BillingSoftware.Views;

public partial class BackupPage : Page
{
    public BackupPage(BackupViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
