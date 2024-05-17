using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSoftware.Domain.Models
{
     
    public class InvoiceDto
    { 
        public Guid? InvoiceId { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public string? InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceDisplayNumber { get; set; }
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return InvoiceNo;
        }
    }
}
