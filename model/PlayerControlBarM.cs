using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;


namespace FlightSimulator2.model
{
    class PlayerControlBarM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //fields

        private long scroll_place; // our scroll place
        private double suspend_time; // the time we wait before send the next stat of the flight.
        private long max_scroll; // should show the time in string.
        private StreamReader flight_data; // the csv file
        Dictionary<long, string> flight_state; // all the lines of the flight wil be bind to specifiec time.
        private string current_time_string; // the current time in string

        private string max_Time;
        public string Max_Time
        {
            get
            {
                return max_Time;
            }
            set
            {
                this.max_Time = value;
                NotifyPropertyChanged("Max_Time");
            }
        }
        public string Current_time_string
         {
            get
            {
                return current_time_string;
            }
            set
            {
                this.current_time_string = value;
                NotifyPropertyChanged("Current_time_string");
    }
}
        long current_line; // the current line we send to flight gear.
        public long Current_line // current line property
        {
            get
            {
                return this.current_line;
            }
            set
            {
                current_line = value;
                NotifyPropertyChanged("Current_Line");
            }
        }
        private long number_of_rows; // the number of lines totaly in csv file
        public long Number_of_rows
        {
            get
            {
                return this.number_of_rows;
            }
            set
            {
                this.number_of_rows = value;
                NotifyPropertyChanged("Number_of_rows");
            }
        }

        // to know if the 
        private bool play_or_pause;
        public bool Play_or_Pause
        {
            get
            {
                return this.play_or_pause;
            }
            set
            {
                this.play_or_pause = value;
                NotifyPropertyChanged("Play_or_Pause");
            }
        }

        // constructor
        public PlayerControlBarM( double suspend)
        {
            scroll_place = 0;
            Number_of_rows = 0;
            suspend_time = suspend;
            max_scroll = 0;
            Current_line = 0;
            flight_state = new Dictionary<long, string>();
            this.play_or_pause = true;
        }
        /*
         * flightAnalysis = calculate the number of lines and bind every line to specefiec time, also calculate the max scroll.
         */
        public void flightAnalysis()
        {
            int i = 0;
            string line_string;
            while ((line_string = flight_data.ReadLine()) != null)
            {
                flight_state.Add(i, line_string);
                i++;
            }
            Number_of_rows = i;

            // for adding max time (will updater later)
            max_scroll = (long)(number_of_rows / (suspend_time * 0.001));
            TimeSpan time = TimeSpan.FromSeconds(max_scroll);
            string displayTime = time.ToString(@"hh\:mm\:ss");
            Max_Time = displayTime;
        }

        /*
         * getflightState = return the string that wil sent to the flight gear
         */
        public string getFlightState()
        {
            if (flight_state.ContainsKey(current_line)) // check if the flight end if not we return the flight of the specifiec line
            {
                if (play_or_pause)
                {
                    ++Current_line;
                    long n = (long)(Current_line / (suspend_time * 0.001));
                    TimeSpan t = TimeSpan.FromSeconds(n);
                    this.Current_time_string = t.ToString(@"hh\:mm\:ss");
                    return flight_state[current_line - 1];
                }
                return flight_state[current_line]; // if the pause button pressed then we send the same line over and over.
            }
            return null;
        }

        public void NotifyPropertyChanged(string propName)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public void setFlight(StreamReader f)
        {
            this.flight_data = f;
        }
        public string currentState()
        {
            if (!this.flight_state.ContainsKey(current_line)) return null;
            return this.flight_state[current_line];
        }
        public long rowsNumber()
        {
            return this.Number_of_rows;
        }
        
        // INotifyPropertyChanged implementation.
        // public event PropertyChangedEventHandler Property;



         //INotifyPropertyChanged implementation.

    }
}
