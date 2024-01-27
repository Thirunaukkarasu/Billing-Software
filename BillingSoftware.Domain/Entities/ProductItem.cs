using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSoftware.Domain.Entities
{
    [Table("ProductItem")]
    public class ProductItem
    {
        [Key]
        public Guid ProductId { get; set; } 
        public string ProductName { get; set; }
       
        public string DisplayName { get; set; }
        public string BatchNo { get; set; }
        public string QuantityPerUnit { get; set; }
        public int NoOfUnits { get; set; }
        public string HSNCode { get; set; }
        public decimal MRP { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal SalesRate { get; set; }
        public decimal GSTPercent { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime InsertedDt { get; set; } = DateTime.Now;
        public DateTime? ModifiedDt { get; set; }
        public Guid? InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
        public Guid? CategoryId { get; set; }
        public ProductCategory? productCategory { get; set; } 
        public Guid? MeasurementUnitId { get; set; } 
        public ProductMeasurementUnit? productMeasurementUnit { get; set; }
    }
}
