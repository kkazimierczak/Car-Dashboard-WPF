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
    /// Interaction logic for ExampleThermometerWithMenu.xaml
    /// </summary>
    public partial class ExampleThermometerWithMenu : UserControl
    {
        public ExampleThermometerWithMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //slider.Minimum++;
            gauge.PrimaryScale.RangeBegin++;
        }

        private void EndAddRangeButton_Click(object sender, RoutedEventArgs e)
        {
            //slider.Maximum++;
            gauge.PrimaryScale.RangeEnd++;
        }

        private void StartDecreaseRangeButton_Click(object sender, RoutedEventArgs e)
        {
            //slider.Minimum--;
            gauge.PrimaryScale.RangeBegin--;
        }

        private void EndDecreaseRangeButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            //slider.Maximum--;
            gauge.PrimaryScale.RangeEnd--;
        }
    }
}
