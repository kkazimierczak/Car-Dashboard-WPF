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

namespace Car_Dashboard_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EngineModel engine;
        delegate void UpdaterDelegate();

        public MainWindow()
        {
            InitializeComponent();
            Top = 0;
            Left = 50;

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
                    FuelUsageTextBox.Text = Math.Round(engine.fuelUsage * 3600, 1).ToString();
                }));
                FuelGauge.Dispatcher.Invoke(new UpdaterDelegate(() =>
                {
                    FuelGauge.PrimaryScale.Value = engine.fuelLeft;
                }));
            }
        }
    }
}
