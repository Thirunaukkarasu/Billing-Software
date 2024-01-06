using BillingSoftware.Contracts.Views;
using BillingSoftware.ViewModels;
using MahApps.Metro.Controls;
using System.Windows.Controls;

namespace BillingSoftware.Views;

public partial class ShellWindow : MetroWindow, IShellWindow
{
    public ShellWindow(ShellViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public Frame GetNavigationFrame()
        => shellFrame;

    public void ShowWindow()
        => Show();

    public void CloseWindow()
        => Close();
}
