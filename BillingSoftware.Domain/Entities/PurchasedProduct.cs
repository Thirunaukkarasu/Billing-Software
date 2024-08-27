using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSoftware.Domain.Entities
{
    //Represents the purchased product items with productId
    [Table("PurchasedProducts")]
    public class PurchasedProduct : ProductsBase
    {
        [Key]
        public Guid PurchasedProductId { get; set; }

        public Guid ProductId { get; set; }

        public Guid PurchaseId { get; set; }
         
        public Guid CategoryId { get; set; } // Required foreign key property

        public Guid MeasurementUnitId { get; set; } // Required foreign key property

        public ProductCategory Category { get; set; }

        public ProductMeasurementUnit MeasurementUnit { get; set; }

        public Invoice Invoice { get; set; } = null!; // Required reference navigation to principal 
    }
}
