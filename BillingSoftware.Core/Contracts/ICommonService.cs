using Billing.Domain.Results;
using BillingSoftware.Domain.Models;

namespace BillingSoftware.Core.Contracts
{
    public interface ICommonService
    {
        Result<List<CompanyDetails>> GetCompanyDetails();

        Result<List<InvoiceDetails>> GetInvoiceDetails(Guid? companyId = null);

        Guid SaveCompanyDetails(CompanyDetails companyDetails);

        Guid SaveInvoiceDetails(InvoiceDetails invoiceDetails);

        public Result<List<ProductItemCategory>> GetProductItemCategory();

        public Result<List<ProductItemMeasurementUnit>> GetProductItemMeasurementUnit();
    }
}