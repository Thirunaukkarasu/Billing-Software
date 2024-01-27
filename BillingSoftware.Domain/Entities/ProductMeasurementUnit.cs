using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSoftware.Domain.Entities
{
    [Table("ProductMeasurementUnit")]
    public class ProductMeasurementUnit
    {
        [Key]
        public Guid MeasurementUnitId { get; set; }

        public string MeasurementUnitName { get; set; }

        public string Symbol { get; set; }
    }
}
