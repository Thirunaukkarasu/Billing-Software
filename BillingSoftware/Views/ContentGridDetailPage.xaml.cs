using BillingSoftware.ViewModels;
using System.Windows.Controls;

namespace BillingSoftware.Views;

public partial class ContentGridDetailPage : Page
{
    public ContentGridDetailPage(ContentGridDetailViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
