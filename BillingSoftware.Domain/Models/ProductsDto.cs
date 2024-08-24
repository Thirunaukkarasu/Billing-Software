using BillingSoftware.Domain.Extentions;
using BillingSoftware.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Billing.Domain.Models
{
    public class ProductsDto :  INotifyPropertyChanged
    {
        public ProductsDto()
        {
            ProductsDBSource = new List<ProductsDto>(); 
        }
         
        public Guid? ProductId { get; set; }
        private string productName;
        private string _description;
        private string displayName;
        private string hSNCode;
        private string batchNumber;
        private decimal mRP;
        private int quantity;
        private decimal productSize;
        private decimal gSTPercent;
        private decimal cGSTPercent;
        private decimal purchaseDiscountPercent; 
        private decimal purchaseRate;
        private decimal salesDiscountPercent;
        private decimal salesRate;
        private Guid? purchaseId;
        private Guid categoryId;

        public List<ProductsDto> ProductsDBSource { get; set; }

        private ProductCategoryDto _SelectedProductCategory;
        public ProductCategoryDto SelectedProductCategoryRow
        {
            get
            {
                return _SelectedProductCategory;
            }
            set
            {
                if (value != null && _SelectedProductCategory != value)
                {
                    _SelectedProductCategory = value;
                    LoadProducts();
                    ClearRowValues();
                    OnPropertyChanged(nameof(SelectedProductCategoryRow));
                }
            }
        }

        private void ClearRowValues()
        {
            //throw new NotImplementedException();
        }

        private ObservableCollection<ProductsDto> _cBProductsSource;
        public ObservableCollection<ProductsDto> CBProductsSource
        {
            get
            {
                return _cBProductsSource;
            }
            set
            {
                _cBProductsSource = value;
                OnPropertyChanged(nameof(CBProductsSource));
            }
        }
         
        private  MeasurementUnitDto  _selectedMeasurementUnit; 
        public  MeasurementUnitDto SelectedMeasurementUnit
        {
            get
            {
                return _selectedMeasurementUnit;
            }
            set
            {
                _selectedMeasurementUnit = value;
                OnPropertyChanged(nameof(SelectedMeasurementUnit));
            }
        }

        private ProductsDto _SelectedProduct;
       
        public ProductsDto SelectedProduct
        {
            get
            {
                return _SelectedProduct;
            }
            set
            {
                if (value != null && _SelectedProduct != value)
                {
                    _SelectedProduct = value;
                    LoadProductRow();
                    OnPropertyChanged(nameof(SelectedMeasurementUnit));
                    OnPropertyChanged(nameof(SelectedProduct));
                }
            }
        }

        public string ProductName
        {
            get 
            { 
                return productName; 
            }
            set
            {
                productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }
        public string Description
        {
            get 
            { 
                return _description; 
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public string DisplayName
        {
            get 
            { 
                return displayName; 
            }
            set
            {
                displayName = value;
                OnPropertyChanged(nameof(DisplayName));
            }
        }
        public string HSNCode
        {
            get 
            { 
                return hSNCode; 
            }
            set
            {
                hSNCode = value;
                OnPropertyChanged(nameof(HSNCode));
            }
        }
        public string BatchNumber
        {
            get 
            { 
                return batchNumber; 
            }
            set
            {
                batchNumber = value;
                OnPropertyChanged(nameof(BatchNumber));
            }
        }
        public int Quantity
        {
            get 
            { 
                return quantity; 
            }
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        public decimal ProductSize
        {
            get
            {
                return productSize;
            }

            set
            {
                productSize = value;
                OnPropertyChanged(nameof(ProductSize));
            }
        }
        public decimal MRP
        {
            get
            {
                return mRP;
            }

            set
            {
                mRP = value;
                OnPropertyChanged(nameof(MRP));
            }
        }
        public decimal GSTPercent
        {
            get
            {
                return gSTPercent;
            }

            set
            {
                gSTPercent = value;
                OnPropertyChanged(nameof(GSTPercent));
            }
        }
        public decimal CGSTPercent
        {
            get
            {
                return cGSTPercent;
            }

            set
            {
                cGSTPercent = value;
                OnPropertyChanged(nameof(CGSTPercent));
            }
        }
        public decimal PurchaseDiscountPercent
        {
            get
            {
                return purchaseDiscountPercent;
            }

            set
            {
                purchaseDiscountPercent = value;
                OnPropertyChanged(nameof(PurchaseDiscountPercent));
            }
        }
        public decimal PurchaseRate
        {
            get
            {
                return purchaseRate;
            }

            set
            {
                purchaseRate = value;
                OnPropertyChanged(nameof(PurchaseRate));
            }
        }
        public decimal SalesRate
        {
            get
            {
                return salesRate;
            }

            set
            {
                salesRate = value;
                OnPropertyChanged(nameof(SalesRate));
            }
        }

        public decimal SalesDiscountPercent
        {
            get
            {
                return salesDiscountPercent;
            }

            set
            {
                salesDiscountPercent = value;
                OnPropertyChanged(nameof(SalesDiscountPercent));
            }
        }
        public Guid? PurchaseId
        {
            get
            {
                return purchaseId;
            }

            set
            {
                purchaseId = value;
            }
        }
        public Guid CategoryId
        {
            get
            {
                return categoryId;
            }

            set
            {
                categoryId = value;
            }
        }

        public override string ToString()
        {
            return DisplayName;
        }



        #region Private Methods
        private void LoadProducts()
        {
            CBProductsSource = new ObservableCollection<ProductsDto>();
            CBProductsSource = ProductsDBSource.Where(x => x.CategoryId == SelectedProductCategoryRow.CategoryId).ToList().ToObservableCollection();
        }


        private void LoadProductRow()
        {
             ProductId = SelectedProduct.ProductId;
             Description = SelectedProduct.Description;
             hSNCode = SelectedProduct.HSNCode;
             BatchNumber = SelectedProduct.BatchNumber;
             Quantity = SelectedProduct.Quantity;
             ProductSize = SelectedProduct.ProductSize;
             MRP = SelectedProduct.MRP;
             GSTPercent = SelectedProduct.GSTPercent;
             CGSTPercent = SelectedProduct.CGSTPercent;
             PurchaseDiscountPercent = SelectedProduct.PurchaseDiscountPercent;
             PurchaseRate = SelectedProduct.PurchaseRate;
             SalesDiscountPercent = SelectedProduct.SalesDiscountPercent;
             SalesRate = SelectedProduct.SalesRate;
             DisplayName = SelectedProduct.DisplayName;
             SelectedMeasurementUnit = SelectedProduct.SelectedMeasurementUnit;
             CategoryId = SelectedProductCategoryRow.CategoryId;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Private Methods
    
    }
}
