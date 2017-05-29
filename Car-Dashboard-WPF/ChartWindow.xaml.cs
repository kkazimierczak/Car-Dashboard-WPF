using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private static bool isVisible = false;

        public ChartWindow()
        {
            InitializeComponent();
            Top = 0;
            Left = 1280;
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
