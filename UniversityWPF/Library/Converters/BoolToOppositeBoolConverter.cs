using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UniversityWPF.Library.Converters
{
	public class BoolToOppositeBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
			{
				throw new ArgumentException("Value isn't bool");
			}

			return !(bool)value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
			{
				throw new ArgumentException("Value isn't bool");
			}

			return !(bool)value;
		}
	}
}
