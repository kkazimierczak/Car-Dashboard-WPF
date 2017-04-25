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

        public MainWindow()
        {
            InitializeComponent();

            engine = new EngineModel();
            stateObserver = new ModelInterfaceCommunication();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Thread dupa = new Thread(Observe);
            dupa.Start();
        }

        public delegate void UpdaterDelegate();

        private void Observe()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(100);
                stateObserver.UpdateCurrentValues(engine);
                SpeedGauge.Dispatcher.Invoke(new UpdaterDelegate(UpdateGauge));
            }
        }

        private void UpdateGauge()
        {
            SpeedGauge.PrimaryScale.Value = stateObserver.currentSpeed;
        }
    }
}
