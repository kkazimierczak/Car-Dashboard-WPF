using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ExampleSpeedGaugeDashboard.xaml
    /// </summary>
    public partial class ExampleSpeedGaugeDashboard : UserControl
    {
        private bool increase = true;
        private bool stop = false;

        private double batCurrentValue = 94;
        private double oilCurrentValue = 56;
        private double speedCurrentValue = 125;

        DispatcherTimer timer;
        public ExampleSpeedGaugeDashboard()
        {
            InitializeComponent();
            //InitCarDashboard();
            BatteryGauge.PrimaryScale.AngleBegin = 270;
            SpeedGauge_Copy.PrimaryScale.MajorTickCount = 6;
            myStoryboard.Begin();
        }

        private void InitCarDashboard()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(100000);
            timer.Tick += timer_TickInit;
            timer.Start();
        }

        private void timer_TickInit(object sender, EventArgs e)
        {
            if (!stop)
            {
                if (increase)
                {
                    if (BatteryGauge.PrimaryScale.Value != BatteryGauge.PrimaryScale.RangeEnd)
                        batLevel.Value++;

                    if (OilGauge.PrimaryScale.Value != OilGauge.PrimaryScale.RangeEnd)
                        oilLevel.Value++;

                    if (SpeedGauge.PrimaryScale.Value != SpeedGauge.PrimaryScale.RangeEnd)
                        SpeedGauge.PrimaryScale.Value += 2;
                    else
                        increase = false;
                }
                else
                {
                    if (BatteryGauge.PrimaryScale.Value != BatteryGauge.PrimaryScale.RangeBegin)
                        batLevel.Value--;

                    if (OilGauge.PrimaryScale.Value != OilGauge.PrimaryScale.RangeBegin)
                        oilLevel.Value--;

                    if (SpeedGauge.PrimaryScale.Value != SpeedGauge.PrimaryScale.RangeBegin)
                        SpeedGauge.PrimaryScale.Value -= 2;
                    else
                    {
                        stop = true;
                        timer.Stop();
                        Thread.Sleep(500);
                        SetValuesToGauges();
                    }
                }
            }

        }

        private void SetValuesToGauges()
        {
            stop = false;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(100000);
            timer.Tick += timer_TickSetValue;
            timer.Start();
        }

        private void timer_TickSetValue(object sender, EventArgs e)
        {
            if (!stop)
            {
                if (BatteryGauge.PrimaryScale.Value != batCurrentValue)
                    batLevel.Value++;

                if (OilGauge.PrimaryScale.Value != oilCurrentValue)
                    oilLevel.Value++;

                if (SpeedGauge.PrimaryScale.Value != speedCurrentValue)
                    SpeedGauge.PrimaryScale.Value += 1;
                else
                {
                    stop = true;
                    timer.Stop();
                }
            }
        }
    }
}
