using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSoftware.Domain.Entities
{
    [Table("ProductCategory")]
    public class ProductCategory
    {
        [Key]
        public Guid CategoryId { get; set; }
        
        public string CategoryName { get; set; }

        public string LocalCategoryName { get; set; }

        public string CategoryCode { get; set; }
    }
}
