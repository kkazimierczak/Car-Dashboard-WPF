using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Car_Dashboard_WPF
{
    /// <summary>
    /// Interaction logic for ChartWindow.xaml
    /// </summary>
    public partial class ChartWindow : Window
    {
        static bool isVisible = false;
        const int SAMPLING_TIME = 200;
        Timer timer;
        ObservableDataSource<Point> data;
        LineGraph linegraph;
        long cycles = 0;

        public ChartWindow()
        {
            InitializeComponent();
            InitializeTimer();
            Top = 0;
            Left = 970;

            data = new ObservableDataSource<Point>();
            linegraph = new LineGraph(data);
            Plot.Children.Add(linegraph);
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
            var point = new Point(cycles * timer.Interval / 1000, MainWindow.engineData.speed);
            data.AppendAsync(Dispatcher, point);
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
