using Billing.Domain.Results;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Domain.Models;
using BillingSoftware.Repository.CommonRepo;
using BillingSoftware.Repository.Contracts;
using System.ComponentModel.Design;

namespace BillingSoftware.Core.Services
{
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductMeasurementRepository _productMeasurementRepository;
        public CommonService(ICommonRepository commonRepository, IProductCategoryRepository productCategoryRepository, IProductMeasurementRepository productMeasurementRepository)
        {
            _commonRepository = commonRepository;
            _productCategoryRepository = productCategoryRepository;
            _productMeasurementRepository = productMeasurementRepository;
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

        public Guid SaveCompanyDetails(CompanyDetails companyDetails)
        {
           return _commonRepository.SaveCompanyDetails(companyDetails); 
        }

        public Guid SaveInvoiceDetails(InvoiceDetails invoiceDetails)
        {
            return _commonRepository.SaveInvoiceDetails(invoiceDetails); 
        }

        public Result<List<ProductItemCategory>> GetProductItemCategory()
        {

            try
            {
                var result = _productCategoryRepository.GetProductItemCategory();
                return result is null ? Result.Fail<List<ProductItemCategory>>("Category not fount. Please add Category") : Result.Ok(result);
            }
            catch (Exception e)
            {
                return Result.Fail<List<ProductItemCategory>>("Error while reading " + e.Message);
            }
        }

        public Result<List<ProductItemMeasurementUnit>> GetProductItemMeasurementUnit()
        {

            try
            {
                var result = _productMeasurementRepository.GetProductMeasurementUnit();
                return result is null ? Result.Fail<List<ProductItemMeasurementUnit>>("Measurement Units not fount. Please add Category") : Result.Ok(result);
            }
            catch (Exception e)
            {
                return Result.Fail<List<ProductItemMeasurementUnit>>("Error while reading " + e.Message);
            }
        }
    }
}
