using Billing.Domain.Models;
using BillingSoftware.Contracts.Services;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Contracts.Services;
using BillingSoftware.Domain.Extentions;
using BillingSoftware.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using System.Printing;
using System.IO;
using System.ComponentModel;

namespace BillingSoftware.ViewModels;

public class AddPurchaseViewModel : ObservableObject
{
    #region Private Property
    private ICommand _savePurchaseOrderCommand;
    private ICommand _addProductCommand;
    private ICommand _deleteProductRowCommand;
    private PurchasedProductsDto _products;
    private readonly ICommonService _commonService;
    private readonly IProductService _productService;
    private readonly ISupplierService _supplierService;
    private readonly IPurchaseService _purchaseService;
    private SuppliersDto suppliersDto;
    private ProductCategoryDto productCategoryDto;
    private InvoiceDto invoiceDto;
    private readonly IPrintingService _printingService;
    private readonly IPaginator _paginator;
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
            PurchaseProductsSource = new();
            GetInvoiceSourceDetails();
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
            suppliersDto ??= new();
            suppliersDto.SupplierName = value;
            PurchaseProductsSource = new();
            GetInvoiceSourceDetails();
            OnPropertyChanged(nameof(SelectedSupplier));
            OnPropertyChanged(nameof(SupplierText));
            OnPropertyChanged(nameof(SupplierAddress));
            OnPropertyChanged(nameof(SupplierPhoneNo));
            OnPropertyChanged(nameof(InvoiceSource));
            OnPropertyChanged(nameof(SelectedInvoice));
            OnPropertyChanged(nameof(InvoiceText));
        }
    }

    private string _supplierAddress;
    public string SupplierAddress
    {
        get => _supplierAddress ?? suppliersDto?.SupplierAddress;
        set
        {
            _supplierAddress = value;
            suppliersDto.SupplierAddress = value;
            GetInvoiceSourceDetails();
            OnPropertyChanged(nameof(SupplierAddress));
        }
    }

    private string _supplierPhone;
    public string SupplierPhoneNo
    {
        get => _supplierPhone ?? suppliersDto?.SupplierPhoneNumber;
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

    private void GetInvoiceSourceDetails()
    {
        if (suppliersDto.SupplierId != Guid.Empty)
        {
            InvoiceSource = _commonService.GetInvoicesBySupplier(suppliersDto.SupplierId).ToObservableCollection();
            InvoiceText = string.Empty;
            SelectedInvoice = new();
        }
        else {
            InvoiceSource = new ObservableCollection<InvoiceDto>();
            InvoiceText = string.Empty;
            SelectedInvoice = new();
        }
        OnPropertyChanged(nameof(InvoiceSource));
        OnPropertyChanged(nameof(InvoiceText));
        OnPropertyChanged(nameof(SelectedInvoice));
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
            if (value != null)
            {
                PurchaseProductsSource = _purchaseService.GetPurchasedProduct(invoiceDto.PurchaseId).ToObservableCollection();
                AddPropertyChangedForPPSource();
                CalculateTotalPurchasedRate(); 
                TotalAmountPaid = value.AmountPaid;
                BalanceAmount = value.Balance;
                PurchaseProductsSource.CollectionChanged += (s, e) => CalculateTotalPurchasedRate();
            }
            OnPropertyChanged(nameof(SelectedInvoice));
            OnPropertyChanged(nameof(PurchaseProductsSource));
            OnPropertyChanged(nameof(InvoiceDate));
        }
    }

    private void AddPropertyChangedForPPSource()
    {
        foreach (var product in PurchaseProductsSource)
        {
            product.PropertyChanged += PurchaseProducts_PropertyChanged;
        }
    }

    private string _invoiceText;
    public string InvoiceText
    {
        get => _invoiceText;
        set
        {
            _invoiceText = value;
            invoiceDto ??= new();
            invoiceDto.InvoiceNo = value;
            OnPropertyChanged(nameof(InvoiceText));
        }
    }

    private DateTime _invoiceDate;
    public DateTime InvoiceDate
    {
        get { return invoiceDto?.InvoiceDate == DateTime.MinValue || invoiceDto == null ? DateTime.Now : invoiceDto.InvoiceDate; }
        set
        {
            _invoiceDate = value;
            invoiceDto.InvoiceDate = value;
            OnPropertyChanged(nameof(InvoiceDate));
        }
    }

    #endregion Invoice Number

    #region Commands
    public ICommand SavePurchaseOrderCommand => _savePurchaseOrderCommand ??= new RelayCommand(SavePurchaseOrder);
    public ICommand AddProductCommand => _addProductCommand ??= new RelayCommand(AddProduct);
    private ICommand _addProductRowCommand;
    public ICommand AddProductGridRowCommand => _addProductRowCommand ??= new RelayCommand(AddProductGridRow);
    public ICommand DeleteProductRowCommand => _deleteProductRowCommand ??= new RelayCommand(DeleteProductRow);
    private ICommand _printInvoiceCommand;

    public ICommand PrintInvoiceCommand => _printInvoiceCommand ?? (_printInvoiceCommand = new RelayCommand(PrintInvoice));
    private void DeleteProductRow()
    {
        PurchaseProductsSource.Remove(_products);
    }

    private void AddProductGridRow()
    {
        _products = new PurchasedProductsDto() { ProductsDBSource = ProductsList };
        _products.PropertyChanged += PurchaseProducts_PropertyChanged;
        PurchaseProductsSource.Add(_products);
        OnPropertyChanged(nameof(ProductCategorySource));
        OnPropertyChanged(nameof(ProductsSource));
    }

    private void AddProduct()
    {
        //TODO: Add validation
        //bool isNull = _products.GetType().GetProperties().All(p => p.GetValue(_products) == null); 
        //if (!isNull)
        //{ 
        _products = new PurchasedProductsDto() { ProductsDBSource = ProductsList };
        _products.PropertyChanged += PurchaseProducts_PropertyChanged;
        PurchaseProductsSource.Add(_products);
        //_productService.SaveProduct(_products);
        //_products = new();
        //}
        OnPropertyChanged(nameof(ProductCategorySource));
    }

    private void SavePurchaseOrder()
    {
        if (suppliersDto.SupplierId == Guid.Empty)
        {
            suppliersDto.SupplierId = _supplierService.SaveSupplier(suppliersDto);
        }
        else
        {
            suppliersDto.SupplierId = _supplierService.UpdateSupplier(suppliersDto);
        }

        invoiceDto.TotalPurchaseAmount = TotalPurchasedRate;
        invoiceDto.AmountPaid = TotalAmountPaid;
        invoiceDto.Balance = BalanceAmount;
        if (invoiceDto.PurchaseId == Guid.Empty)
        {
            invoiceDto.PurchaseId = _commonService.SaveInvoiceDetails(invoiceDto, suppliersDto.SupplierId);
        }
        else
        {
            _commonService.UpdateInvoiceDetails(invoiceDto);
        }
        _purchaseService.SavePurchaseProducts(PurchaseProductsSource.ToList(), invoiceDto.PurchaseId);

        MessageBox.Show("Saved Successfully..!!");
    }
    #endregion Commands

    private IDialogService _dialogService;

    private AddCatergoryViewModel _addCatergoryViewModel;
    public ICommand AddCategoryCommand { get; set; }

    public List<ProductsDto> ProductsList { get; set; }
    public AddPurchaseViewModel(ICommonService commonService, AddCatergoryViewModel addCatergoryViewModel, IDialogService dialogService, ISupplierService supplierService, IPurchaseService purchaseService, IProductService productService, IPrintingService printingService, IPaginator paginator)
    {
        _products = new();
        _commonService = commonService;
        _supplierService = supplierService;
        _purchaseService = purchaseService;
        _productService = productService;
        _dialogService = dialogService;
        _addCatergoryViewModel = addCatergoryViewModel;
        suppliersDto = new SuppliersDto();
        productCategoryDto = new ProductCategoryDto();
        invoiceDto = new InvoiceDto();
        AddCategoryCommand = new RelayCommand(OpenAddProductCategoryDialog);
        ProductCategorySource = _commonService.GetProductCategory().Value.ToObservableCollection();
        SuppliersSource = _supplierService.GetSuppliers().ToObservableCollection();
        PurchaseProductsSource = new ObservableCollection<PurchasedProductsDto>();
        _printingService = printingService;
        _paginator = paginator;

        ProductsList = _productService.GetProducts().ToList();
        MeasurementUnitSource = _commonService.GetMeasurementUnits().ToObservableCollection();

        PurchaseProductsSource.CollectionChanged += (s, e) => CalculateTotalPurchasedRate();
    }

    private void PurchaseProducts_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PurchasedProductsDto.Amount))
        {
            CalculateTotalPurchasedRate();
        }
    }


    private void CalculateTotalPurchasedRate()
    {
        TotalPurchasedRate = PurchaseProductsSource.Sum(x => x.Amount);
    }

    private decimal _totalPurchaseRate;
    public decimal TotalPurchasedRate
    {
        get
        {
            return _totalPurchaseRate;
        }
        set
        {
            _totalPurchaseRate = value;
            OnPropertyChanged(nameof(TotalPurchasedRate));
            OnPropertyChanged(nameof(TotalAmountPaid));
            CalculateBalanceAmount();
        }
    }


    private decimal _totalAmountPaid;
    public decimal TotalAmountPaid
    {
        get
        {
            return _totalAmountPaid;
        }
        set
        {
            if (_totalAmountPaid != value)
            {
                _totalAmountPaid = value;
                CalculateBalanceAmount();
                OnPropertyChanged(nameof(TotalAmountPaid));

            }
        }
    }

    private void CalculateBalanceAmount()
    {
        BalanceAmount = TotalPurchasedRate - TotalAmountPaid;
        OnPropertyChanged(nameof(BalanceAmount));
    }

    private decimal _balanceAmount;
    public decimal BalanceAmount
    {
        get
        {
            return _balanceAmount;
        }
        set
        {
            _balanceAmount = value;
            OnPropertyChanged(nameof(TotalPurchasedRate));
        }
    }


    private void OpenAddProductCategoryDialog()
    {
        _dialogService.ShowDialog(_addCatergoryViewModel);
        ProductCategorySource = _commonService.GetProductCategory().Value.ToObservableCollection();
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


    private ObservableCollection<MeasurementUnitDto> _measurementUnitSource;
    public ObservableCollection<MeasurementUnitDto> MeasurementUnitSource
    {
        get
        {
            return _measurementUnitSource;
        }
        set
        {
            _measurementUnitSource = value;
            OnPropertyChanged(nameof(ProductCategorySource));
        }
    }


    //private ProductCategoryDto _SelectedProductCategory;
    //public ProductCategoryDto SelectedProductCategory
    //{
    //    get
    //    {
    //        return _SelectedProductCategory;
    //    }
    //    set
    //    {
    //        _SelectedProductCategory = value;
    //        //productCategoryDto = value;
    //        _products.CategoryId = value.CategoryId;
    //        _products.SelectedProductCategoryRow = value;
    //        if (value.CategoryId != Guid.Empty)
    //        {
    //            ProductsSource = _productService.GetProductsByCategory(value.CategoryId).ToObservableCollection();
    //        }
    //        OnPropertyChanged(nameof(SelectedProductCategory));
    //        OnPropertyChanged(nameof(ProductsSource));
    //    }
    //}



    private ObservableCollection<PurchasedProductsDto> _purchaseProductsSource;
    public ObservableCollection<PurchasedProductsDto> PurchaseProductsSource
    {
        get
        {
            return _purchaseProductsSource;
        }
        set
        {
            if (value != null)
            {
                SetProperty(ref _purchaseProductsSource, value);
            }
            OnPropertyChanged(nameof(PurchaseProductsSource));
        }
    }

    private ObservableCollection<PurchasedProductsDto> _productsSource;
    public ObservableCollection<PurchasedProductsDto> ProductsSource
    {
        get
        {
            return _productsSource;
        }
        set
        {
            _productsSource = value;
            OnPropertyChanged(nameof(ProductsSource));
        }
    }

    //private ProductsDto _selectedProductRow;
    //public ProductsDto SelectedProductRow
    //{
    //    get => _selectedProductRow;
    //    set
    //    {
    //        if (value != null)
    //        {
    //            SetProperty(ref _selectedProductRow, value);
    //            _products = value;
    //        }
    //        OnPropertyChanged(nameof(SelectedProductRow));
    //        //OnPropertyChanged(nameof(SelectedProductCategory));
    //    }
    //}

    //private ProductsDto _selectedProduct;
    //public ProductsDto SelectedProduct
    //{
    //    get
    //    {
    //        return _selectedProduct;
    //    }
    //    set
    //    {
    //        _selectedProduct = value;
    //        //productCategoryDto = value;
    //        _products.ProductName = value?.ProductName;
    //        OnPropertyChanged(nameof(SelectedProduct));
    //    }
    //}


    #region Print
    public void PrintInvoice()
    {
        Func<UIElement> reportFactory;
        //reportFactory = () => new SampleTemplate(PurchaseProductsSource.ToList());
        //LoadReport(reportFactory, CancellationToken.None);
    }
    private XpsDocument xpsDocument;

    private IDocumentPaginatorSource generatedDocument;
    public IDocumentPaginatorSource GeneratedDocument
    {
        get => generatedDocument;
        set
        {
            generatedDocument = value;
        }
    }

    private void LoadReport(Func<UIElement> reportFactory, CancellationToken cancellationToken)
    {
        var printTicket = _printingService.GetPrintTicket("Microsoft Print to PDF", new PageMediaSize(PageMediaSizeName.NorthAmericaLetter, 816, 1056), PageOrientation.Portrait);
        var printCapabilities = _printingService.GetPrinterCapabilitiesForPrintTicket(printTicket, "Microsoft Print to PDF");

        if (printCapabilities.OrientedPageMediaWidth.HasValue && printCapabilities.OrientedPageMediaHeight.HasValue)
        {
            var pageSize = new Size(printCapabilities.OrientedPageMediaWidth.Value, printCapabilities.OrientedPageMediaHeight.Value);

            var desiredMargin = new Thickness(40);
            var printerMinMargins = _printingService.GetMinimumPageMargins(printCapabilities);
            AdjustMargins(ref desiredMargin, printerMinMargins);

            var pages = _paginator.PaginateAsync(reportFactory, pageSize, desiredMargin, cancellationToken);
            var fixedDocument = _paginator.GetFixedDocumentFromPages(pages, pageSize);

            // We now could simply assign the fixedDocument to GeneratedDocument
            // But then for some reason the DocumentViewer search feature breaks
            // The solution is to create an XPS file first and get the FixedDocumentSequence
            // from it and then use that in the DocumentViewer

            // Delete old XPS file first
            CleanXpsDocumentResources();

            xpsDocument = _printingService.GetXpsDocumentFromFixedDocument(fixedDocument);
            GeneratedDocument = xpsDocument.GetFixedDocumentSequence();

            _printingService.PrintDocument("Microsoft Print to PDF", generatedDocument, "Hello from WPF!", printTicket);
        }
    }

    private void CleanXpsDocumentResources()
    {
        if (xpsDocument != null)
        {
            try
            {
                xpsDocument.Close();
                File.Delete(xpsDocument.Uri.AbsolutePath);
            }
            catch
            {
            }
            finally
            {
                xpsDocument = null;
            }
        }
    }

    private static void AdjustMargins(ref Thickness pageMargins, Thickness minimumMargins)
    {
        if (pageMargins.Left < minimumMargins.Left)
        {
            pageMargins.Left = minimumMargins.Left;
        }

        if (pageMargins.Top < minimumMargins.Top)
        {
            pageMargins.Top = minimumMargins.Top;
        }

        if (pageMargins.Right < minimumMargins.Right)
        {
            pageMargins.Right = minimumMargins.Right;
        }

        if (pageMargins.Bottom < minimumMargins.Bottom)
        {
            pageMargins.Bottom = minimumMargins.Bottom;
        }
    }

    #endregion Print
}
