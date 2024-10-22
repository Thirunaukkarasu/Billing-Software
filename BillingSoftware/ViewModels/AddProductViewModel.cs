using Billing.Domain.Models;
using BillingSoftware.Contracts.Services;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Services;
using BillingSoftware.Domain.Extentions;
using BillingSoftware.Domain.Models;
using BillingSoftware.Services;
using BillingSoftware.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BillingSoftware.ViewModels;

public class AddProductViewModel : ObservableObject
{
    private readonly ICommonService _commonService;
    public ICommand AddCategoryCommand { get; set; }

    private IDialogService _dialogService;

    private AddCatergoryViewModel _addCatergoryViewModel;

    private ProductsDto _productDto;

    private IProductService _productService;
    
    private ICommand _saveProductCommand;
    public ICommand SaveProductCommand => _saveProductCommand ??= new RelayCommand(SaveProduct); 

    public AddProductViewModel(ICommonService commonService, IDialogService dialogService, AddCatergoryViewModel addCatergoryViewModel, IProductService productService)
    {
        _productDto = new ProductsDto();
        _commonService = commonService;
        _dialogService = dialogService;
        _productService = productService;
        _addCatergoryViewModel = addCatergoryViewModel;
        AddCategoryCommand = new RelayCommand(OpenAddProductCategoryDialog);
        ProductCategorySource = _commonService.GetProductCategory().Value.ToObservableCollection();
        MeasurementUnitSource = _commonService.GetMeasurementUnits().ToObservableCollection();
    }

    private void SaveProduct()
    {
        var productId = _productService.SaveProduct(_productDto);
        MessageBox.Show("New Product added Successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information); 
    }

    private bool isAutoGenerateProductName;
    public bool IsAutoGenerateProductName
    {
        get
        {
            return isAutoGenerateProductName;
        }
        set
        {
            if (value == true)
            {
                isAutoGenerateProductName = true;
                AutoGenerateProductName();
            }
            else
            {
                isAutoGenerateProductName = false; 
                ProductName = string.Empty;
            }
            OnPropertyChanged(nameof(IsAutoGenerateProductName));
        }
    }

    private bool isAutoGenerateProductCode;
    public bool IsAutoGenerateProductCode
    {
        get 
        {
            return isAutoGenerateProductCode;
        }
        set
        {
            if (value == true)
            {
                isAutoGenerateProductCode = true;
                AutoGenerateProductCode();
            }
            else
            {
                isAutoGenerateProductCode = false;
            }
            OnPropertyChanged(nameof(isAutoGenerateProductCode));
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

    private MeasurementUnitDto _selectedMeasurementUnit;
    public MeasurementUnitDto SelectedMeasurementUnit
    {
        get
        {
            return _productDto.MeasurementUnit;
        }
        set
        {
            _productDto.MeasurementUnit = value;
            OnPropertyChanged(nameof(SelectedMeasurementUnit));
        }
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
            return _productDto.Category;
        }
        set
        {
            if (value != null)
            {
                _productDto.Category = value;
                _productDto.CategoryId = value.CategoryId;
                OnPropertyChanged(nameof(SelectedProductCategory));
            }
        }
    }

    private string _productName;
    public string ProductName
    {
        get => _productDto.ProductName;
        set
        {
            _productName = value;
            _productDto.ProductName = value;
            OnPropertyChanged(nameof(ProductName));
        }
    }
    
    private string _productCode;
    public string ProductCode
    {
        get => _productDto.ProductCode;
        set
        { 
            _productDto.ProductCode = value;
            OnPropertyChanged(nameof(ProductCode));
        }
    }

    private string _description;
    public string Description
    {
        get => _productDto.Description;
        set
        { 
            _productDto.Description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    private string _brand;
    public string Brand
    {
        get => _productDto.Brand;
        set
        { 
            _productDto.Brand = value;
            OnPropertyChanged(nameof(Brand));
        }
    }

    private string _hsnCode;
    public string HSNCode
    {
        get => _productDto.HSNCode;
        set
        { 
            _productDto.HSNCode = value;
            OnPropertyChanged(nameof(HSNCode));
        }
    }

    private string _batchNumber;
    public string BatchNumber
    {
        get => _productDto.BatchNumber;
        set
        {
            _batchNumber = value;
            _productDto.BatchNumber = value;
            OnPropertyChanged(nameof(BatchNumber));
        }
    }

    private decimal _productSize;
    public decimal ProductSize
    {
        get
        {
            return _productDto.ProductSize;
        }

        set
        {
            _productDto.ProductSize = value;
            OnPropertyChanged(nameof(ProductSize));
        }
    }
     
    private decimal _productMRP;
    public decimal ProductMRP
    {
        get => _productDto.MRP;
        set
        {
            _productMRP = value;
            _productDto.MRP = value;
            OnPropertyChanged(nameof(ProductMRP));
        }
    }

    private decimal _salesPrice;
    public decimal SalesPrice
    {
        get => _productDto.SalesRate;
        set
        {
            _salesPrice = value;
            _productDto.SalesRate = value;
            OnPropertyChanged(nameof(SalesPrice));
        }
    }

    private decimal? _wholeSalePrice;
    public decimal? WholeSalePrice
    {
        get => _productDto.WholeSalePrice;
        set
        {
            _wholeSalePrice = value;
            _productDto.WholeSalePrice = value;
            OnPropertyChanged(nameof(WholeSalePrice));
        }
    }

    private int? _minimumQuatityForWS;
    public int?  MinimumQuatityForWS
    {
        get => _productDto.MinimumQuatityForWS;
        set
        {
            _minimumQuatityForWS = value;
            _productDto.MinimumQuatityForWS = value;
            OnPropertyChanged(nameof(MinimumQuatityForWS));
        }
    }

    private int _quantity;
    public int Quantity
    {
        get => _productDto.Quantity;
        set
        {
            _quantity = value;
            _productDto.Quantity = value;
            OnPropertyChanged(nameof(Quantity));
        }
    }

    private int _stocks;
    public int Stocks
    {
        get => _productDto.Stocks;
        set
        {
            _stocks = value;
            _productDto.Stocks = value;
            OnPropertyChanged(nameof(Stocks));
        }
    }

    private decimal _gstPercent;
    public decimal GSTPercent
    {
        get => _productDto.GSTPercent;
        set
        {
            _gstPercent = value;
            _productDto.GSTPercent = value;
            OnPropertyChanged(nameof(GSTPercent));
        }
    }

    private decimal _cgstPercent;
    public decimal CGSTPercent
    {
        get => _productDto.CGSTPercent;
        set
        {

            _cgstPercent = value;
            _productDto.CGSTPercent = value;
            OnPropertyChanged(nameof(CGSTPercent));
        }
    }

    private decimal _sgstPercent;
    public decimal SGSTPercent
    {
        get => _productDto.SGSTPercent;
        set
        {

            _sgstPercent = value;
            _productDto.SGSTPercent = value;
            OnPropertyChanged(nameof(CGSTPercent));
        }
    }

    private decimal _discount;
    public decimal Discount
    {
        get => _productDto.SalesDiscountPercent;
        set
        {
            _discount = value;
            _productDto.SalesDiscountPercent = value;
            OnPropertyChanged(nameof(Discount));
        }
    }

    private decimal? _priceWithTax;
    public decimal? PriceWithTax
    {
        get => _productDto.PriceWithTax;
        set
        {
            _priceWithTax = value;
            _productDto.PriceWithTax = value;
            OnPropertyChanged(nameof(PriceWithTax));
        }
    }

    #region Private Methods
    private void OpenAddProductCategoryDialog()
    {
        _dialogService.ShowDialog<CategoryDialog>(_addCatergoryViewModel);
        ProductCategorySource = _commonService.GetProductCategory().Value.ToObservableCollection();
    }

    public void AutoGenerateProductName()
    {
        if (!string.IsNullOrWhiteSpace(Brand) && !string.IsNullOrWhiteSpace(SelectedProductCategory.CategoryName)
            && !string.IsNullOrWhiteSpace(SelectedMeasurementUnit.MeasurementUnitName))
        {
            ProductName = $"{Brand} {SelectedProductCategory.CategoryName} {ProductSize} {SelectedMeasurementUnit.MeasurementUnitName}";
        }
    }

    public void AutoGenerateProductCode()
    {
        Random random = new Random();
        ProductCode = new string(Enumerable.Range(0, 5)
                               .Select(_ => (char)random.Next('A', 'Z' + 1))
                               .ToArray());
    }
    #endregion Private Methods
}
