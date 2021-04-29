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
using FlightSimulator2.viewModel;

namespace FlightSimulator2.view
{
    /// <summary>
    /// Interaction logic for SpeedRatio.xaml
    /// </summary>
    public partial class SpeedRatio : UserControl
    {

        SpeedRatioVM SpeedRatioVM;



        public SpeedRatio()
        {
            InitializeComponent();
            SpeedRatioVM = new SpeedRatioVM();
            DataContext = SpeedRatioVM;
        }
    }
}
