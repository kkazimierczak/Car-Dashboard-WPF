/*
 * Car Dashboard Simulator in WPF
 * made (mostly) by Krzysztof Kazimierczak
 * and maybe by Lukasz Janasz
 */
using System;
using System.Threading;
using System.Windows;
using System.Collections.ObjectModel;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace Car_Dashboard_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static EngineDataContainer engineData = new EngineDataContainer();
        Thread observer;

        EngineModel engine;
        delegate void UpdaterDelegate();
        public ObservableCollection<Point> Data { get; set; }

        public static double currentspeed = 0;

        private void SpeechRecognitionButton_Click(object sender, RoutedEventArgs e)
        {
            RecognizeSpeech();
        }            
        static SpeechRecognitionEngine _recognizer = null;
        SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
        void RecognizeSpeech()
        {
            _recognizer = new SpeechRecognitionEngine();
            _recognizer.LoadGrammar(new DictationGrammar());
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("activate")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("deactivate")));
            _recognizer.SpeechRecognized += _recognizeSpeech_SpeechRecognized;
            _recognizer.SpeechRecognitionRejected += _recognizeSpeech_SpeechRecognitionRejected;
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }
        void _recognizeSpeech_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "activate")
            {
                this.listBox1.Items.Add("AUTOPILOT START!");
                speechSynthesizer.Speak("Initiating automatic driving mode");
                SpeedSlider.Value = 50;

            }
            else if (e.Result.Text == "deactivate")
            {
                this.listBox1.Items.Add("AUTOPILOT STOP!");
                speechSynthesizer.Speak("Deactivating automatic driving mode");
                SpeedSlider.Value = 0;
            }
            else
            {
                this.listBox1.Items.Add(e.Result.Text);
            }
        }
        void _recognizeSpeech_SpeechRecognitionRejected(object sender,
        SpeechRecognitionRejectedEventArgs e)
        {
            this.listBox1.Items.Add("Unrecognized command...");
        }

        public MainWindow()
        {
            InitializeComponent();
            Top = 0;
            Left = -5;
            
            engine = new EngineModel();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            observer = new Thread(Observe);
            observer.Start();
        }
        private void Observe()
        {
            while(true)
            {
                Thread.Sleep(5);

                engineData.UpdateData(engine);

                RPMGauge.Dispatcher.Invoke(new UpdaterDelegate(() => {
                    currentspeed = RPMGauge.PrimaryScale.Value;
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
                TempGauge.Dispatcher.Invoke(new UpdaterDelegate(() =>
                {
                    TempGauge.PrimaryScale.Value = engine.currentTemperature;
                }));
            }
        }

        private void ChartsButton_Click(object sender, RoutedEventArgs e)
        {
            new ChartWindow().Show();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
            observer.Abort();
        }
    }
}
