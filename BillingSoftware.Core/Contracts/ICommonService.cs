using Billing.Domain.Results;
using BillingSoftware.Domain.Models;

namespace BillingSoftware.Core.Contracts
{
    public interface ICommonService
    {
        Result<List<CompanyDetails>> GetCompanyDetails();

        Result<List<InvoiceDetails>> GetInvoiceDetails(Guid? companyId = null);
    }
}