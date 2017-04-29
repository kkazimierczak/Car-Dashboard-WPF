/*
 * Car Dashboard Simulator in WPF
 * made (mostly) by Krzysztof Kazimierczak
 * and maybe by Lukasz Janasz
 */
using System;
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
        ModelInterfaceCommunication stateObserver;

        public delegate void UpdaterDelegate();

        public MainWindow()
        {
            InitializeComponent();
            Top = 0;
            Left = 50;

            engine = new EngineModel();
            stateObserver = new ModelInterfaceCommunication();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Thread dupa = new Thread(Observe);
            dupa.Start();
        }

        private void Observe()
        {
            while(true)
            {
                Thread.Sleep(10);
                stateObserver.UpdateCurrentValues(engine, SpeedSlider);
                SpeedGauge.Dispatcher.Invoke(new UpdaterDelegate(() => {
                    SpeedGauge.PrimaryScale.Value = engine.currentSpeed;
                }));
                SpeedSlider.Dispatcher.Invoke(new UpdaterDelegate(() =>
                {
                    engine.wantedSpeed = (int)SpeedSlider.Value;
                }));
            }
        }

        //private void UpdateGauge()
        //{
        //    SpeedGauge.PrimaryScale.Value = stateObserver.currentSpeed;
        //}
    }
}
