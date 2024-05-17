namespace BillingSoftware.Domain.Entities
{
    public abstract class ProductsBase
    { 
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
    }
}
