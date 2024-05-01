using System;

namespace BillingSoftware.Domain.Models
{
    public class CompanyDetails
    { 
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set;}
        public string CompanyDescription { get; set;} = string.Empty;
        public string CompanyEmail { get; set;}
        public string CompanyPhoneNumber { get; set;}
        public string CompanyCity { get; set;}
        public string CompanyState { get; set;}
        public string CompanyZipCode { get; set;}
        public string CompanyCountry { get; set;}
        public bool IsActive { get; set; } = true;
        public DateTime InsertDt { get; set; }
        public DateTime ModifiedDt { get; set; }
        public override string ToString()
        {
            return CompanyName;
        }
    }
}
