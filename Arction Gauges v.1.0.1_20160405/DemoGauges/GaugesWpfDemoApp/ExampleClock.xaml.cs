using Arction.Gauges;
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
using System.Windows.Threading;

namespace GaugesWpfDemoApp
{
    /// <summary>
    /// Interaction logic for ExampleWatch.xaml
    /// </summary>
    public partial class ExampleClock : UserControl
    {
        public ExampleClock()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += TimeChanged;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void TimeChanged(object sender, EventArgs e)
        {
            if (ClockGauge == null) { return; }

            if (ClockGauge.SecondaryScales.Count >= 2)
            {
                DateTime time = DateTime.Now;
                Scale Hours = ClockGauge.PrimaryScale;
                Scale Mins = ClockGauge.SecondaryScales[0];
                Scale Secs = ClockGauge.SecondaryScales[1];

                if (Hours != null)
                {
                    Hours.Value = time.Hour;
                }

                if (Mins != null)
                {
                    Mins.Value = time.Minute;
                }

                if (Secs != null)
                {
                    Secs.Value = time.Second;
                }

            }
        }
    }
}
