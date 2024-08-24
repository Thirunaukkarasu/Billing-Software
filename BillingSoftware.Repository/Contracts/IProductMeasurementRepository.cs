using BillingSoftware.Domain.Models;
using System.Collections.Generic;

namespace BillingSoftware.Repository.Contracts
{
    public interface IProductMeasurementRepository
    {
        public List<MeasurementUnitDto> GetProductMeasurementUnit();
    }
}
