using Billing.Repository.Imp.DBContext;
using BillingSoftware.Domain.Entities;
using BillingSoftware.Domain.Models;
using BillingSoftware.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillingSoftware.Repository.CommonRepo
{
    public class CommonRepository : ICommonRepository
    {
        private readonly POSBillingDBContext dbContext;
        public CommonRepository(POSBillingDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //public List<CompanyDetails> GetCompanyDetails()
        //{
        //    var companyDetails = new List<CompanyDetails>();
        //    foreach (var item in dbContext.Company)
        //    {
        //        companyDetails.Add(new CompanyDetails()
        //        {
        //            CompanyId = item.CompanyId,
        //            CompanyName = item.CompanyName,
        //            CompanyDescription = item.CompanyDescription,
        //            CompanyEmail = item.CompanyEmail,
        //            CompanyCity = item.CompanyCity,
        //            CompanyPhoneNumber = item.CompanyPhoneNumber,
        //            CompanyCountry = item.CompanyCountry,
        //            CompanyState = item.CompanyState,
        //            CompanyZipCode = item.CompanyZipCode,
        //            IsActive= item.IsActive, 
        //            InsertDt = item.InsertedDt
        //        });
        //    };

        //    return companyDetails;
        //}

        //public List<InvoiceDetails> GetInvoiceDetails(Guid? companyId = null)
        //{ 
        //    var invoiceDetails = new List<InvoiceDetails>();
        //    var invoices = new List<Invoice>(); 
        //    if (companyId != null)
        //    {
        //       var company = dbContext.Company
        //                               .Include(x => x.Invoice)
        //                               .Where(company => company.CompanyId == companyId).FirstOrDefault();
        //        if (company != null)
        //        {
        //            invoices.AddRange(company.Invoice);
        //        } 
        //    }
        //    else
        //    {
        //         invoices = dbContext.Invoice.ToList();   
        //    }

        //    foreach (var item in invoices)
        //    {
        //        invoiceDetails.Add(new InvoiceDetails
        //        {
        //            CompanyId = item.CompanyId,
        //            InvoiceDate = item.InvoiceDate,
        //            InvoiceDisplayNumber = item.InvoiceDisplayNumber,
        //            InvoiceId = item.InvoiceId,
        //            InvoiceNo = item.InvoiceNo,
        //            IsActive = item.IsActive,
        //            SupplierAddress = item.SupplierAddress,
        //            SupplierName    = item.SupplierName,
        //            SupplierPhoneNumber = item.SupplierPhoneNumber
        //        });  
        //    }
        //    return invoiceDetails;
        //}

        public List<InvoiceDto> GetInvoicesBySupplier(Guid supplierId)
        { 
          return dbContext.Invoice.Where(x => x.SupplierId == supplierId && x.IsActive == true)
                .Select( y => new InvoiceDto() { 
                  InvoiceDate = y.InvoiceDate,
                  InvoiceNo = y.InvoiceNo,
                  IsActive = y.IsActive,
                  PurchaseId = y.PurchaseId
                }).ToList();
        }

        //public Guid SaveCompanyDetails(CompanyDetails companyDetails)
        //{
        //    var company = new Company() {
        //        CompanyId = companyDetails.CompanyId,
        //        CompanyName = companyDetails.CompanyName,
        //        CompanyDescription = companyDetails.CompanyDescription,
        //        CompanyEmail = companyDetails.CompanyEmail,
        //        CompanyCity = companyDetails.CompanyCity,
        //        CompanyPhoneNumber = companyDetails.CompanyPhoneNumber,
        //        CompanyCountry = companyDetails.CompanyCountry,
        //        CompanyState = companyDetails.CompanyState,
        //        CompanyZipCode = companyDetails.CompanyZipCode,
        //        IsActive = companyDetails.IsActive,
        //        InsertedDt = DateTime.Now,
        //        ModifiedDt = null,
        //        Invoice = null
        //    };
        //    dbContext.Company.Add(company);
        //    dbContext.SaveChanges();
        //    return company.CompanyId;
        //}

        public Guid SaveInvoiceDetails(InvoiceDto invoiceDtls, Guid supplierId)
        {
            var purchaseOrders = new Invoice()
            {  
                InvoiceNo = invoiceDtls.InvoiceNo,
                IsActive = invoiceDtls.IsActive, 
                InvoiceDate = invoiceDtls.InvoiceDate,
                SupplierId = supplierId,
            };
            dbContext.Invoice.Add(purchaseOrders);
            dbContext.SaveChanges();
            return purchaseOrders.PurchaseId;
        }

        public void UpdateInvoiceDetails(InvoiceDto invoiceDtls)
        {
            var invoice = dbContext.Invoice.Where( x => x.PurchaseId == invoiceDtls.PurchaseId && x.IsActive).FirstOrDefault();
            invoice.InvoiceNo = invoiceDtls.InvoiceNo;
            invoice.IsActive = invoiceDtls.IsActive; 
            invoice.PurchaseId = invoiceDtls.PurchaseId;    
            dbContext.Invoice.Update(invoice);
            dbContext.SaveChanges(); 
        }

    }
}
