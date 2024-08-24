using Billing.Repository.Imp.DBContext;
using BillingSoftware.Domain.Models;
using BillingSoftware.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BillingSoftware.Repository.CommonRepo
{
    public class ProductMeasurementRepository : IProductMeasurementRepository
    {
        private readonly POSBillingDBContext dbContext;
        public ProductMeasurementRepository(POSBillingDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<MeasurementUnitDto> GetProductMeasurementUnit()
        {
            return dbContext.ProductMeasurementUnit.Select(x => new MeasurementUnitDto()
            {
                MeasurementUnitId = x.MeasurementUnitId,
                MeasurementUnitName = x.MeasurementUnitName,
                Symbol = x.Symbol,
            }).ToList();
        }
    }
}
