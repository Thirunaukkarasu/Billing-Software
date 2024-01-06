using Billing.Domain.Models;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Contracts.Services;
using BillingSoftware.Core.Services;
using BillingSoftware.Domain.Extentions;
using BillingSoftware.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Windows.Input;

namespace BillingSoftware.ViewModels;

public class PurchaseViewModel : ObservableObject, INotifyPropertyChanged
{
    #region Declaration
    private ProductItems _productItem;
    private ProductsCollection _selectedItem = new ProductsCollection();
    private ICommand _saveProductsCommand;
    private ICommand _productSelectionChangedCommand;
    private ICommand _companyDDSelectionChangeCommand;
    private ObservableCollection<ProductsCollection> _productItems;
    private readonly ICommonService _commonService;
    #endregion Declaration

    #region Commands
    public ICommand SaveProductsCommand => _saveProductsCommand ?? (_saveProductsCommand = new RelayCommand(SaveProducts));
    public ICommand ProductSelectionChangedCommand => _productSelectionChangedCommand ?? (_productSelectionChangedCommand = new RelayCommand(ProductSelectionChanged));
    public ICommand CompanyDDSelectionChangeCommand => _companyDDSelectionChangeCommand ?? (_companyDDSelectionChangeCommand = new RelayCommand(CompanyDDSelectionChanged));

   
    #endregion Commands

    #region Properties
    public ObservableCollection<ProductsCollection> ProductItemsSource
    {
        get 
        { 
            return _productItems; 
        } 
        set
        {
            SetProperty(ref _productItems, value);
            OnPropertyChanged("ProductItems");
        }
    }
    public ProductsCollection SelectedProduct
    { 
        get => _selectedItem;
        set
        {
            SetProperty(ref _selectedItem, value);

            OnPropertyChanged("SelectedProduct");
        }
    } 
    public ProductItems ProductItem
    {
        get
        {
            return _productItem;
        }
        set
        {
            SetProperty(ref _productItem, value);
            OnPropertyChanged("Product");
        }
    }

    private ObservableCollection<CompanyDetails> _companySource; 
    public ObservableCollection<CompanyDetails> CompanySource
    { 
        get 
        { 
            return _companySource; 
        } 
        set 
        {
            _companySource = value;
            OnPropertyChanged("CompanySource");
        } 
    }

    private ObservableCollection<InvoiceDetails> _invoiceSource;
    public ObservableCollection<InvoiceDetails> InvoiceSource
    {
        get
        {
            return _invoiceSource;
        }
        set
        {
            _invoiceSource = value;
            OnPropertyChanged("InvoiceSource");
        }
    }

    private CompanyDetails _SelectedCompany;
    public CompanyDetails SelectedCompany
    { 
        get
        {
            return _SelectedCompany;
        }
        set
        {
            _SelectedCompany = value; 
            OnPropertyChanged("SelectedCompany");
        }
    }

    private string _companyText;
    public string CompanyText
    { 
        set
        {
            _companyText = value;
            if (SelectedCompany != null && SelectedCompany.CompanyName.ToLower() != value.ToLower())
            {
                SelectedCompany = null;
            }
            InvoiceSource?.Clear(); 
            if (SelectedCompany != null)
            {
                InvoiceSource = _commonService.GetInvoiceDetails(_SelectedCompany.CompanyId).Value.ToObservableCollection();
                OnPropertyChanged("InvoiceSource");
            }
            OnPropertyChanged("CompanyText");
        }
    }


    private InvoiceDetails _SelectedInvoice;
    public InvoiceDetails SelectedInvoice
    {
        get
        {
            return _SelectedInvoice;
        }
        set
        {
            _SelectedInvoice = value;
            OnPropertyChanged("SelectedInvoice");
        }
    }

    private InvoiceDetails _SelectedInvoiceDetails;
    public InvoiceDetails SelectedInvoiceDetails
    {
        get
        {
            return _SelectedInvoiceDetails;
        }
        set
        {
            _SelectedInvoiceDetails = value;
            OnPropertyChanged("SelectedInvoiceDetails");
        }
    }

    private string _invoiceText;
    public string InvoiceText
    {
        set
        {
            _invoiceText = value;
            if (SelectedInvoice != null && SelectedInvoice.ToString().ToLower() != value.ToLower())
            {
               SelectedInvoice = null;
               SelectedInvoiceDetails = null;
            }
            if (SelectedInvoice != null)
            {
                SelectedInvoiceDetails = SelectedInvoice;
                OnPropertyChanged("SelectedInvoiceDetails");
            }
            OnPropertyChanged("InvoiceText");
        }
    }

    #endregion Properties

    #region Constructor
    public PurchaseViewModel(ICommonService commonService)
    { 
        _commonService = commonService; 
        ProductItemsSource = new ObservableCollection<ProductsCollection>();
        ProductItem = new ProductItems(); 
        CompanySource =  _commonService.GetCompanyDetails().Value.ToObservableCollection(); 
        
    }
    #endregion Constructor

    #region CommandMethods 
    public void SaveProducts()
    {
         
    }

    public void ProductSelectionChanged()
    {
       
    }

    public void CompanyDDSelectionChanged()
    {
        InvoiceSource = _commonService.GetInvoiceDetails(null).Value.ToObservableCollection();
    }

    #endregion CommandMethods
}
