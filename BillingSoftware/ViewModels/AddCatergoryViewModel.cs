using BillingSoftware.Contracts.Services;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace BillingSoftware.ViewModels;

public class AddCatergoryViewModel : ObservableObject
{
    private readonly ICommonService _commonService;

    private string _categoryName;
    
    private ProductCategoryDto _productCategoryDto;

    private ICommand _addCategoryCommand;
    public ICommand AddCategoryCommand => _addCategoryCommand ??= new RelayCommand(AddProductCategory);

    public string CategoryName
    {
        get { return _categoryName; }
        set { _categoryName = value; _productCategoryDto.CategoryName = value; }
    }
    public AddCatergoryViewModel(ICommonService commonService)
    {
        _productCategoryDto = new ProductCategoryDto();
        _commonService = commonService; 
    }

    private void AddProductCategory() 
    {
        _commonService.SaveProductCategory(_productCategoryDto);
    }
}

