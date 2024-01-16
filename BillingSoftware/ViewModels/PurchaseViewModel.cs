using Billing.Domain.Models;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Contracts.Services;
using BillingSoftware.Core.Services;
using BillingSoftware.Domain.Extentions;
using BillingSoftware.Domain.Models;
using BillingSoftware.PrintTemplates;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
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
            OnPropertyChanged("ProductItems");
        }
    }
    public ProductItems SelectedProductRow
    { 
        get => _selectedItem;
        set
        {
            SetProperty(ref _selectedItem, value); 
            OnPropertyChanged("SelectedProductRow");
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
            //selected company
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
            _SelectedInvoice = value;
            GetProductItemsByInvoice(value.InvoiceId, SelectedCompany.CompanyId);
            ProductItem = ProductItemsSource.FirstOrDefault();
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
            OnPropertyChanged("ProductItemsSource");
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
        //ProductItemsSource = new ObservableCollection<ProductItems>();
        ProductItem = new ProductItems(); 
        CompanySource =  _commonService.GetCompanyDetails().Value.ToObservableCollection(); 
    }
    #endregion Constructor

    #region CommandMethods 
    public void SaveProducts()
    {
        bool IsNewCompany = false;
        if(SelectedCompany != null)
        { 
            ProductItem.CompanyId = SelectedCompany.CompanyId;
        }
        if(SelectedInvoiceDetails != null)
        {
            ProductItem.InvoiceId = SelectedInvoiceDetails.InvoiceId;
        } 
        _productService.SaveProductItems(ProductItem);
        ProductItem = new();
        GetProductItemsByInvoice(SelectedInvoice.InvoiceId, SelectedCompany.CompanyId);
    }

    public void ProductSelectionChanged()
    {
       
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

    private void LoadReport(Func<UIElement> reportFactory, CancellationToken cancellationToken)
    {
        var printTicket = _printingService.GetPrintTicket("Microsoft Print to PDF", new PageMediaSize(PageMediaSizeName.NorthAmericaLetter, 816, 1056), PageOrientation.Portrait);
        var printCapabilities = _printingService.GetPrinterCapabilitiesForPrintTicket(printTicket, "Microsoft Print to PDF");

        if (printCapabilities.OrientedPageMediaWidth.HasValue && printCapabilities.OrientedPageMediaHeight.HasValue)
        {
            var pageSize = new Size(printCapabilities.OrientedPageMediaWidth.Value, printCapabilities.OrientedPageMediaHeight.Value);

            var desiredMargin = new Thickness(15);
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
    #endregion Private Methods
}
