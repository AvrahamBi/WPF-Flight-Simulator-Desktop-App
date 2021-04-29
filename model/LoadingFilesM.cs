using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace FlightSimulator2.model
{
    class LoadingFilesM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private String from_playback = @"C:\Downloads";
        public String From_playback
        {
            get
            {

                return from_playback;
            }
            set
            {
                from_playback = value;
                NotifyPropertyChanged("From_playback");
            }
        }

        private String to_playback = @"C:\Program Files\FlightGear 2020.3.6\data\Protocol";
        public String To_playback
        {
            get
            {

                return to_playback;
            }
            set
            {
                to_playback = value;
                NotifyPropertyChanged("To_playback");
            }
        }

        public void CopyXML()
        {
            try
            {
                string fileName = "playback_small.xml";
                string sourceFile = System.IO.Path.Combine(this.from_playback, fileName);
                string destFile = System.IO.Path.Combine(this.to_playback, fileName);
                File.Copy(sourceFile, destFile, true);
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
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
