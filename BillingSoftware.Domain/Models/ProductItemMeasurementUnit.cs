using System;

namespace BillingSoftware.Domain.Models
{
    public class MeasurementUnitDto
    {
        public Guid MeasurementUnitId { get; set; }

        public string MeasurementUnitName { get; set; }

        public string Symbol { get; set; }

        public override string ToString()
        {
            return $"{MeasurementUnitName} ({Symbol})";
        }
    }
}
