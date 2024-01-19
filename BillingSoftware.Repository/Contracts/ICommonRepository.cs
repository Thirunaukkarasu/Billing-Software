using BillingSoftware.Domain.Models;
using System;
using System.Collections.Generic;

namespace BillingSoftware.Repository.Contracts
{
    public interface ICommonRepository
    {
        List<CompanyDetails> GetCompanyDetails();
        List<InvoiceDetails> GetInvoiceDetails(Guid? companyId = null);

        void SaveCompanyDetails(CompanyDetails companyDetails);
        void SaveInvoiceDetails(InvoiceDetails invoiceDetails);
    }
}