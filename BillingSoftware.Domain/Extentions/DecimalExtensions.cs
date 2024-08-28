using System;

namespace BillingSoftware.Domain.Extentions
{
    public static class DecimalExtensions
    {
        public static decimal RoundToTwoDecimalPlaces(this decimal value)
        {
            return Math.Round(value, 2);
        }
    }
}
