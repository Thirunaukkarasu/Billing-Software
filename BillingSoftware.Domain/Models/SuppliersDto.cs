using System; 

namespace BillingSoftware.Domain.Models
{
     
    public class SuppliersDto
    {
        public Guid? SupplierId { get; set; }
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
    }
}
