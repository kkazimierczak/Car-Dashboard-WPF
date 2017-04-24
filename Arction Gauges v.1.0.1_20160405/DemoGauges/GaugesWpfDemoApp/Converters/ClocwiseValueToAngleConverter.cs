using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace GaugesWpfDemoApp
{
    public class ClocwiseValueToAngleConverter: IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double k = 0;
            double angle = 0;
            if(double.TryParse(parameter.ToString(), out angle))
            {
                k = -2.25;
            }
            return k * (double)value + angle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
