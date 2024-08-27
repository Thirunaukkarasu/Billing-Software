using System;

namespace BillingSoftware.Domain.Models
{
    public class ProductCategoryDto
    {
        public Guid CategoryId { get; set; } 

        public string CategoryName { get; set; }

        public string TamilCategoryName { get; set; }

        public override string ToString()
        {
            return $"{CategoryName} ({TamilCategoryName})";
        }

        public override bool Equals(object obj)
        {
            if (obj is ProductCategoryDto category)
            {
                return CategoryId == category.CategoryId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return CategoryId.GetHashCode();
        }
    }
}
