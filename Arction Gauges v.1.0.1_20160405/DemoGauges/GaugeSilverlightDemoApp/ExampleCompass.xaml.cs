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


using Arction.Gauges.Extensions;
using Arction.Gauges;
using Arction.DProp;
using System.IO;
using System.Windows.Resources;
using Arction.Gauges.Ticks;
using System.Reflection;
using sio = System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Media.Effects;
using Arction.Gauges.Geo;

namespace GaugesWpfDemoApp
{
    /// <summary>
    /// Interaction logic for ExampleCompass.xaml
    /// </summary>
    public partial class ExampleCompass : UserControl
    {
        public ExampleCompass()
        {
            InitializeComponent();
            Compass.MouseEnter += Compass_MouseEnter;
        }

        void Compass_MouseEnter(object sender, MouseEventArgs e)
        {
            Compass.MouseMove += Compass_MouseMove;
        }

        private void Compass_MouseMove(object sender, MouseEventArgs e)
        {
            Point MouseCompassPosition = e.GetPosition(Compass);
            VecS CC = new VecS(Compass.ContentCenter);
            Angle angle = Angle.FromPoints(CC, MouseCompassPosition);
            Compass.PrimaryScale.Value = -angle.Deg+90;
        }
    }
}
