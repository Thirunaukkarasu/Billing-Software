using Billing.Domain.Models;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Contracts.Services;
using BillingSoftware.Domain.Extentions;
using BillingSoftware.Domain.Models;
using BillingSoftware.PrintTemplates;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Xps.Packaging;

namespace BillingSoftware.ViewModels;

public class PurchaseViewModel : ObservableObject, INotifyPropertyChanged
{
    #region Declaration
    private ProductItems _productItem;
    private ProductItems _selectedItem;
    private ICommand _saveProductsCommand;
    private ICommand _productSelectionChangedCommand;
    private ICommand _companyDDSelectionChangeCommand;
    private ICommand _printInvoiceCommand;
    private ICommand _addNewProductCommand;
    private ObservableCollection<ProductItems> _productItemsSource;
    private readonly ICommonService _commonService;
    private readonly IProductService _productService;
    private readonly IPrintingService _printingService;
    private readonly IPaginator _paginator;
    #endregion Declaration

    #region Commands
    public ICommand SaveProductsCommand => _saveProductsCommand ?? (_saveProductsCommand = new RelayCommand(SaveProducts));
    public ICommand ProductSelectionChangedCommand => _productSelectionChangedCommand ?? (_productSelectionChangedCommand = new RelayCommand(ProductSelectionChanged));
    public ICommand CompanyDDSelectionChangeCommand => _companyDDSelectionChangeCommand ?? (_companyDDSelectionChangeCommand = new RelayCommand(CompanyDDSelectionChanged));
    public ICommand PrintInvoiceCommand => _printInvoiceCommand ?? (_printInvoiceCommand = new RelayCommand(PrintInvoice));
    public ICommand AddNewProductCommand => _addNewProductCommand ?? (_addNewProductCommand = new RelayCommand(AddNewProduct));



    #endregion Commands

    #region Properties
    public ObservableCollection<ProductItems> ProductItemsSource
    {
        get 
        { 
            return _productItemsSource; 
        } 
        set
        {
            _productItemsSource = new();
            SetProperty(ref _productItemsSource, value); 
            OnPropertyChanged(nameof(ProductItemsSource));
        }
    }
    public ProductItems SelectedProductRow
    { 
        get => _selectedItem;
        set
        {
            SetProperty(ref _selectedItem, value); 
            OnPropertyChanged(nameof(SelectedProductRow));
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
            _productItem = new ProductItems();
            SetProperty(ref _productItem, value);
            if (value.CategoryId != null)
            {
                SelectedProductItemCategory = ProductItemCategorySource.FirstOrDefault(x => x.CategoryId == value.CategoryId);
            }
            if (value.MeasurementUnitId != null)
            {
                SelectedProductItemMeasurementUnit = ProductItemMeasurementUnitSource.FirstOrDefault(x => x.MeasurementUnitId == value.MeasurementUnitId);
            }
            OnPropertyChanged(nameof(ProductItem)); 
            OnPropertyChanged(nameof(DisplayName)); 
        }
    }

    public string DisplayName
    {
        get 
        {
            var companyName = SelectedCompany != null ? SelectedCompany.CompanyName : CompanyText;
            return $"{companyName} {ProductItem?.ProductName} {ProductItem?.QuantityPerUnit}{SelectedProductItemMeasurementUnit?.Symbol}";   
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
            OnPropertyChanged(nameof(CompanySource));
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
            OnPropertyChanged(nameof(InvoiceSource));
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
           SetProperty(ref _SelectedCompany, value, nameof(SelectedCompany));
           ResetInvoiceField();
        }
    }

    private string _companyText;
    public string CompanyText
    {
        get => _companyText;
        set
        {
            _companyText = value; 
            //selected company
            if (SelectedCompany != null && SelectedCompany.CompanyName.ToLower() != value?.ToLower())
            {
                SelectedCompany = null;
            }
            InvoiceSource?.Clear(); 
            if (SelectedCompany != null)
            {
                InvoiceSource = _commonService.GetInvoiceDetails(_SelectedCompany.CompanyId).Value.ToObservableCollection();
                OnPropertyChanged(nameof(InvoiceSource));
            }
            OnPropertyChanged(nameof(CompanyText));
        }
    }

    //Invoice DD selected values
    private InvoiceDetails _SelectedInvoice;
    public InvoiceDetails SelectedInvoice
    {
        get
        {
            return _SelectedInvoice;
        }
        set
        {
            if(value != null) 
            { 
                _SelectedInvoice = value;
                GetProductItemsByInvoice(value.InvoiceId, SelectedCompany.CompanyId);
                ProductItem = ProductItemsSource.FirstOrDefault();
                if (ProductItem?.CategoryId != null)
                {
                    SelectedProductItemCategory = ProductItemCategorySource.FirstOrDefault(x => x.CategoryId == ProductItem.CategoryId);
                }
                if (ProductItem?.MeasurementUnitId != null)
                {
                    SelectedProductItemMeasurementUnit = ProductItemMeasurementUnitSource.FirstOrDefault(x => x.MeasurementUnitId == ProductItem.MeasurementUnitId);
                }
                if (ProductItem == null)
                {
                    ProductItem = new ProductItems();
                }
                OnPropertyChanged(nameof(SelectedInvoice));
                OnPropertyChanged(nameof(ProductItem));
            }
            else
            {
                SetProperty(ref _SelectedInvoice, value);
            }
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
            OnPropertyChanged(nameof(SelectedInvoiceDetails));
            OnPropertyChanged(nameof(ProductItemsSource));
        }
    } 

    private string _invoiceText;
    public string InvoiceText
    {
        get => _invoiceText;
        set
        {
            _invoiceText = value;  
            if (SelectedInvoice != null && SelectedInvoice.ToString().ToLower() != value?.ToLower()) //on change of text
            {
               SelectedInvoice = null;
               SelectedInvoiceDetails = null;
            }
            if (SelectedInvoice != null) // on change of DD
            {
                SelectedInvoiceDetails = SelectedInvoice;
                OnPropertyChanged(nameof(SelectedInvoiceDetails));
            } 
            OnPropertyChanged(nameof(InvoiceText));
        }
    }


    private ObservableCollection<ProductItemCategory> _productItemCategorySource;
    public ObservableCollection<ProductItemCategory> ProductItemCategorySource
    {
        get
        {
            return _productItemCategorySource;
        }
        set
        {
            _productItemCategorySource = value;
            OnPropertyChanged(nameof(ProductItemCategorySource));
        }
    }

    private ProductItemCategory _SelectedProductItemCategory;
    public ProductItemCategory SelectedProductItemCategory
    {
        get
        {
            return _SelectedProductItemCategory;
        }
        set
        {
            _SelectedProductItemCategory = value;
            ProductItem.CategoryId = value?.CategoryId;
            OnPropertyChanged(nameof(SelectedProductItemCategory));
        }
    }

    private string _ProductItemCategoryText;
    public string ProductItemCategoryText
    {
        get => _ProductItemCategoryText;
        set
        {
            _ProductItemCategoryText = value; 
            OnPropertyChanged(nameof(ProductItemCategoryText));
        }
    }


    //MeasurementUnit 

    private ObservableCollection<ProductItemMeasurementUnit> _productItemMeasurementUnitSource;
    public ObservableCollection<ProductItemMeasurementUnit> ProductItemMeasurementUnitSource
    {
        get
        {
            return _productItemMeasurementUnitSource;
        }
        set
        {
            _productItemMeasurementUnitSource = value;
            OnPropertyChanged(nameof(ProductItemMeasurementUnitSource));
        }
    }

    private ProductItemMeasurementUnit _SelectedProductItemMeasurementUnit;
    public ProductItemMeasurementUnit SelectedProductItemMeasurementUnit
    {
        get
        {
            return _SelectedProductItemMeasurementUnit;
        }
        set
        {
            _SelectedProductItemMeasurementUnit = value;
            ProductItem.MeasurementUnitId = value?.MeasurementUnitId;
            OnPropertyChanged(nameof(SelectedProductItemMeasurementUnit));
            OnPropertyChanged(nameof(DisplayName));
        }
    }

    private string _ProductItemMeasurementUnitText;
    public string ProductItemMeasurementUnitText
    {
        get => _ProductItemMeasurementUnitText;
        set
        {
            _ProductItemMeasurementUnitText = value;
            OnPropertyChanged(nameof(ProductItemMeasurementUnitText));
        }
    }


    /// <summary>
    /// XPS version of the FixedDocument. 
    /// </summary>
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

    #endregion Properties

    #region Constructor
    public PurchaseViewModel(ICommonService commonService, IProductService productService, IPrintingService printingService, IPaginator paginator)
    { 
        _commonService = commonService; 
        _productService = productService;
        _printingService = printingService;
        _paginator = paginator;
        ProductItem = new ProductItems(); 
        SelectedInvoiceDetails = new InvoiceDetails();
        SelectedInvoiceDetails.InvoiceDate = DateTime.Now; 
        CompanySource =  _commonService.GetCompanyDetails().Value.ToObservableCollection(); 
        ProductItemCategorySource = _commonService.GetProductItemCategory().Value.ToObservableCollection();
        ProductItemMeasurementUnitSource = _commonService.GetProductItemMeasurementUnit().Value.ToObservableCollection();
    }
    #endregion Constructor

    #region CommandMethods 
    public void SaveProducts()
    {
        var companyDetails = new CompanyDetails();
        Guid companyId = Guid.Empty;
        if (ProductItem.ProductId != null)
        {
            _productService.SaveProductItems(ProductItem);
        }
        else
        {
            if (SelectedCompany != null)
            {
                ProductItem.CompanyId = SelectedCompany.CompanyId;
            }
            else
            {
                companyDetails.CompanyName = CompanyText;
                companyId = _commonService.SaveCompanyDetails(companyDetails);
                CompanySource = _commonService.GetCompanyDetails().Value.ToObservableCollection();
                SelectedCompany = CompanySource.Where(x => x.CompanyId == companyId).FirstOrDefault();
                ProductItem.CompanyId = companyId;
            }
            if (SelectedInvoice != null)
            {
                ProductItem.InvoiceId = SelectedInvoice.InvoiceId;
            }
            else
            {
                var invoiceDetails = new InvoiceDetails()
                {
                    InvoiceNo = InvoiceText,
                    InvoiceDate = SelectedInvoiceDetails.InvoiceDate,
                    SupplierAddress = SelectedInvoiceDetails.SupplierAddress,
                    SupplierName = SelectedInvoiceDetails.SupplierName,
                    SupplierPhoneNumber = SelectedInvoiceDetails.SupplierPhoneNumber,
                    CompanyId = companyId,
                    InvoiceDisplayNumber = $"{InvoiceText} - ({SelectedInvoiceDetails.InvoiceDate:dd MMMM yyyy})"
                };

                ProductItem.InvoiceId = _commonService.SaveInvoiceDetails(invoiceDetails);
            }
            ProductItem.DisplayName = DisplayName;
            _productService.SaveProductItems(ProductItem);
        }
        InvoiceSource = _commonService.GetInvoiceDetails(ProductItem.CompanyId).Value.ToObservableCollection();
        SelectedInvoice = InvoiceSource.Where(x => ProductItem.InvoiceId == x.InvoiceId).FirstOrDefault();
        GetProductItemsByInvoice(ProductItem.InvoiceId, ProductItem.CompanyId);
        ProductItem = new();
        SelectedProductItemCategory = null;
        SelectedProductItemMeasurementUnit = null;
    }

    public void ProductSelectionChanged()
    {
        ProductItem = SelectedProductRow;
    }

    public void CompanyDDSelectionChanged()
    {
        InvoiceSource = _commonService.GetInvoiceDetails(null).Value.ToObservableCollection();
    }

    public void PrintInvoice()
    {
        Func<UIElement> reportFactory;
        reportFactory = () => new SampleTemplate();
        LoadReport(reportFactory,  CancellationToken.None); 
    }
    #endregion CommandMethods

    #region Private Methods
    private void GetProductItemsByInvoice(Guid invoiceId, Guid companyId)
    { 
        var result = _productService.GetProductsItemsDetails(invoiceId, companyId).Value; 
        ProductItemsSource = result.Products.ToObservableCollection();
    }

    private void AddNewProduct()
    {
        MessageBox.Show("Current fields will be reseted");
        ResetProductItemFields();
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

    private void ResetInvoiceField()
    {
        SelectedInvoice = null;
        SelectedInvoiceDetails = new InvoiceDetails(); 
        ResetProductItemFields();
        ResetCategoryField();
        ResetUnitField();
    }

    private void ResetProductItemFields()
    { 
        ProductItem = new ProductItems();
        ResetCategoryField();
        ResetUnitField();
    }

    private void ResetCategoryField()
    {
        SelectedProductItemCategory = null;
        ProductItemCategoryText = null; 
    }

    private void ResetUnitField()
    {
        SelectedProductItemMeasurementUnit = null;
        ProductItemMeasurementUnitText = null;
    }
    #endregion Private Methods
}
