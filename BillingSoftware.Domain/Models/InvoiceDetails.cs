﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSoftware.Domain.Models
{
     
    public class InvoiceDetails
    { 
        public Guid InvoiceId { get; set; }
        public Guid CompanyId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceDisplayNumber { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierPhoneNumber { get; set; }
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return $"{InvoiceNo} - ({InvoiceDate:dd MMMM yyyy})";
        }
    }
}
