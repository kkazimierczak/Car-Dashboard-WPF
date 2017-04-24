using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace GaugeSilverlightDemoApp
{
    public class ClocwiseValueToAngleConverter: IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double angle = 0;
            if (!double.TryParse(parameter.ToString(), out angle))
            {
                // error
                //angle should be = 270
            }
            return -2.25 * (double)value + angle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
