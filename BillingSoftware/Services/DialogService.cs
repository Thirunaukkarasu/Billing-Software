using BillingSoftware.Contracts.Services;
using BillingSoftware.Views; 

namespace BillingSoftware.Services
{
    public class DialogService : IDialogService
    {
        public void ShowDialog(object viewModel)
        {
            var win = new CategoryDialog();
            win.Content = viewModel;
            win.ShowDialog();
        }
    }
}
