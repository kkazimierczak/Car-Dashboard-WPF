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
    /// Interaction logic for ExampleMultimeter.xaml
    /// </summary>
    public partial class ExampleMultimeter : UserControl
    {
        public ExampleMultimeter()
        {
            InitializeComponent();
            double resistance = Math.Round(ResistanceSlider.Value, 2);
            ResistanceIndicator.Text = resistance.ToString();
        }

        private void ResistanceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (VoltageCheck.IsChecked == true)
            {
                ResistanceSlider.Maximum = VoltageGauge.PrimaryScale.RangeEnd + CurrentGauge.PrimaryScale.RangeEnd;
                ResistanceSlider.Minimum = 2;
                valueSlider.Maximum = VoltageGauge.PrimaryScale.RangeEnd;
                double value = Math.Round(valueSlider.Value / ResistanceSlider.Value, 2);
                if (value < CurrentGauge.PrimaryScale.RangeEnd)
                    CurrentGauge.PrimaryScale.Value = valueSlider.Value / ResistanceSlider.Value;
                else
                    CurrentGauge.PrimaryScale.Value = CurrentGauge.PrimaryScale.RangeEnd;
                CurrentGauge.PrimaryScale.ValueIndicator.Text = value.ToString();
            }
            else if(CurrentCheck.IsChecked == true)
            {
                valueSlider.Maximum = CurrentGauge.PrimaryScale.RangeEnd; 
                double value = Math.Round(valueSlider.Value * ResistanceSlider.Value, 2);
                if (value < VoltageGauge.PrimaryScale.RangeEnd)
                    VoltageGauge.PrimaryScale.Value = valueSlider.Value * ResistanceSlider.Value;
                else
                    VoltageGauge.PrimaryScale.Value = VoltageGauge.PrimaryScale.RangeEnd;
                VoltageGauge.PrimaryScale.ValueIndicator.Text = value.ToString();
            }
            double resistance =  Math.Round(ResistanceSlider.Value, 2);
            ResistanceIndicator.Text = resistance.ToString();
        }

        private void valueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (VoltageCheck.IsChecked == true)
            {
                ResistanceSlider.Maximum = VoltageGauge.PrimaryScale.RangeEnd + CurrentGauge.PrimaryScale.RangeEnd;
                ResistanceSlider.Minimum = 2;
                valueSlider.Maximum = VoltageGauge.PrimaryScale.RangeEnd;
                VoltageGauge.PrimaryScale.Value = valueSlider.Value;
                double value = Math.Round(valueSlider.Value / ResistanceSlider.Value, 2);
                if (value < CurrentGauge.PrimaryScale.RangeEnd)
                    CurrentGauge.PrimaryScale.Value = valueSlider.Value / ResistanceSlider.Value;
                else
                    CurrentGauge.PrimaryScale.Value = CurrentGauge.PrimaryScale.RangeEnd;
                CurrentGauge.PrimaryScale.ValueIndicator.Text = value.ToString();
            }
            else if (CurrentCheck.IsChecked == true)
            {
                valueSlider.Maximum = CurrentGauge.PrimaryScale.RangeEnd;
                CurrentGauge.PrimaryScale.Value = valueSlider.Value;

                double value = Math.Round(valueSlider.Value * ResistanceSlider.Value, 2);
                if (value < VoltageGauge.PrimaryScale.RangeEnd)
                    VoltageGauge.PrimaryScale.Value = valueSlider.Value * ResistanceSlider.Value;
                else
                    VoltageGauge.PrimaryScale.Value = VoltageGauge.PrimaryScale.RangeEnd;
                VoltageGauge.PrimaryScale.ValueIndicator.Text = value.ToString();
            }
        }
    }
}
