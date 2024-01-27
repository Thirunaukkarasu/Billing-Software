using BillingSoftware.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Repository.Contracts
{
    public interface IProductCategoryRepository
    {
        public List<ProductItemCategory> GetProductItemCategory();
    }
}
