using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UniversityWPF.Library.Converters
{
	public class MultiValueEqualityConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			bool areEqual = false;
			if (values.Count() > 0)
			{
				areEqual = true;
				foreach (var item in values)
				{
					if (!item.Equals(values[0]))
					{
						areEqual = false;
						break;
					}
				}
			}
			return areEqual;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
