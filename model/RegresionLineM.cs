using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxyPlot;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Shapes;

namespace FlightSimulator2.model
{
    class RegresionLineM : INotifyPropertyChanged
    {
       
        public event PropertyChangedEventHandler PropertyChanged;
        FeaturesGraphM graphM;
        private string x_feature, y_feature;
        private List<DataPoint> x_feature_list, y_feature_list;
        private Line reg_line;
        private List<DataPoint> points;
       // private DataPoint lineP1, lineP2;
        private List<DataPoint> line;

        /*public RegresionLineM(FeaturesGraphM graphM)
        {

        }*/
        public RegresionLineM()
        {
            graphM = new FeaturesGraphM();
/*            while (graphM.M_Points.Count() == 0)
            {
                continue;
            }*/
            
           /* x_feature_list = graphM.M_Points;
            y_feature_list = graphM.M_CorrelatedPoints;*/
            FindRegLine();
            findLineValue();

        }

        /*public DataPoint LineP1
        {
            get
            {
                return this.lineP1;
            }

            set
            {
                this.lineP1 = value;
                NotifyPropertyChanged("LineP1");
            }
        }

        public DataPoint LineP2
        {
            get
            {
                return this.lineP2;
            }

            set
            {
                this.lineP2 = value;
                NotifyPropertyChanged("LineP2");
            }
        }*/

        public List<DataPoint> LineByPoints
        {
            get
            {
                return this.line;
            }

            set
            {
                this.line = value;
                NotifyPropertyChanged("lineByPoint");
            }
        }
        public void findLineValue()
        {
            new Thread(delegate()
            {
            while(graphM.M_Points.Count() != 0) {
                    Thread.Sleep(100);
                    this.line.Add(new DataPoint(graphM.M_Points.First().Y, this.reg_line.f(graphM.M_Points.First().Y)));
                    this.line.Add(new DataPoint(graphM.M_CorrelatedPoints.Last().Y, this.reg_line.f(graphM.M_CorrelatedPoints.Last().Y)));
                    Console.WriteLine("P:");
                    Console.WriteLine(graphM.M_CorrelatedPoints.First().X);
                    Console.WriteLine(this.reg_line.f(graphM.M_Points.First().Y));
                    Console.WriteLine(graphM.M_CorrelatedPoints.Last().X);
                    Console.WriteLine(this.reg_line.f(graphM.M_Points.Last().Y));

                }
            }).Start();
        }

        public Line Reg_line
        {
            get
            {
                return this.reg_line;
            }

            set
            {
                this.reg_line = value;
                NotifyPropertyChanged("Reg_line");
            }
        }

        public List<DataPoint> Points
        {
            get
            {
                return this.points;
            }

            set
            {
                this.points = value;
                NotifyPropertyChanged("Points");
            }
        }

        public void FindRegLine()
        {
            new Thread(delegate ()
            {
                while (graphM.M_Points.Count() != 0)
                {
                    this.reg_line = new Line();
                    this.reg_line.linear_reg(graphM.M_Points, graphM.M_CorrelatedPoints, graphM.M_Points.Count());
                }
            }).Start();
        }
        public List<DataPoint> X_feature_list
        {
            get
            {
                return this.x_feature_list;
            }

            set
            {
                this.x_feature_list = value;
                NotifyPropertyChanged("X_feature_list");
            }
        }

        public List<DataPoint> Y_feature_list
        {
            get
            {
                return this.y_feature_list;
            }

            set
            {
                this.y_feature_list = value;
                NotifyPropertyChanged("Y_feature_list");
            }
        }
        public string X_feature
        {
            get
            {
                return this.x_feature;
            }

            set
            {
                this.x_feature = value;
                NotifyPropertyChanged("X_feature");
            }
        }

        public string Y_feature
        {
            get
            {
                return this.y_feature;
            }

            set
            {
                this.y_feature = value;
                NotifyPropertyChanged("Y_feature");
            }
        }

        public void GetListOfData()
        {
            
        }
 
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
