using Billing.Domain.Results;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Domain.Entities;
using BillingSoftware.Domain.Models;
using BillingSoftware.Repository.Contracts;

namespace BillingSoftware.Core.Services
{
    public class CommonService : ICommonService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductMeasurementRepository _productMeasurementRepository;
        public CommonService(IInvoiceRepository invoiceRepository, IProductCategoryRepository productCategoryRepository, IProductMeasurementRepository productMeasurementRepository)
        {
            _invoiceRepository = invoiceRepository;
            _productCategoryRepository = productCategoryRepository;
            _productMeasurementRepository = productMeasurementRepository;
        }

        //public Result<List<CompanyDetails>> GetCompanyDetails()
        //{

        //    try
        //    {
        //        var result = _commonRepository.GetCompanyDetails();
        //        return result is null ? Result.Fail<List<CompanyDetails>>("Company details not found. Please add company") : Result.Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return Result.Fail<List<CompanyDetails>>("Error while reading " + e.Message);
        //    }
        //}

        //public Result<List<InvoiceDetails>> GetInvoiceDetails(Guid? companyId = null)
        //{ 
        //    try
        //    {
        //        var result = _commonRepository.GetInvoiceDetails(companyId);
        //        return result is null ? Result.Fail<List<InvoiceDetails>>("Invoice not found. Please add invoice") : Result.Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return Result.Fail<List<InvoiceDetails>>("Error while reading " + e.Message);
        //    }
        //}

        public Guid SaveInvoiceDetails(InvoiceDto invoiceDtls, Guid supplierId)
        {
            return _invoiceRepository.SaveInvoiceDetails(invoiceDtls, supplierId);
        }
        public void UpdateInvoiceDetails(InvoiceDto invoiceDtls)
        { 
            _invoiceRepository.UpdateInvoiceDetails(invoiceDtls); 
        }
        public List<InvoiceDto> GetInvoicesBySupplier(Guid supplierId)
        {
            return _invoiceRepository.GetInvoicesBySupplier(supplierId);
        } 
        public List<InvoiceDto> GetAllInvoicesDetails()
        {
            return _invoiceRepository.GetInvoices();
        }


        //public Guid SaveCompanyDetails(CompanyDetails companyDetails)
        //{
        //    return _commonRepository.SaveCompanyDetails(companyDetails);
        //}

        //public Guid SaveInvoiceDetails(InvoiceDetails invoiceDetails)
        //{
        //    return _commonRepository.SaveInvoiceDetails(invoiceDetails); 
        //}

        public Result<List<ProductCategoryDto>> GetProductCategory()
        {

            try
            {
                var result = _productCategoryRepository.GetProductCategory();
                return result is null ? Result.Fail<List<ProductCategoryDto>>("Category not fount. Please add Category") : Result.Ok(result);
            }
            catch (Exception e)
            {
                return Result.Fail<List<ProductCategoryDto>>("Error while reading " + e.Message);
            }
        }

        public Result<Guid> SaveProductCategory(ProductCategoryDto productCategory)
        {
            try
            {
               var categoryId = _productCategoryRepository.SaveProductCategory(productCategory);
               return categoryId == Guid.Empty ? Result.Fail<Guid>("Error while adding category. Please try again.!!") : Result.Ok(categoryId);

            }
            catch (Exception e)
            {
                return Result.Fail<Guid>("Error while adding category. Please try again.!!");
            }
        }

        public List<MeasurementUnitDto>  GetMeasurementUnits()
        {    
            var result = _productMeasurementRepository.GetProductMeasurementUnit();
            return result;
             
        } 
    }
}
