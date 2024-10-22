using BillingSoftware.Domain.Entities;
using System; 

namespace BillingSoftware.Domain.Models
{
     
    public class SuppliersDto
    {
        public Guid SupplierId { get; set; } = Guid.Empty;
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierPhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifyDate { get; set; }

        public override string ToString()
        {
            return SupplierName;
        }
          
        public override bool Equals(object obj)
        {
            if (obj is SuppliersDto supplier)
            {
                return SupplierId == supplier.SupplierId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return SupplierId.GetHashCode();
        }
    }
}
