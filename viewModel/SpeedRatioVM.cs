using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using FlightSimulator2.model;

namespace FlightSimulator2.viewModel
{
    class SpeedRatioVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Client client;

        public SpeedRatioVM()
        {
            // changed because client is singelton.
            this.client = Client.client_instance;
            client.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public double VM_SpeedRatio
        {
            get
            {
                return client.SpeedRatio;
            }
            set
            {
                client.SpeedRatio = value;
            }

        }


        public void NotifyPropertyChanged(String propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }

   


}
