using BillingSoftware.Contracts.Services;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;

namespace BillingSoftware.ViewModels;

public class AddCatergoryViewModel : ObservableObject
{
    private readonly ICommonService _commonService;
    private IDialogService _dialogService;
    private string _categoryName;
    private ProductCategoryDto _productCategoryDto;
    private ICommand _addCategoryCommand;
    private ICommand _resetFieldsCommand;
    private ICommand _cancelCommand;


    public ICommand AddCategoryCommand => _addCategoryCommand ??= new RelayCommand(AddProductCategory);
    public ICommand ResetFieldsCommand => _resetFieldsCommand ??= new RelayCommand(ResetCategoryFields);
    public ICommand CancelCommand => _cancelCommand ??= new RelayCommand(CloseWindow);

   
    public string CategoryName
    {
        get { return _categoryName; }
        set 
        { 
            _categoryName = value; 
            _productCategoryDto.CategoryName = value;
            OnPropertyChanged(nameof(CategoryName));
        }
    }

    private string _categoryCode;
    public string CategoryCode
    {
        get { return _categoryCode; }
        set { _categoryCode = value; _productCategoryDto.CategoryCode = value;
            OnPropertyChanged(nameof(CategoryCode));
        }
    }

    private string _localCategoryName;
    public string LocalCategoryName
    {
        get { return _localCategoryName; }
        set { _localCategoryName = value; _productCategoryDto.LocalCategoryName = value; OnPropertyChanged(nameof(LocalCategoryName)); }
    }

    public AddCatergoryViewModel(ICommonService commonService, IDialogService dialogService)
    {
        _productCategoryDto = new ProductCategoryDto();
        _commonService = commonService;
        _dialogService = dialogService;
    }

    #region Private Methods
    private void AddProductCategory()
    {
        var IsExists = CheckCategoryExists();
        if (IsExists)
        {
            MessageBox.Show("Category Code already exists. Please try different code.!!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        } 
        var result = _commonService.SaveProductCategory(_productCategoryDto);
        if (result.Success)
        {
            MessageBox.Show("New category added successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
        {
            MessageBox.Show(result.Error, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool CheckCategoryExists()
    {
        var categories = _commonService.GetProductCategory();
        if (categories.Success && categories.Value.Any())
        {
            if (categories.Value.Exists(x => x.CategoryCode?.ToLower() == _productCategoryDto.CategoryCode.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private void ResetCategoryFields()
    {
        _productCategoryDto = new();
        CategoryCode = string.Empty;
        CategoryName = string.Empty;
        LocalCategoryName = string.Empty;
    }
    private void CloseWindow()
    {
        _dialogService.CloseDialog();
    }
    #endregion Private Methods

}

