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
        private Color c1 = Color.FromRgb(251, 10, 10);
        private Color c2 = Color.FromRgb(255, 255, 0);
        private Color c3 = Color.FromRgb(116, 255, 0);
        private Color c;
        private double m_dpumpLimit;
        //DispatcherTimer timer;
        private Storyboard myStoryboard;
        private DoubleAnimation pumpAnim;
        public ExamplePressureGauge()
        {
            InitializeComponent();

            IndicatorCheck(slider.Value);

            pumpLimit.Text = "3,5";

            //timer = new DispatcherTimer();
            //timer.Interval = new TimeSpan(200000);
        }

        private void IndicatorCheck(double value)
        {
            if (value >= PresureGauge.SecondaryScales[2].RangeBegin && value <= PresureGauge.SecondaryScales[2].RangeEnd)
                c = c1;
            else if (value >= PresureGauge.SecondaryScales[1].RangeBegin && value <= PresureGauge.SecondaryScales[1].RangeEnd)
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
                //timer.Tick += timer_Tick;
                //timer.Start();
                PumpThePresure(slider.Value, m_dpumpLimit);
            }
        }

        private void PumpThePresure(double curValue, double toValue)
        {
            myStoryboard = new Storyboard();

            if (toValue > PresureGauge.PrimaryScale.Value)
            {
                if (PresureGauge.PrimaryScale.Value <= PresureGauge.PrimaryScale.RangeEnd && toValue <= PresureGauge.PrimaryScale.RangeEnd)
                {
                    //slider.Value += 0.01;
                    pumpAnim = new DoubleAnimation(curValue, toValue, TimeSpan.FromSeconds(5));
                }
            }
            else if (toValue < PresureGauge.PrimaryScale.Value)
            {
                if (PresureGauge.PrimaryScale.Value >= PresureGauge.PrimaryScale.RangeBegin && toValue >= PresureGauge.PrimaryScale.RangeBegin)
                {
                    //slider.Value -= 0.01;
                    pumpAnim = new DoubleAnimation(curValue, toValue, TimeSpan.FromSeconds(5));
                }
            }
            else
            {
                pumpLimit.Clear();
            }
            myStoryboard.Children.Add(pumpAnim);
            Storyboard.SetTargetName(pumpAnim, slider.Name);
            Storyboard.SetTargetProperty(pumpAnim, new PropertyPath("Value"));
            myStoryboard.Begin(slider);
        }
    }
}
