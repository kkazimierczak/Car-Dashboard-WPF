using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace GaugeSilverlightDemoApp
{
    public partial class ExamplePressure : UserControl
    {
        private Color c1 = Color.FromArgb(255, 251, 10, 10);
        private Color c2 = Color.FromArgb(255, 255, 255, 0);
        private Color c3 = Color.FromArgb(255, 116, 255, 0);
        private Color c;
        private double m_dpumpLimit;
        //DispatcherTimer timer;
        private Storyboard myStoryboard1;
        private DoubleAnimation pumpAnim;
        public ExamplePressure()
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
            myStoryboard2.Begin();
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
            myStoryboard1 = new Storyboard();

            if (m_dpumpLimit > PresureGauge.PrimaryScale.Value)
            {
                if (PresureGauge.PrimaryScale.Value <= PresureGauge.PrimaryScale.RangeEnd && m_dpumpLimit <= PresureGauge.PrimaryScale.RangeEnd)
                {
                    //slider.Value += 0.01;
                    pumpAnim = new DoubleAnimation();
                }
            }
            else if (m_dpumpLimit < PresureGauge.PrimaryScale.Value)
            {
                if (PresureGauge.PrimaryScale.Value >= PresureGauge.PrimaryScale.RangeBegin && m_dpumpLimit >= PresureGauge.PrimaryScale.RangeBegin)
                {
                    //slider.Value -= 0.01;
                    pumpAnim = new DoubleAnimation();
                }
            }
            else
            {
                //pumpLimit.Clear();
            }
            pumpAnim.From = curValue;
            pumpAnim.To = toValue;
            pumpAnim.Duration = TimeSpan.FromSeconds(5);
            myStoryboard1.Children.Add(pumpAnim);
            Storyboard.SetTargetName(pumpAnim, "slider");
            Storyboard.SetTargetProperty(pumpAnim, new PropertyPath("Value"));
            myStoryboard1.Begin();
        }

        private void Animation(object sender, RoutedEventArgs e)
        {
            myStoryboard2.Begin();
        }
    }
}