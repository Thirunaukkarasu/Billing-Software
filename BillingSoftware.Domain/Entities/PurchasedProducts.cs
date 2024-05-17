using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Domain.Entities
{
    [Table("PurchasedProducts")]
    public class PurchasedProducts : ProductsBase
    {
        [Key]
        public Guid PurchasedProductsId { get; set; }
        public Guid ProductId { get; set; }
        public Guid PurchaseId { get; set; }
        public PurchaseOrders PurchaseOrders { get; set; } = null!; // Required reference navigation to principal 
    }
}
