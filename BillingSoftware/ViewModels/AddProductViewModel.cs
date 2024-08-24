using BillingSoftware.Domain.Entities;
using BillingSoftware.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BillingSoftware.ViewModels;

public class AddProductViewModel : ObservableObject
{
    public AddProductViewModel()
    {
    }

    private string _ProductCategoryText;
    public string ProductCategoryText
    {
        get => _ProductCategoryText;
        set
        {
            _ProductCategoryText = value;
            //productCategoryDto.CategoryName = value;
            OnPropertyChanged(nameof(ProductCategoryText));
        }
    }

    private string _productNameText;
    public string ProductNameText
    {
        get => _productNameText;
        set
        {
            _productNameText = value;
            //_products.ProductName = value;
            OnPropertyChanged(nameof(ProductNameText));
        }
    }

    private string _description;
    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            //_products.Description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    private string _hsnCode;
    public string HSNCode
    {
        get => _hsnCode;
        set
        {
            _hsnCode = value;
            //_products.HSNCode = value;
            OnPropertyChanged(nameof(HSNCode));
        }
    }

    private string _batchNumber;
    public string BatchNumber
    {
        get => _batchNumber;
        set
        {
            _batchNumber = value;
            //_products.BatchNumber = value;
            OnPropertyChanged(nameof(BatchNumber));
        }
    }

    private int _quantity;
    public int Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            //_products.Quantity = value;
            OnPropertyChanged(nameof(Quantity));
        }
    }

    private decimal _ratePerUnit;
    public decimal RatePerUnit
    {
        //get => _products.PurchaseRate;
        set
        {
            _ratePerUnit = value;
            //_products.PurchaseRate = value;
            OnPropertyChanged(nameof(RatePerUnit));
        }
    }

    private int _productMRP;
    public int ProductMRP
    {
        get => _productMRP;
        set
        {
            _productMRP = value;
            //_products.MRP = value;
            OnPropertyChanged(nameof(ProductMRP));
        }
    }

    private decimal _gstPercent;
    public decimal GSTPercent
    {
        get => _gstPercent;
        set
        {
            _gstPercent = value;
            //_products.GSTPercent = value;
            OnPropertyChanged(nameof(GSTPercent));
        }
    }

    private decimal _cgstPercent;
    public decimal CGSTPercent
    {
        get => _cgstPercent;
        set
        {

            _cgstPercent = value;
            //_products.CGSTPercent = value;
            OnPropertyChanged(nameof(CGSTPercent));
        }
    }

    private decimal _discount;
    public decimal Discount
    {
        get => _discount;
        set
        {
            _discount = value;
            //_products.PurchaseDiscountPercent = value;
            OnPropertyChanged(nameof(Discount));
        }
    }

    private decimal _sellsRate;
    public decimal SellsRate
    {
        get => _sellsRate;
        set
        {
            _sellsRate = value;
            //_products.SalesRate = value;
            OnPropertyChanged(nameof(SellsRate));
        }
    }
}
