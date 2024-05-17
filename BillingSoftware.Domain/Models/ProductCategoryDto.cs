using System;

namespace BillingSoftware.Domain.Models
{
    public class ProductCategoryDto
    {
        public Guid? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public override string ToString()
        {
            return CategoryName;
        }
    }
}
