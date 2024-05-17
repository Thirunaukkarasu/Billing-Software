using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSoftware.Domain.Entities
{
    [Table("Products")]
    public class Products : ProductsBase
    {
        public Products() { }

        [Key]
        public Guid ProductId { get; set; }
    }
}
