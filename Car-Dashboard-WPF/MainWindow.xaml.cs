/*
 * Car Dashboard Simulator in WPF
 * made (mostly) by Krzysztof Kazimierczak
 * and maybe by Lukasz Janasz
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

namespace IiMwT_Projekt_CS_WPF_2017
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool increase = true;
        private bool stop = false;

        private double batCurrentValue = 94;
        private double oilCurrentValue = 56;
        private double speedCurrentValue = 125;

        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            InitCarDashboard();
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
                    if (SpeedGauge.PrimaryScale.Value != SpeedGauge.PrimaryScale.RangeEnd)
                        SpeedGauge.PrimaryScale.Value += 2;
                    else
                        increase = false;
                }
                else
                {
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
