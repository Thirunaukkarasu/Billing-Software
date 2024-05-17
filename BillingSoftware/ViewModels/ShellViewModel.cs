using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using BillingSoftware.Contracts.Services;
using BillingSoftware.Properties;
using BillingSoftware.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MahApps.Metro.Controls;

namespace BillingSoftware.ViewModels;

public class ShellViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private HamburgerMenuItem _selectedMenuItem;
    private RelayCommand _goBackCommand;
    private ICommand _menuItemInvokedCommand;
    private ICommand _loadedCommand;
    private ICommand _unloadedCommand;

    public HamburgerMenuItem SelectedMenuItem
    {
        get { return _selectedMenuItem; }
        set { SetProperty(ref _selectedMenuItem, value); }
    }

    // TODO: Change the icons and titles for all HamburgerMenuItems here.
    
    public ObservableCollection<HamburgerMenuImageItem> MenuItems { get; } = new ObservableCollection<HamburgerMenuImageItem>()
    {
        new HamburgerMenuImageItem() { Label = Resources.ShellMainPage, Thumbnail =  new BitmapImage(new Uri("./Icons/down.png", UriKind.Relative)), TargetPageType = typeof(MainViewModel) },
        new HamburgerMenuImageItem() { Label = Resources.ShellDashboardPage, Thumbnail = new BitmapImage(new Uri("./Icons/down.png", UriKind.Relative)), TargetPageType = typeof(DashboardViewModel) },
        new HamburgerMenuImageItem() { Label = Resources.ShellPurchasePage, Thumbnail = new BitmapImage(new Uri("./Icons/down.png", UriKind.Relative)), TargetPageType = typeof(PurchaseViewModel) },
        new HamburgerMenuImageItem() { Label = Resources.ShellPurchaseBoardPage, Thumbnail = new BitmapImage(new Uri("./Icons/down.png", UriKind.Relative)), TargetPageType = typeof(PurchaseBoardViewModel) },
        new HamburgerMenuImageItem() { Label = Resources.ShellAddPurchasePage, Thumbnail = new BitmapImage(new Uri("./Icons/down.png", UriKind.Relative)), TargetPageType = typeof(AddPurchaseViewModel ) },
        new HamburgerMenuImageItem() { Label = Resources.ShellSalesPage, Thumbnail = new BitmapImage(new Uri("./Icons/down.png", UriKind.Relative)), TargetPageType = typeof(SalesViewModel) },
        new HamburgerMenuImageItem() { Label = Resources.ShellQuotationPage, Thumbnail = new BitmapImage(new Uri("./Icons/down.png", UriKind.Relative)), TargetPageType = typeof(QuotationViewModel) },
        new HamburgerMenuImageItem() { Label = Resources.ShellCustomersPage, Thumbnail = new BitmapImage(new Uri("./Icons/down.png", UriKind.Relative)), TargetPageType = typeof(CustomersViewModel) },
        new HamburgerMenuImageItem() { Label = Resources.ShellReportPage, Thumbnail = new BitmapImage(new Uri("./Icons/down.png", UriKind.Relative)), TargetPageType = typeof(ReportViewModel) },
        new HamburgerMenuImageItem() { Label = Resources.ShellBackupPage, Thumbnail = new BitmapImage(new Uri("./Icons/down.png", UriKind.Relative)), TargetPageType = typeof(BackupViewModel) },
        new HamburgerMenuImageItem() { Label = Resources.ShellContentGridPage, Thumbnail = new BitmapImage(new Uri("./Icons/down.png", UriKind.Relative)), TargetPageType = typeof(ContentGridViewModel) },
        new HamburgerMenuImageItem() { Label = Resources.ShellListDetailsPage, Thumbnail = new BitmapImage(new Uri("./Icons/down.png", UriKind.Relative)), TargetPageType = typeof(ListDetailsViewModel) },
    };

    public RelayCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(OnGoBack, CanGoBack));

    public ICommand MenuItemInvokedCommand => _menuItemInvokedCommand ?? (_menuItemInvokedCommand = new RelayCommand(OnMenuItemInvoked));

    public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(OnLoaded));

    public ICommand UnloadedCommand => _unloadedCommand ?? (_unloadedCommand = new RelayCommand(OnUnloaded));

    public ShellViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    private void OnLoaded()
    {
        _navigationService.Navigated += OnNavigated;
    }

    private void OnUnloaded()
    {
        _navigationService.Navigated -= OnNavigated;
    }

    private bool CanGoBack()
        => _navigationService.CanGoBack;

    private void OnGoBack()
        => _navigationService.GoBack();

    private void OnMenuItemInvoked()
        => NavigateTo(SelectedMenuItem.TargetPageType);

    private void NavigateTo(Type targetViewModel)
    {
        if (targetViewModel != null)
        {
            _navigationService.NavigateTo(targetViewModel.FullName);
        }
    }

    private void OnNavigated(object sender, string viewModelName)
    {
        var item = MenuItems
                    .OfType<HamburgerMenuItem>()
                    .FirstOrDefault(i => viewModelName == i.TargetPageType?.FullName);
        if (item != null)
        {
            SelectedMenuItem = item;
        }

        GoBackCommand.NotifyCanExecuteChanged();
    }
}
