using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BillingSoftware.Contracts.Services
{
    public interface IDialogService
    {
        void ShowDialog<TWindow>(object viewModel) where TWindow : Window, new();

        void CloseDialog();
    }
}
