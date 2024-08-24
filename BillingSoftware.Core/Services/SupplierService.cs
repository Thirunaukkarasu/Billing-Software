using BillingSoftware.Core.Contracts;
using BillingSoftware.Domain.Models;
using BillingSoftware.Repository.Contracts;

namespace BillingSoftware.Core.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISuppliersRepository _suppliersRepository;
        public SupplierService(ISuppliersRepository suppliersRepository)
        {
            _suppliersRepository = suppliersRepository;
        }
        public List<SuppliersDto> GetSuppliers()
        {
            return _suppliersRepository.GetSuppliersDetails();
        }

        public Guid SaveSupplier(SuppliersDto supplier)
        {
            return _suppliersRepository.SaveSuppliersDetails(supplier);
        }

        public Guid UpdateSupplier(SuppliersDto supplier)
        {
            return _suppliersRepository.UpdateSuppliersDetails(supplier);
        }
    }
}
