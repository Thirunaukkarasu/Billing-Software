using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSoftware.Domain.Entities
{
    //Represents the purchase order details with supplier and invoice
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        public Guid PurchaseId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; } 
        public bool IsActive { get; set; } 
        public decimal TotalPurchaseAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifyDate { get; set; }  
        public Guid SupplierId { get; set; } //Required foreign key property
        public Supplier Supplier{ get; set; } = null!; //Required reference navigation to principal 
        public ICollection<PurchasedProduct> PurchasedProducts { get; } = new List<PurchasedProduct>();

    }
}
