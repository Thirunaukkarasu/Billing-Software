using System;

namespace BillingSoftware.Domain.Entities
{
    public abstract class ProductsBase
    { 
        public string ProductName { get; set; } 
        
        public string ProductDescription { get; set; }
        
        public string Brand { get; set; }
        
        public string DisplayName { get; set; }

        public string HSNCode { get; set; }

        public string BatchNumber { get; set; }

        public string ProductCode { get; set; }

        public decimal? PriceWithTax { get; set; }

        public int Quantity { get; set; }
         
        public decimal ProductSize { get; set; } 

        public decimal MRP { get; set; }

        public decimal SalesRate { get; set; }

        public decimal? WholeSalePrice { get; set; }
        
        public int? MinimumQuatityForWS { get; set; }

        public decimal GSTPercent { get; set; }

        public decimal SGSTPercent { get; set; }

        public decimal CGSTPercent { get; set; }

        public decimal PurchaseDiscountPercent { get; set; }

        public decimal PurchaseRate { get; set; }

        public decimal SalesDiscountPercent { get; set; } 

        public DateTime? CreatedDate { get; set; }
        
        public DateTime? ModifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
    }
}
