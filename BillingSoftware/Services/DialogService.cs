using BillingSoftware.Contracts.Services;
using BillingSoftware.Views;
using System.Windows;

namespace BillingSoftware.Services
{
    public class DialogService : IDialogService  
    {
        private Window _currentWindow;

        // Method to show a dialog for any window type
        public void ShowDialog<TWindow>(object viewModel) where TWindow : Window, new()
        {
            _currentWindow = new TWindow();
            _currentWindow.Content = viewModel;  // Set the DataContext to the viewModel
            _currentWindow.ShowDialog();  // Show the dialog
        }

        // Method to close the currently open dialog
        public void CloseDialog()
        {
            _currentWindow?.Close();  // Close the window if it's open
        }
    }
}
