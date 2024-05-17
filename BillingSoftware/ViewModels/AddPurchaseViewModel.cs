using Billing.Domain.Models;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Services;
using BillingSoftware.Domain.Entities;
using BillingSoftware.Domain.Extentions;
using BillingSoftware.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BillingSoftware.ViewModels;

public class AddPurchaseViewModel : ObservableObject
{
    #region Private Property
    private ICommand _savePurchaseOrderCommand;
    private Products _products;
    private ObservableCollection<Products> _productsSource;
    private readonly ICommonService _commonService;
    private readonly IProductService _productService;
    private readonly ISupplierService _supplierService;
    private  SuppliersDto suppliersDto;
    private ProductCategoryDto productCategoryDto;
    private PurchaseOrders purchaseOrders;
    private InvoiceDto invoiceDto;
    #endregion Private Property

    #region Supplier
    private ObservableCollection<SuppliersDto> _suppliersSource;
    public ObservableCollection<SuppliersDto> SuppliersSource
    {
        get
        {
            return _suppliersSource;
        }
        set
        {
            _suppliersSource = value;
            OnPropertyChanged(nameof(SuppliersSource));
        }
    }

    private SuppliersDto _selectedSupplier;
    public SuppliersDto SelectedSupplier
    {
        get
        {
            return _selectedSupplier;
        }
        set
        {
            SetProperty(ref _selectedSupplier, value, nameof(SelectedSupplier));
            suppliersDto = value;
            OnPropertyChanged(nameof(SupplierAddress));
            OnPropertyChanged(nameof(SupplierPhoneNo));
            OnPropertyChanged(nameof(InvoiceSource));
            OnPropertyChanged(nameof(SelectedInvoice));
            OnPropertyChanged(nameof(InvoiceText));
        }
    }

    private string _supplierText;
    public string SupplierText
    {
        get => _supplierText;
        set
        {
            _supplierText = value;
            suppliersDto.SupplierName = value;
            OnPropertyChanged(nameof(SupplierText));
        }
    }

    private string _supplierAddress;
    public string SupplierAddress
    {
        get => _supplierAddress?? suppliersDto.SupplierAddress;
        set
        {
            _supplierAddress = value;
            suppliersDto.SupplierAddress = value;
            OnPropertyChanged(nameof(SupplierAddress));
        }
    }

    private string _supplierPhone;
    public string SupplierPhoneNo
    {
        get => _supplierPhone ?? suppliersDto.SupplierPhoneNumber;
        set
        {
            _supplierPhone = value;
            suppliersDto.SupplierPhoneNumber = value;
            OnPropertyChanged(nameof(SupplierPhoneNo));
        }
    }

    #endregion Supplier

    #region Invoice Number
    private ObservableCollection<InvoiceDto> _invoiceSource;
    public ObservableCollection<InvoiceDto> InvoiceSource
    {
        get
        {
            return _invoiceSource;
        }
        set
        {
            _invoiceSource = value;
            OnPropertyChanged(nameof(InvoiceSource));
            OnPropertyChanged(nameof(InvoiceDate));
        }
    }

    private InvoiceDto _selectedInvoice;
    public InvoiceDto SelectedInvoice
    {
        get
        {
            return _selectedInvoice;
        }
        set
        {
            _selectedInvoice = value;
            invoiceDto = value;
            OnPropertyChanged(nameof(SelectedInvoice)); 
            OnPropertyChanged(nameof(InvoiceDate)); 
        }
    }

    private string _invoiceText;
    public string InvoiceText
    {
        get => _invoiceText;
        set
        {
            _invoiceText = value;
            invoiceDto.InvoiceNo = value;
            OnPropertyChanged(nameof(InvoiceText));
        }
    }

    private DateTime _invoiceDate;
    public DateTime InvoiceDate
    {
        get { return _invoiceDate == DateTime.MinValue ? DateTime.Now : invoiceDto.InvoiceDate; }
        set
        {
            _invoiceDate = value;
            invoiceDto.InvoiceDate = value;
            OnPropertyChanged(nameof(InvoiceDate));
        }
    }

    #endregion Invoice Number

    #region Commands
    public ICommand SavePurchaseOrderCommand => _savePurchaseOrderCommand ?? (_savePurchaseOrderCommand = new RelayCommand(SavePurchaseOrder));

    private void SavePurchaseOrder()
    {
        Guid supplierId;
        if (suppliersDto.SupplierId is null)
        {
            supplierId = _supplierService.SaveSupplier(suppliersDto);
        }
        else
        {
            supplierId = _supplierService.UpdateSupplier(suppliersDto);
        }

        if (productCategoryDto.CategoryId is null)
        { 
            _commonService.SaveProductCategory(productCategoryDto);
        }

        _commonService.SaveInvoiceDetails(invoiceDto, supplierId);

    }
    #endregion Commands
    public AddPurchaseViewModel(ICommonService commonService, ISupplierService supplierService)
    {
        suppliersDto = new SuppliersDto();
        productCategoryDto = new ProductCategoryDto();
        invoiceDto = new InvoiceDto();
        _commonService = commonService;
        _supplierService = supplierService;
        ProductCategorySource = _commonService.GetProductCategory().Value.ToObservableCollection();
        SuppliersSource = _supplierService.GetSuppliers().ToObservableCollection();
    }

    private ObservableCollection<ProductCategoryDto> _productCategorySource;
    public ObservableCollection<ProductCategoryDto> ProductCategorySource
    {
        get
        {
            return _productCategorySource;
        }
        set
        {
            _productCategorySource = value;
            OnPropertyChanged(nameof(ProductCategorySource));
        }
    }

    private ProductCategoryDto _SelectedProductCategory;
    public ProductCategoryDto SelectedProductCategory
    {
        get
        {
            return _SelectedProductCategory;
        }
        set
        {
            _SelectedProductCategory = value;
            productCategoryDto = value;
            OnPropertyChanged(nameof(SelectedProductCategory));
        }
    }

    private string _ProductCategoryText;
    public string ProductCategoryText
    {
        get => _ProductCategoryText;
        set
        {
            _ProductCategoryText = value;
            OnPropertyChanged(nameof(ProductCategoryText));
        }
    }

 
    public Products Products
    {
        get
        {
            return _products;
        }
        set
        {
            _products = new Products();
            SetProperty(ref _products, value);
        }
    }

}
