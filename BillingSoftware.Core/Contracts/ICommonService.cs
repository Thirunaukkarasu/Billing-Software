using Billing.Domain.Results;
using BillingSoftware.Domain.Models;

namespace BillingSoftware.Core.Contracts
{
    public interface ICommonService
    {
        //Result<List<CompanyDetails>> GetCompanyDetails();

        //Result<List<InvoiceDetails>> GetInvoiceDetails(Guid? companyId = null);

        //Guid SaveCompanyDetails(CompanyDetails companyDetails);

        void UpdateInvoiceDetails(InvoiceDto invoiceDtls);

        Guid SaveInvoiceDetails(InvoiceDto invoiceDtls, Guid supplierId);

        public Result<List<ProductCategoryDto>> GetProductCategory();

        public Guid SaveProductCategory(ProductCategoryDto productCategory);

        public List<MeasurementUnitDto>  GetMeasurementUnits();

        public List<InvoiceDto> GetInvoicesBySupplier(Guid supplierId);
    }
}