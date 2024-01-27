using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Domain.Models
{
    [Table("ProductItem")]
    public class ProductItems
    {
        [Key]
        public Guid? ProductId { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid CompanyId { get; set; }
        public string ProductName { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? MeasurementUnitId { get; set; }
        public string DisplayName { get; set; }
        public string BatchNo { get; set; }
        public string QuantityPerUnit { get; set; }
        public string NoOfUnits { get; set; }
        public string HSNCode { get; set; }
        public string MRP { get; set; }
        public string PurchaseRate { get; set; }
        public string SalesRate { get; set; }
        public string GSTPercent { get; set; }
        public string DiscountPercent { get; set; }
    }
}
