using BillingSoftware.Domain.Models;
using System;
using System.Collections.Generic;

namespace BillingSoftware.Repository.Contracts
{
    public interface ISuppliersRepository
    {
        Guid SaveSuppliersDetails(SuppliersDto suppliersDto);
        List<SuppliersDto> GetSuppliersDetails();

        Guid UpdateSuppliersDetails(SuppliersDto suppliersDto);
    }
}
