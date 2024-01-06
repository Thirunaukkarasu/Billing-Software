using Billing.Domain.Models;
using Billing.Repository.Imp.DBContext;
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

        public List<CompanyDetails> GetCompanyDetails()
        { 
            return dbContext.CompanyDetails.ToList();
        }

        public List<InvoiceDetails> GetInvoiceDetails(Guid? companyId = null)
        { 
            if (companyId != null)
            {
                return dbContext.InvoiceDetails.Where(x => x.CompanyId == companyId).ToList();
            }
            else
            {
                return dbContext.InvoiceDetails.ToList();
            } 
        }
    }
}
