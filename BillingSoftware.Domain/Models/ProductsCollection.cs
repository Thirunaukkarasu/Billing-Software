namespace Billing.Domain.Models
{
    public class ProductsCollection
    {
        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string CompanyName { get; set; } 
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
