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

using Arction.Gauges.Dials;
using Arction.Gauges.ValueMappers;
using System.Windows.Threading;
using Arction.Gauges.Common.Accessory;

namespace GaugesWpfDemoApp
{
    /// <summary>
    /// Interaction logic for ExampleSpeedometer.xaml
    /// </summary>
    public partial class ExampleSpeedometer : UserControl
    {
        private bool increase = true;
        
        public ExampleSpeedometer()
        {
            InitializeComponent();
            //StartTickAnimation();
        }

        private void StartTickAnimation()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(200000);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (increase)
            {
                if (SpeedGauge.PrimaryScale.Value != SpeedGauge.PrimaryScale.RangeEnd)
                    SpeedGauge.PrimaryScale.Value++;
                else
                    increase = false;
            }
            else
            {
                if (SpeedGauge.PrimaryScale.Value != SpeedGauge.PrimaryScale.RangeBegin)
                    SpeedGauge.PrimaryScale.Value--;
                else
                    increase = true;
            }
        }
    }
}
