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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using OxyPlot;
using FlightSimulator2.viewModel;
using FlightSimulator2.model;

namespace FlightSimulator2.view
{
    /// <summary>
    /// Interaction logic for RegresionLine.xaml
    /// </summary>
    public partial class RegresionLine : UserControl
    {
        private RegresionLineVM regLineVM;
 

  /*      public RegresionLine RegLineVM
        {
            get
            {
                return this.regLineVM; 
            }

            set
            {
                this.RegLineVM = value;
            }
        }
*/
        public RegresionLine()
        {
            InitializeComponent();
            this.regLineVM = new RegresionLineVM(new RegresionLineM());
            DataContext = this.regLineVM;
            // thread that make the plots updated using thread
            new Thread(delegate ()
            {
                while (true)
                {
                    Thread.Sleep(100);
                    this.Dispatcher.Invoke(() =>
                    {
                        RegresionLineGraph.InvalidatePlot(true);
                        /*CorrelationGraph.InvalidatePlot(true);
                        FeaturesGraph.InvalidatePlot(true);*/

                    });
                }
            }).Start();

        }
    }
    }
