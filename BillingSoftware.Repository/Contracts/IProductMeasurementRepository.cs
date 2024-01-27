using BillingSoftware.Domain.Entities;
using BillingSoftware.Domain.Models;
using System.Collections.Generic;

namespace BillingSoftware.Repository.Contracts
{
    public interface IProductMeasurementRepository
    {
        public List<ProductItemMeasurementUnit> GetProductMeasurementUnit();
    }
}
