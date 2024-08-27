using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSoftware.Domain.Entities
{
    //Represents the product items (over all items)
    [Table("Product")]
    public class Product : ProductsBase
    {
        public Product() { }

        [Key]
        public Guid ProductId { get; set; }

        public Guid CategoryId { get; set; } // Required foreign key property

        public int Stocks { get; set; }

        public Guid MeasurementUnitId { get; set; }

        public ProductCategory Category { get; set; }

        public ProductMeasurementUnit MeasurementUnit { get; set; }

    }
}
