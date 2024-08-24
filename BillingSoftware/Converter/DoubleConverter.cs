using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Converter
{
	public class DoubleConverter : IValueConverter
	{ 

		public object Convert(object value,
							  Type targetType,
							  object parameter,
							  System.Globalization.CultureInfo culture)
		{
			if (value is Double)
			{
				double a = (Double)value;
				if (a == 0.0)
				{
					return String.Empty;
				}
				else
				{
					return a;
				}
			}
			return "Error";
		}

		public object ConvertBack(object value,
								  Type targetType,
								  object parameter,
								  System.Globalization.CultureInfo culture)
		{

			if (string.IsNullOrEmpty(value.ToString()))
				value = 0;
			return value;
		} 
	}
