using System;
using FlightSimulator2.viewModel;
using FlightSimulator2.model;
using FlightSimulator2;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxyPlot;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace FlightSimulator2.view
{
    /// <summary>
    /// Interaction logic for FeaturesGraphV.xaml
    /// </summary>
    public partial class FeaturesGraphV : UserControl
    {


        private FeaturesGraphVM _viewModel;
        public FeaturesGraphVM _ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }


        public FeaturesGraphV()
        {

            InitializeComponent();
            _viewModel = new FeaturesGraphVM(new FeaturesGraphM());
            DataContext = _viewModel;

            // thread that make the plots updated using thread
            new Thread(delegate ()
            {
                while (true)
                {
                    Thread.Sleep(100);
                    this.Dispatcher.Invoke(() =>
                    {
                        CorrelationGraph.InvalidatePlot(true);
                        FeaturesGraph.InvalidatePlot(true);
                        RegresionGraph.InvalidatePlot(true);

                    });
                }
            }).Start();

        }

        // function invoked when item selected in the list
        private void featuresListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            _viewModel.featureSelected(featuresListBox.SelectedIndex);
            _viewModel.NameOfFeatureSelected = featuresListBox.SelectedItem.ToString();

        }
    }
}