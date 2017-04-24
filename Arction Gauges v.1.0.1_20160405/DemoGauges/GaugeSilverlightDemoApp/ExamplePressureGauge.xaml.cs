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

using Arction.Gauges.Extensions;
using Arction.Gauges;
using Arction.DProp;
using System.IO;
using System.Windows.Resources;
using Arction.Gauges.Ticks;
using System.Reflection;
using sio = System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Media.Effects;
using Arction.Gauges.Geo;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace GaugesWpfDemoApp
{
    /// <summary>
    /// Interaction logic for ExamplePressureGauge.xaml
    /// </summary>
    public partial class ExamplePressureGauge : UserControl
    {
        private Color c1 = Color.FromArgb(255, 251, 10, 10);
        private Color c2 = Color.FromArgb(255, 255, 255, 0);
        private Color c3 = Color.FromArgb(255, 116, 255, 0);
        private Color c;
        private double m_dpumpLimit;
        private DoubleAnimation pumpAnim;
        public ExamplePressureGauge()
        {
            InitializeComponent();
            PresureGauge.SecondaryScales[3].AngleEnd = -30;
            PresureGauge.SecondaryScales[3].AngleBegin = 270;
            PresureGauge.SecondaryScales[2].AngleBegin = 30;
            PresureGauge.SecondaryScales[2].AngleEnd = -30;
            PresureGauge.SecondaryScales[0].AngleBegin = 270;
            PresureGauge.SecondaryScales[0].AngleEnd = 140;

            slider.ValueChanged+=slider_ValueChanged;

            pumpLimit.Text = "3,5";
        }

        private void IndicatorCheck(double value)
        {
            if (slider.Value >= PresureGauge.SecondaryScales[2].RangeBegin && slider.Value <= PresureGauge.SecondaryScales[2].RangeEnd)
                c = c1;
            else if (slider.Value >= PresureGauge.SecondaryScales[1].RangeBegin && slider.Value <= PresureGauge.SecondaryScales[1].RangeEnd)
                c = c2;
            else
                c = c3;

            alarm.Fill = new SolidColorBrush(c);
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            IndicatorCheck(e.NewValue);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool parsed = double.TryParse(pumpLimit.Text, out m_dpumpLimit);
            m_dpumpLimit = Math.Round(m_dpumpLimit, 2);

            if (parsed)
            {
                PumpThePresure(slider.Value, m_dpumpLimit);
            }
        }

        private void PumpThePresure(double curValue, double toValue)
        {
            if (m_dpumpLimit > PresureGauge.PrimaryScale.Value)
            {
                if (PresureGauge.PrimaryScale.Value <= PresureGauge.PrimaryScale.RangeEnd && m_dpumpLimit <= PresureGauge.PrimaryScale.RangeEnd)
                {
                    pumpAnim = PumpAnim;
                }
            }
            else if (m_dpumpLimit < PresureGauge.PrimaryScale.Value)
            {
                if (PresureGauge.PrimaryScale.Value >= PresureGauge.PrimaryScale.RangeBegin && m_dpumpLimit >= PresureGauge.PrimaryScale.RangeBegin)
                {
                    pumpAnim = PumpAnim;
                }
            }
            else
            {
                //pumpLimit.Clear();
            }
            pumpAnim.From = curValue;
            pumpAnim.To = toValue;
            pumpAnim.Duration = TimeSpan.FromSeconds(5);
            myStoryboard2.Begin();
        }
    }
}
