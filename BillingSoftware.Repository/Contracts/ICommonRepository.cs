using BillingSoftware.Domain.Models;
using System;
using System.Collections.Generic;

namespace BillingSoftware.Repository.Contracts
{
    public interface ICommonRepository
    {
        //List<CompanyDetails> GetCompanyDetails();
        //List<InvoiceDetails> GetInvoiceDetails(Guid? companyId = null);
        void UpdateInvoiceDetails(InvoiceDto invoiceDtls);
        List<InvoiceDto> GetInvoicesBySupplier(Guid supplierId);

        //Guid SaveCompanyDetails(CompanyDetails companyDetails);
        Guid SaveInvoiceDetails(InvoiceDto invoiceDtls, Guid supplierId);
    }
}