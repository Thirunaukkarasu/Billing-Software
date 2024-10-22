using BillingSoftware.Contracts.Services;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Domain.Entities;
using BillingSoftware.Domain.Enums;
using BillingSoftware.Domain.Extentions;
using BillingSoftware.Domain.Models;
using BillingSoftware.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace BillingSoftware.ViewModels
{
    public class PurchaseBoardViewModel : ObservableObject, INotifyDataErrorInfo
    {

        private readonly ICommonService _commonService;
        private ObservableCollection<InvoiceDto> purchaseInvoiceSource;
        private readonly List<InvoiceDto> _invoiceDBSource;
        private readonly ObservableCollection<InvoiceDto> _invoiceOCDBSource;
        private readonly INavigationService _navigationService;
        public ObservableCollection<InvoiceDto> PurchaseInvoiceSource
        {
            get
            {
                return purchaseInvoiceSource;
            }
            set
            {
                if (value != null)
                {
                    SetProperty(ref purchaseInvoiceSource, value);
                }
                OnPropertyChanged(nameof(purchaseInvoiceSource));
            }
        }

        private PurchaseDateFilterOptions _selectedDateOption;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private bool _isCustomDateSelected;
        private bool _isSpecificDateSelected;

        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public ObservableCollection<PurchaseDateFilterOptions> DateOptions { get; }

        public PurchaseDateFilterOptions SelectedDateOption
        {
            get => _selectedDateOption;
            set
            {
                if (_selectedDateOption != value)
                {
                    _selectedDateOption = value;
                    OnPropertyChanged(nameof(SelectedDateOption));
                    OnSelectedDateOptionChanged();
                }
            }
        }

        private DateTime? _specificDate;
        public DateTime? SpecificDate
        {
            get => _specificDate;
            set
            {
                _specificDate = value;
                OnPropertyChanged(nameof(SpecificDate));
                //ValidateDate();
            }
        }

        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
                //ValidateDate();
            }
        }

        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
                //ValidateDate();
            }
        }
        public bool IsCustomDateSelected
        {
            get => _isCustomDateSelected;
            set
            {
                _isCustomDateSelected = value;
                OnPropertyChanged(nameof(IsCustomDateSelected));
            }
        }

        public bool IsSpecificDateSelected
        {
            get => _isSpecificDateSelected;
            set
            {
                _isSpecificDateSelected = value;
                OnPropertyChanged(nameof(IsSpecificDateSelected));
            }
        }
        public PurchaseBoardViewModel(ICommonService commonService, INavigationService navigationService)
        {
            _navigationService = navigationService;
            DateOptions = new ObservableCollection<PurchaseDateFilterOptions>
                                {
                                    new() { Key = "All",  Value = "All" },
                                    new() { Key = "Today",  Value = "Today"  },
                                    new() { Key = "ThisWeek", Value = "This Week" },
                                    new() { Key = "ThisMonth", Value = "This Month"  },
                                    new() { Key = "SpecificDate", Value = "Specific Date"  },
                                    new() { Key = "CustomDate", Value = "Custom Date"  }
                                };

            _commonService = commonService;
            _invoiceDBSource = _commonService.GetAllInvoicesDetails();
            _invoiceOCDBSource = _invoiceDBSource.ToObservableCollection();
            PurchaseInvoiceSource = _invoiceOCDBSource;
            SelectedDateOption = DateOptions[2];
            GetFilteredInvoice(); 
        }

        private InvoiceDto _selectedInvoice;

        public InvoiceDto SelectedInvoiceRow
        { 
            get { return _selectedInvoice; }
            set { _selectedInvoice = value; OnPropertyChanged(nameof(SelectedInvoiceRow)); }
        
        }

        private void OnViewInvoice()
        {
            if (SelectedInvoiceRow != null)
            { 
                // Navigation logic to the Purchase Page
                // You can use a Frame or NavigationService depending on your app
                _navigationService.NavigateTo(typeof(AddPurchaseViewModel).FullName, SelectedInvoiceRow);
            }
        }

        private void GetAllInvoices()
        {
            PurchaseInvoiceSource = _invoiceOCDBSource;
        }

        private void OnSelectedDateOptionChanged()
        {
            if (SelectedDateOption != null)
            {
                if (SelectedDateOption.Key == "CustomDate")
                {
                    IsCustomDateSelected = true;
                    IsSpecificDateSelected = false;
                }
                else if (SelectedDateOption.Key == "SpecificDate")
                {
                    IsCustomDateSelected = false;
                    IsSpecificDateSelected = true;
                }
                else
                {
                    IsCustomDateSelected = false;
                    IsSpecificDateSelected = false;
                    StartDate = null;
                    EndDate = null;
                    SpecificDate = null;
                }
            }
            else
            {
                IsCustomDateSelected = false;
                IsSpecificDateSelected = false;
            }
        }

        private void CalculateTilesAmounts()
        {
            if (PurchaseInvoiceSource.Count > 0)
            {
                TotalAmount = PurchaseInvoiceSource.Sum(x => x.TotalPurchaseAmount);
                AmountPaid = PurchaseInvoiceSource.Sum(x => x.AmountPaid);
                AmountUnPaid = TotalAmount - AmountPaid;
            }

        }

        private void ClearTilesValues()
        {
            if (PurchaseInvoiceSource.Count == 0)
            {
                TotalAmount = 0;
                AmountPaid = 0;
                AmountUnPaid = 0;
            }
        }

        private string _invoiceNumber;

        public string InvoiceNumber
        {
            get { return _invoiceNumber; }
            set { _invoiceNumber = value; OnPropertyChanged(nameof(InvoiceNumber)); }
        }

        private string _supplierName;

        public string SupplierName
        {
            get { return _supplierName; }
            set { _supplierName = value; OnPropertyChanged(nameof(SupplierName)); }
        }
         
        private decimal _amountPaid;

        public decimal AmountPaid
        {
            get { return _amountPaid; }
            set
            {
                _amountPaid = value;
                OnPropertyChanged(nameof(AmountPaid));
            }
        }

        private decimal _amountUnPaid;

        public decimal AmountUnPaid
        {
            get { return _amountUnPaid; }
            set
            {
                _amountUnPaid = value;
                OnPropertyChanged(nameof(AmountUnPaid));
            }
        }

        private decimal _totalAmount;

        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                _totalAmount = value;
                OnPropertyChanged(nameof(TotalAmount));
            }
        }

        private ICommand _searchInvoiceClickedCommand; 

        private ICommand _viewInvoiceCommandClickedCommand;

        public ICommand SearchInvoiceClickedCommand => _searchInvoiceClickedCommand ??= new RelayCommand(GetFilteredInvoice);
        public ICommand ViewInvoiceCommand => _viewInvoiceCommandClickedCommand ??= new RelayCommand(OnViewInvoice);

        private void GetFilteredInvoice()
        {
            ValidateDate();
            if (HasErrors)
            {
                return;
            }
            //if (string.IsNullOrWhiteSpace(SupplierName) &&
            //    string.IsNullOrWhiteSpace(InvoiceNumber) &&
            //    !InvoiceDate.HasValue)
            //{
            //    // No search criteria entered, show popup
            //    MessageBox.Show("Please enter at least one search criteria.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}

            var filtered = _invoiceDBSource.AsEnumerable(); 
            switch (SelectedDateOption.Key)
            {
                case "Today":
                    filtered = filtered.Where(i => i.InvoiceDate.Date == DateTime.Today);
                    break;
                case "ThisWeek":
                    StartDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                    EndDate = StartDate?.AddDays(6);
                    filtered = filtered.Where(i => i.InvoiceDate >= StartDate && i.InvoiceDate <= EndDate);
                    break;
                case "ThisMonth":
                    StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    EndDate = StartDate?.AddMonths(1).AddDays(-1);
                    filtered = filtered.Where(i => i.InvoiceDate >= StartDate && i.InvoiceDate <= EndDate);
                    break;
                case "SpecificDate":
                    if (SpecificDate.HasValue)
                    {
                        filtered = filtered.Where(i => i.InvoiceDate == SpecificDate.Value);
                    }
                    break;

                case "CustomDate":
                    if (StartDate.HasValue && EndDate.HasValue)
                    {
                        filtered = filtered.Where(i => i.InvoiceDate >= StartDate.Value && i.InvoiceDate <= EndDate.Value);
                    }
                    break;
            }


            // Filter based on the search criteria
            filtered = filtered.Where(invoice =>
               (string.IsNullOrWhiteSpace(SupplierName) || invoice.SupplierName.Contains(SupplierName)) &&
               (string.IsNullOrWhiteSpace(InvoiceNumber) || invoice.InvoiceNo.Contains(InvoiceNumber))).AsEnumerable();

            if (filtered.Any())
            {
                PurchaseInvoiceSource.Clear();
                PurchaseInvoiceSource = filtered.ToList().ToObservableCollection();
                CalculateTilesAmounts();
            }
            else
            {
                MessageBox.Show("No invoices found matching the search criteria.", "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);
                PurchaseInvoiceSource.Clear();
                ClearTilesValues();
                return;
            }
        }
         
        private void ValidateDate()
        {
            ClearErrors(nameof(StartDate));
            ClearErrors(nameof(EndDate));
            ClearErrors(nameof(SpecificDate));
            if (IsSpecificDateSelected)
            {
                if (!SpecificDate.HasValue)
                {
                    AddError(nameof(SpecificDate), "Enter Specific Date.");
                }
                if (SpecificDate.HasValue && SpecificDate.Value > DateTime.Today)
                {
                    AddError(nameof(SpecificDate), "Date cannot be greater than today Date.");
                }
                if (HasErrors)
                {
                    if (_errors.ContainsKey("SpecificDate"))
                    {
                        foreach (var error in _errors["SpecificDate"])
                            MessageBox.Show(error, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    return;
                }
            }
            if (IsCustomDateSelected)
            {
                if (!StartDate.HasValue)
                {
                    AddError(nameof(StartDate), "Start Date is required.");
                }

                if (!EndDate.HasValue)
                {
                    AddError(nameof(EndDate), "End Date is required.");
                }

                if (StartDate.HasValue && EndDate.HasValue && StartDate > EndDate)
                {
                    AddError(nameof(EndDate), "End Date cannot be earlier than Start Date.");
                }

                if (HasErrors)
                {
                    MessageBox.Show("Please correct the errors before proceeding.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
        }

        #region INotifyDataErrorInfo Implementation

        public bool HasErrors => _errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = new List<string>();
            }
            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion
    }
}