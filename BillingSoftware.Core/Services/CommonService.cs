using Billing.Domain.Results;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Domain.Models;
using BillingSoftware.Repository.Contracts;

namespace BillingSoftware.Core.Services
{
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository _commonRepository;
        public CommonService(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;
        }

        public Result<List<CompanyDetails>> GetCompanyDetails()
        {

            try
            {
                var result = _commonRepository.GetCompanyDetails();
                return result is null ? Result.Fail<List<CompanyDetails>>("Company details not found. Please add company") : Result.Ok(result);
            }
            catch (Exception e)
            {
                return Result.Fail<List<CompanyDetails>>("Error while reading " + e.Message);
            }
        }

        public Result<List<InvoiceDetails>> GetInvoiceDetails(Guid? companyId = null)
        { 
            try
            {
                var result = _commonRepository.GetInvoiceDetails(companyId);
                return result is null ? Result.Fail<List<InvoiceDetails>>("Invoice not found. Please add invoice") : Result.Ok(result);
            }
            catch (Exception e)
            {
                return Result.Fail<List<InvoiceDetails>>("Error while reading " + e.Message);
            }
        }
    }
}
