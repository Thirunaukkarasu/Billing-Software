using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSoftware.Domain.Entities
{
    [Table("PurchaseOrders")]
    public class PurchaseOrders 
    {
        [Key]
        public Guid PurchaseId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; } 
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifyDate { get; set; } 
        public Guid ProductId { get; set; }
        public Guid SupplierId { get; set; } // Required foreign key property
        public Suppliers Supplier{ get; set; } = null!; // Required reference navigation to principal 
        public ICollection<PurchasedProducts> PurchasedProducts { get; } = new List<PurchasedProducts>();

    }
}
