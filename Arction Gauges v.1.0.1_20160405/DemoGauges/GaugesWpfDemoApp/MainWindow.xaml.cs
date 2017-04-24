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

namespace GaugesWpfDemoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserControl m_selectedExample;
        TreeViewItem m_prevItem;

        public MainWindow()
        {
            InitializeComponent();

            treeView1.ItemsSource = ExampleNodes;
        }

        private static readonly Node[] ExampleNodes =
            new Node[] 
			{
				new Node("Basic gauges", null, 
                    new Node[] 
                    {
                        new Node("Clock", typeof(ExampleClock), null),                         
                        new Node("Speedometer", typeof(ExampleSpeedometer), null),
                        new Node("Pressure gauge", typeof(ExamplePressureGauge), null),
                        new Node("Thermometer", typeof(ExampleThermometerWithMenu), null),
                    }
                ), 

                new Node("Advanced gauges", null, 
                    new Node[] 
                    {
                        new Node("Compass", typeof(ExampleCompass), null),
                        new Node("Car Dashboard", typeof(ExampleSpeedGaugeDashboard), null),
                        new Node("Multimeter", typeof(ExampleMultimeter), null),
                    }
                ), 
            };


        private void treeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            m_selectedExample = null;
            gridExampleContainer.Children.Clear(); 

            
            
            if (treeView1.SelectedItem != null)
            {
                TreeViewItem tvi = treeView1.SelectedItem as TreeViewItem;

                tvi.Foreground = Brushes.Yellow;

                if (m_prevItem != null)
                {
                    m_prevItem.Foreground = Brushes.White;
                }

                m_prevItem = tvi;

                if (tvi.Tag != null)
                {
                    m_selectedExample = (UserControl)Activator.CreateInstance(tvi.Tag as Type);
                    gridExampleContainer.Children.Add(m_selectedExample);
                }
            }
            
        }
    }
}
