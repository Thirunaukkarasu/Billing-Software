using BillingSoftware.Domain.Models;

namespace BillingSoftware.Core.Contracts
{
    public interface ISupplierService
    {
        public List<SuppliersDto> GetSuppliers();

        public Guid SaveSupplier(SuppliersDto supplier);

        Guid UpdateSupplier(SuppliersDto supplier);
    }
}
