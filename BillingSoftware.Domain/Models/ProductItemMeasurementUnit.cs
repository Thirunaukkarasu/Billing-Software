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

        public override bool Equals(object obj)
        {
            if (obj is MeasurementUnitDto measurementUnit)
            {
                return MeasurementUnitId == measurementUnit.MeasurementUnitId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return MeasurementUnitId.GetHashCode();
        }
    }
}
