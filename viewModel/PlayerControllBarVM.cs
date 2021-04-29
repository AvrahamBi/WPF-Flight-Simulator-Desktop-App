using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator2.model;
using System.ComponentModel;

namespace FlightSimulator2.viewModel
{
    class PlayerControllBarVM : INotifyPropertyChanged
    {
        public PlayerControlBarM controlBar;
        

        public PlayerControllBarVM(PlayerControlBarM controlbar)
        {
            this.controlBar = controlbar;
            controlBar.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
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
        public long VM_Current_line
        {
            get
            {
                return controlBar.Current_line;
            }
            set
            {
                controlBar.Current_line = value;
            }
        }
        public long VM_Number_of_rows
        {
            get
            {
                return controlBar.Number_of_rows;
            }
        
        }
        public bool VM_play_or_pause
        {
            get
            {
                return controlBar.Play_or_Pause;
            }
            set
            {
                controlBar.Play_or_Pause = value;
            }
        }
        public string VM_Max_Time
        {
            get
            {
                return this.controlBar.Max_Time;
            }
            set
            {
                controlBar.Max_Time = value;
            }
        }
        public string VM_Current_time_string
        {
            get
            {
                return this.controlBar.Current_time_string;
            }
            set
            {
                controlBar.Current_time_string = value;
            }
        }

    }
}
