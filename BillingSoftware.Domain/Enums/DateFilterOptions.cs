using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Domain.Enums
{
    public enum DateFilterOptions
    {  
        All ,
        Today ,
        ThisMonth,
        SpecificDate,
        CustomDate
    }
}
