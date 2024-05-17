using Billing.Domain.Models;
using Billing.Repository.Imp.DBContext;
using BillingSoftware.Domain.Entities;
using BillingSoftware.Domain.Models;
using BillingSoftware.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillingSoftware.Repository.SuppliersRepo
{
    public class SuppliersRepository : ISuppliersRepository
    {
        private readonly POSBillingDBContext _dbContext;
        public SuppliersRepository(POSBillingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid SaveSuppliersDetails(SuppliersDto suppliersDto)
        {
            var suppliers = new Suppliers()
            {
                CreatedDate = suppliersDto.CreatedDate,
                IsActive = suppliersDto.IsActive,
                ModifyDate = suppliersDto.ModifyDate,
                SupplierAddress = suppliersDto.SupplierAddress,
                SupplierName = suppliersDto.SupplierName,
                SupplierPhoneNumber = suppliersDto.SupplierPhoneNumber
            };
            _dbContext.Suppliers.Add(suppliers);
            _dbContext.SaveChanges();
            return suppliers.SupplierId;
        }

        public Guid UpdateSuppliersDetails(SuppliersDto suppliersDto)
        {
          var supplier=  _dbContext.Suppliers
                                    .Where(x => x.IsActive == true && x.SupplierId == suppliersDto.SupplierId).FirstOrDefault();

            supplier.CreatedDate = suppliersDto.CreatedDate;
            supplier.IsActive = suppliersDto.IsActive;
            supplier.ModifyDate = DateTime.Now;
            supplier.SupplierAddress = suppliersDto.SupplierAddress;
            supplier.SupplierName = suppliersDto.SupplierName;
            supplier.SupplierPhoneNumber = suppliersDto.SupplierPhoneNumber;
           
            _dbContext.Suppliers.Update(supplier);
            _dbContext.SaveChanges();
            return supplier.SupplierId;
        }

        public List<SuppliersDto> GetSuppliersDetails()
        {
            var result = _dbContext.Suppliers
                                    .Where(x => x.IsActive == true)
                                    .Select(supplier => new SuppliersDto()
                                            {
                                                CreatedDate = supplier.CreatedDate,
                                                IsActive = supplier.IsActive,
                                                ModifyDate = supplier.ModifyDate,
                                                SupplierAddress = supplier.SupplierAddress,
                                                SupplierName = supplier.SupplierName,
                                                SupplierPhoneNumber = supplier.SupplierPhoneNumber,
                                                SupplierId = supplier.SupplierId
                                            })
                                    .ToList();
            return result;
        }


    }
}
