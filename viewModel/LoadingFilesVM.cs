using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using FlightSimulator2.model;

namespace FlightSimulator2.viewModel
{
    class LoadingFilesVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public LoadingFilesM model;
        public Client client;


        public String VM_from_playback
        {
            get
            {
                return model.From_playback;
            }
            set
            {
                model.From_playback = value;
            }

        }

        public String VM_to_playback
        {
            get
            {
                return model.To_playback;
            }
            set
            {
                model.To_playback = value;
            }

        }

        public String VM_from_reg
        {
            get
            {
                return client.From_reg;
            }
            set
            {
                client.From_reg = value;
            }

        }
        public String VM_IP
        {
            get
            {
                return client.IP;
            }
            set
            {
                client.IP = value;
            }

        }
        public int VM_Port
        {
            get
            {
                return client.Port;
            }
            set
            {
                client.Port = value;
            }

        }

        public LoadingFilesVM()
        {
            this.model = new LoadingFilesM();
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
            // changed because client is singelton.
            this.client = Client.client_instance;
            client.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        
        public void NotifyPropertyChanged(String propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void copyXML()
        {
            this.model.CopyXML();
        }

        public void connectFG()
        {
            this.client.Connect();
        }
        

    }
}
