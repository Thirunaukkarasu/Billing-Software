using System;
using System.ComponentModel.DataAnnotations;
using Windows.Devices.Sensors;

namespace BillingSoftware.Domain.Models
{
    public class CompanyDetails
    {
        [Key]
        public int Id { get; set; } 
        public Guid CompanyId { get; set; } = Guid.NewGuid();
        public string CompanyName { get; set;}
        public string CompanyDescription { get; set;} = string.Empty;
        public string CompanyEmail { get; set;}
        public string CompanyPhoneNumber { get; set;}
        public string CompanyCity { get; set;}
        public string CompanyState { get; set;}
        public string CompanyZipCode { get; set;}
        public string CompanyCountry { get; set;}   
        public string IsActive { get; set;} 
        public override string ToString()
        {
            return CompanyName;
        }
    }
}
