using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GaugesWpfDemoApp
{
    /// <summary>
    /// Interaction logic for ExampleThermometer.xaml
    /// </summary>
    public partial class ExampleThermometer : UserControl
    {
        public ExampleThermometer()
        {
            InitializeComponent();
        }
    }

    [ValueConversion(typeof(double), typeof(double))]
    public class FarenheitToCelciusConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType,
               object parameter, System.Globalization.CultureInfo culture)
        {
            string sourceValue = value.ToString();
            double decimalValue = 0;
            if (Double.TryParse(sourceValue, out decimalValue))
            {
                return (decimalValue - 32.0) * (5.0 / 9.0);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType,
               object parameter, System.Globalization.CultureInfo culture)
        {
            string sourceValue = value.ToString();
            double decimalValue = 0;
            if (Double.TryParse(sourceValue, out decimalValue))
            {
                return (decimalValue * (9.0 / 5.0)) + 32.0;
            }
            return value;
        }
        #endregion
    }
}
