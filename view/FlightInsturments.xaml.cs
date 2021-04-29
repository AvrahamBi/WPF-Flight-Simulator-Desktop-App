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
using FlightSimulator2.model;

namespace FlightSimulator2.view
{
    /// <summary>
    /// Interaction logic for FlightInsturments.xaml
    /// </summary>
    public partial class FlightInsturments : UserControl
    {
        FlightInsturmentsVM fl;
        public FlightInsturments()
        {
            InitializeComponent();
            this.fl = new FlightInsturmentsVM(new FlightInsturmentsM());
            DataContext = fl;
            fl.startFlightAnalysisInstruments();



        }
    }
}
