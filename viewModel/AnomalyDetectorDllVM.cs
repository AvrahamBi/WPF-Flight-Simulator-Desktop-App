using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator2.model;

namespace FlightSimulator2.viewModel
{
    class AnomalyDetectorDllVM : INotifyPropertyChanged
    {
        //field
        AnomalyDetectorDllM detector;
        public AnomalyDetectorDllVM(AnomalyDetectorDllM d)
        {
            this.detector = d;
            detector.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        // properties
        public string VM_Learning_flight_path
        {
            get
            {
                return this.detector.Learning_flight_path;
            }
            set
            {
                this.detector.Learning_flight_path = value;
            }
        }
        public string VM_Dll_path
        {
            get
            {
                return this.detector.Dll_path;
            }
            set
            {
                this.detector.Dll_path = value;
            }
        }
        public void detect()
        {
            this.detector.Anomalychoosen();
        }
        public int reportSize()
        {
            return this.detector.reportSize();
        }
        public List<anomalyReport> reports()
        {
            return this.detector.Reports();
        }
    }
}
