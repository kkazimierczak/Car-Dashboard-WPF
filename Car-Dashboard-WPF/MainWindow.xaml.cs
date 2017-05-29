/*
 * Car Dashboard Simulator in WPF
 * made (mostly) by Krzysztof Kazimierczak
 * and maybe by Lukasz Janasz
 */
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System.Collections.ObjectModel;

namespace Car_Dashboard_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EngineModel engine;
        delegate void UpdaterDelegate();
        public ObservableCollection<Point> Data { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Top = 0;
            Left = -5;

            ObservableDataSource<Point> data = new ObservableDataSource<Point>();
            LineGraph linegraph = new LineGraph(data);

            var p1 = new Point(0, 1);
            var p2 = new Point(4, 4);

            data.AppendAsync(Dispatcher, p1);
            data.AppendAsync(Dispatcher, p2);

            //Plot.Children.Add(linegraph);

            engine = new EngineModel();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Thread observer = new Thread(Observe);
            observer.Start();
        }

        private void Observe()
        {
            while(true)
            {
                Thread.Sleep(5);
                RPMGauge.Dispatcher.Invoke(new UpdaterDelegate(() => {
                    RPMGauge.PrimaryScale.Value = engine.currentRPM;
                }));
                SpeedGauge.Dispatcher.Invoke(new UpdaterDelegate(() => {
                    SpeedGauge.PrimaryScale.Value = engine.currentSpeed;
                }));
                SpeedSlider.Dispatcher.Invoke(new UpdaterDelegate(() =>
                {
                    engine.wantedSpeed = (int)SpeedSlider.Value;
                }));
                GearTextBox.Dispatcher.Invoke(new UpdaterDelegate(() =>
                {
                    GearTextBox.Text = engine.gear.ToString();
                }));
                WantedSpeedTextBox.Dispatcher.Invoke(new UpdaterDelegate(() =>
                {
                    int value = (int)SpeedSlider.Value;
                    WantedSpeedTextBox.Text = value.ToString();
                }));
                FuelUsageTextBox.Dispatcher.Invoke(new UpdaterDelegate(() =>
                {
                    FuelUsageTextBox.Text = Math.Round(engine.fuelUsage * 3600, 1).ToString() + " l/100 km";
                }));
                FuelGauge.Dispatcher.Invoke(new UpdaterDelegate(() =>
                {
                    FuelGauge.PrimaryScale.Value = engine.fuelLeft;
                }));
            }
        }
    }
}
