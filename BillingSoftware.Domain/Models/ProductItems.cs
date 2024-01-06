using System;
using System.ComponentModel.DataAnnotations;

namespace Billing.Domain.Models
{
    public class ProductItems
    {
        [Key]
        public int Id { get; set; }
        public Guid ProductId { get; set; } = Guid.NewGuid();
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string DisplayName { get; set; }
        public string BatchNo { get; set; }
        public string QuantityPerUnit { get; set; }
        public string NoOfUnits { get; set; }
        public string HSNCode { get; set; }
        public string MRP { get; set; }
        public string PurchaseRate { get; set; }
        public string GSTPercent { get; set; }
        public string DiscountPercent { get; set; }

    }
}
