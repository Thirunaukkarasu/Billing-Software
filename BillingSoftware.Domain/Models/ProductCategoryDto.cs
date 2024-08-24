using System;

namespace BillingSoftware.Domain.Models
{
    public class ProductCategoryDto
    {
        public Guid CategoryId { get; set; } = Guid.Empty;

        public string CategoryName { get; set; }

        public string TamilCategoryName { get; set; }

        public override string ToString()
        {
            return $"{CategoryName} ({TamilCategoryName})";
        }
    }
}
