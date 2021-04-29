using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator2.model;
using System.ComponentModel;
namespace FlightSimulator2.viewModel
{
    class JoystickVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private JoystickM joyM;

        public JoystickVM(JoystickM newJoyM)
        {
            this.joyM = newJoyM;
            joyM.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public double VM_Elevator
        {
            get
            {
                return this.joyM.Elevator;
            }
            set
            {
                this.joyM.Elevator = value;
            }
        }

        public double VM_Aileron
        {
            get
            {
                return this.joyM.Aileron;
            }
            set
            {
                this.joyM.Aileron = value;
            }
        }

        public double VM_throttle
        {
            get
            {
                return this.joyM.Throttle;
            }
            set
            {
                this.joyM.Throttle = value;
            }
        }

        public double VM_rudder
        {
            get
            {
                return this.joyM.Rudder;
            }
            set
            {
                this.joyM.Rudder = value;
            }
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void startflightSpecifiecJoystickMode()
        {
            this.joyM.flightSpecifiecJoystickMode();
        }
    }
}
