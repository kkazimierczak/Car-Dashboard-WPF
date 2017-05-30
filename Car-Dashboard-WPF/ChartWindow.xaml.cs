using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Timers;
using System.Windows;

namespace Car_Dashboard_WPF
{
    /// <summary>
    /// Interaction logic for ChartWindow.xaml
    /// </summary>
    public partial class ChartWindow : Window
    {
        static bool isVisible = false;
        const int SAMPLING_TIME = 400;
        Timer timer;
        long cycles = 0;

        ObservableDataSource<Point> speedData;
        LineGraph speedLinegraph;
        ObservableDataSource<Point> RPMData;
        LineGraph RPMLinegraph;
        ObservableDataSource<Point> fuelUsageData;
        LineGraph fuelUsageLinegraph;

        public ChartWindow()
        {
            InitializeComponent();
            InitializeTimer();
            Top = 0;
            Left = 970;

            speedData = new ObservableDataSource<Point>();
            speedLinegraph = new LineGraph(speedData);
            SpeedPlot.Children.Add(speedLinegraph);
            SpeedPlot.LegendVisible = false;

            RPMData = new ObservableDataSource<Point>();
            RPMLinegraph = new LineGraph(RPMData);
            RPMPlot.Children.Add(RPMLinegraph);
            RPMPlot.LegendVisible = false;

            fuelUsageData = new ObservableDataSource<Point>();
            fuelUsageLinegraph = new LineGraph(fuelUsageData);
            FuelUsagePlot.Children.Add(fuelUsageLinegraph);
            FuelUsagePlot.LegendVisible = false;
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = SAMPLING_TIME;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var currentSpeed = new Point(cycles * timer.Interval / 1000, MainWindow.engineData.speed);
            speedData.AppendAsync(Dispatcher, currentSpeed);
            var currentRPM = new Point(cycles * timer.Interval / 1000, MainWindow.engineData.rpm);
            RPMData.AppendAsync(Dispatcher, currentRPM);
            var currentFuelUsage = new Point(cycles * timer.Interval / 1000, MainWindow.engineData.fuelUsage * 3600);
            fuelUsageData.AppendAsync(Dispatcher, currentFuelUsage);
            cycles++;
        }

        new public void Show()
        {
            if (!isVisible)
            {
                base.Show();
                isVisible = true;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            isVisible = false;
        }
    }
}
