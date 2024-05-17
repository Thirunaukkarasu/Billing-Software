using BillingSoftware.Domain.Models;
using System.Collections.Generic;

namespace Billing.Domain.Models
{
    public class ProductsCollection
    {
        public CompanyDetails Company { get; set; }
        public InvoiceDto Invoice { get; set; }
        public List<ProductItems> Products { get; set; }

        public ProductsCollection()
        {
            Products = new List<ProductItems>();
        }
    }
}
