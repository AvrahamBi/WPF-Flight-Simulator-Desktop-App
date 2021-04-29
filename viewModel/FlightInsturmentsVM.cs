using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using FlightSimulator2.model;

namespace FlightSimulator2.viewModel
{
    class FlightInsturmentsVM : INotifyPropertyChanged
    {
        FlightInsturmentsM fl;
        public FlightInsturmentsVM(FlightInsturmentsM f)
        {
            this.fl = f;
            fl.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
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
        public double VM_Flight_speed
        {
            get
            {
                return this.fl.Flight_speed;
            }
            set
            {
                this.fl.Flight_speed = value;
            }
        }

        public double VM_Altimeter
        {
            get
            {
                return this.fl.Altimeter;
            }
            set
            {
                this.fl.Altimeter = value;
            }
        }
        public double VM_Head_deg
        {
            get
            {
                return this.fl.Head_deg;
            }
            set
            {
                this.fl.Head_deg = value;
            }
        }
        public string VM_Pitch
        {
            get
            {
                return this.fl.Pitch;
            }
            set
            {
                this.fl.Pitch = value;
            }
        }
        public string VM_Roll
        {
            get
            {
                return this.fl.Roll;
            }
            set
            {
                this.fl.Roll = value;
            }
        }
        public string VM_Yaw
        {
            get
            {
                return this.fl.Yaw;
            }
            set
            {
                this.fl.Yaw = value;
            }
        }
        //methods
        public void startFlightAnalysisInstruments()
        {
            fl.flightSpecifiecInsturment();
        }
    }
}
