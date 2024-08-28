 using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSoftware.Domain.Models
{
     
    public class InvoiceDto
    { 
        public Guid PurchaseId { get; set; } = Guid.Empty;
        public string? InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceDisplayNumber { get; set; }
        public bool IsActive { get; set; } = true; 
        public decimal TotalPurchaseAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Balance { get; set; }

        public override string ToString()
        {
            return InvoiceNo;
        }
    }
}
