using BillingSoftware.Domain.Entities;
using BillingSoftware.Domain.Extentions;
using BillingSoftware.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Billing.Domain.Models
{
    public class PurchasedProductsDto :  INotifyPropertyChanged
    {
        public PurchasedProductsDto()
        {
            ProductsDBSource = new List<ProductsDto>(); 
        }

        private  int      existingStock; 
        private  string   productName;
        private  string   _description;
        private  string   displayName;
        private  string   hSNCode;
        private  string   batchNumber;
        private  decimal  mRP;
        private  int      quantity;
        private  decimal  productSize;
        private  decimal  gSTPercent;
        private  decimal  cGSTPercent;
        private  decimal  purchaseDiscountPercent; 
        private  decimal  purchaseRate;
        private  decimal  salesDiscountPercent;
        private  decimal  salesRate;
        private  Guid?    purchaseId;
        private  Guid     categoryId;
        private  int      stocks;

        public Guid ProductId { get => productId; set => productId = value; }
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
                    CategoryId = value.CategoryId;
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
                if (value != null)
                {
                    _selectedMeasurementUnit = value;
                    MeasurementUnitId = value.MeasurementUnitId;
                    OnPropertyChanged(nameof(SelectedMeasurementUnit));
                }
            }
        }

        private Guid measurementUnitId;
        public Guid  MeasurementUnitId
        {
            get
            {
                return measurementUnitId;
            }
            set
            {
                measurementUnitId = value;
                OnPropertyChanged(nameof(MeasurementUnitId));
            }
        }

        private ProductsDto _SelectedProduct;
        private Guid productId;

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
                    ProductId = value.ProductId;
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
                SetIProductnStock(value); // Calculate Stocks
                OnPropertyChanged(nameof(Quantity));
                OnPropertyChanged(nameof(Stocks));
            }
        }

        private void SetIProductnStock(int value)
        {
            if (SelectedProduct != null)
            {
                Stocks = existingStock + value;
                SelectedProduct.Stocks = Stocks;
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
                if (SelectedProduct != null)
                    SelectedProduct.MRP = value;
                OnPropertyChanged(nameof(MRP));
                CalculatePurchaseRate(); 
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
                if (SelectedProduct != null)
                    SelectedProduct.GSTPercent = value;
                OnPropertyChanged(nameof(GSTPercent));
                CalculatePurchaseRate();
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
                if (SelectedProduct != null)
                    SelectedProduct.CGSTPercent = value;
                OnPropertyChanged(nameof(CGSTPercent));
                CalculatePurchaseRate();
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
                CalculatePurchaseRate();
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
                if (purchaseRate != value)
                {
                    purchaseRate = value;
                    OnPropertyChanged(nameof(PurchaseRate)); 
                }
            }
        }

        private void CalculatePurchaseRate()
        { 
            var percent = GSTPercent + CGSTPercent - PurchaseDiscountPercent;
            if (percent > 0)
            {
                var parcentage = percent / 100;
                PurchaseRate = MRP + (MRP * parcentage);
            }
            else
            {
                PurchaseRate = MRP;
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
                if(salesRate != value)  
                    salesRate = value; 

                if (SelectedProduct != null)
                    SelectedProduct.SalesRate = value;

                OnPropertyChanged(nameof(SalesRate));
            }
        }

        private void CalculateSalesRate()
        { 
             //if(SalesRate == 0)
             //   SalesRate = MRP;
              
             if (salesDiscountPercent > 0)
                   SalesRate = Math.Round(MRP - (MRP * (SalesDiscountPercent/100)),2);
             else 
                   SalesRate = MRP;
            OnPropertyChanged(nameof(SalesRate));
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
                CalculateSalesRate();
                if (SelectedProduct != null)
                    SelectedProduct.SalesDiscountPercent = value;
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

        public int Stocks
        {
            get
            {
                return stocks;
            }

            set
            {
                stocks = value;
                OnPropertyChanged(nameof(Stocks));
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
             ProductName = SelectedProduct.ProductName;
             Description = SelectedProduct.Description;
             HSNCode = SelectedProduct.HSNCode;
             BatchNumber = SelectedProduct.BatchNumber;
             //Quantity = SelectedProduct.Quantity;
             //ProductSize = SelectedProduct.ProductSize;
             MRP = SelectedProduct.MRP;
             GSTPercent = SelectedProduct.GSTPercent;
             CGSTPercent = SelectedProduct.CGSTPercent;
             //PurchaseDiscountPercent = SelectedProduct.PurchaseDiscountPercent;
             //PurchaseRate = SelectedProduct.PurchaseRate;
             SalesDiscountPercent = SelectedProduct.SalesDiscountPercent;
             SalesRate = SelectedProduct.SalesRate;
             DisplayName = SelectedProduct.DisplayName;
             SelectedMeasurementUnit = SelectedProduct.MeasurementUnit;
             CategoryId = SelectedProductCategoryRow.CategoryId;
             Stocks = SelectedProduct.Stocks;
             existingStock = SelectedProduct.Stocks;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public event PropertyChangingEventHandler PropertyChanging;
        protected void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        }
        #endregion

    }
}
