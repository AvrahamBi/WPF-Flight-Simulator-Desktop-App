using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Threading;

namespace FlightSimulator2.model
{
    class FlightInsturmentsM : INotifyPropertyChanged
    {
        // fields 
        bool diconnect; // to know if we diconneced from the client
        public event PropertyChangedEventHandler PropertyChanged;
        // the index of the data we need from the string. 
        int speed_index; 
        int altimeter_index;
        int headdeg_index;
        int pitch_index;
        int yaw_index;
        int roll_index;
        // data
        string pitch;
        string roll;
        string yaw;
        double head_deg;
        double altimeter;


        // properites
        public string Pitch
        {
            get
            {
                return pitch;
            }
            set
            {
                pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }
        public string Roll
        {
            get
            {
                return roll;
            }
            set
            {
                roll = value;
                NotifyPropertyChanged("Roll");
            }
        }
        public string Yaw
        {
            get
            {
                return yaw;
            }
            set
            {
                yaw = value;
                NotifyPropertyChanged("Yaw");
            }
        }
        public double Head_deg
        {
            get
            {
                return head_deg;
            }
            set
            {
                head_deg = value;
                NotifyPropertyChanged("Head_deg");
            }
        }
        public double Altimeter
        {
            get
            {
               return altimeter;
            }
            set
            {
                altimeter = value;
                NotifyPropertyChanged("Altimeter");

            }
        }
        double flight_speed;
        public double Flight_speed
        {
            get
            {
                return flight_speed;
            }
            set
            {
                flight_speed = value;
                NotifyPropertyChanged("Flight_speed");
            }
        }
        public FlightInsturmentsM()
        {
            speed_index = 21;
            altimeter_index = 16;
            headdeg_index = 36;
            diconnect = false;
            roll_index = 17;
            pitch_index = 18;
            yaw_index = 20;
            Pitch = "0";
            Roll = "0";
            Yaw = "0";
        }
        
        // create a thread that wil split the string and then take the information we need
        public void flightSpecifiecInsturment()
        {
            new Thread(delegate ()
            {
                while (diconnect == false)
                {
                    if (Client.client_instance.currentFlightState() == null) continue;
                    string[] flightsInsturment;
                    flightsInsturment = Client.client_instance.currentFlightState().Split(',');
                    this.Flight_speed = Convert.ToDouble(flightsInsturment[speed_index]);
                    this.Altimeter = Convert.ToDouble(flightsInsturment[altimeter_index]);
                    this.Head_deg = Convert.ToDouble(flightsInsturment[headdeg_index]);
                    this.Roll = flightsInsturment[roll_index];
                    this.Yaw = flightsInsturment[yaw_index];


                    this.Pitch = flightsInsturment[pitch_index];
                }
            }).Start();
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
