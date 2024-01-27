using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSoftware.Domain.Entities
{
    [Table(nameof(Invoice))]
    public class Invoice
    {
        [Key]
        public Guid InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceDisplayNumber { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierPhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime InsertedDt { get; set; } = DateTime.Now;
        public DateTime? ModifiedDt { get; set; }
        public Guid CompanyId { get; set; }
        public Company? Company { get; set;} 
        public ICollection<ProductItem>? ProductItems { get; set;}
    }
}
