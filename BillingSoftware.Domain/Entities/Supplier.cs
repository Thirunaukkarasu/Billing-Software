using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSoftware.Domain.Entities
{
    [Table("Suppliers")]
    public class Supplier
    {
        [Key]
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierPhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifyDate { get; set; }
        public ICollection<Invoice> PurchaseOrders { get; } = new List<Invoice>();
    }
}
