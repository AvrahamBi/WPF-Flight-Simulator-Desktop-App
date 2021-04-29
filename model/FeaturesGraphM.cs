using System;
using System.Collections.Generic;
using System.ComponentModel;
using OxyPlot;
using System.Linq;
using System.Text;
using System.Threading;
using FlightSimulator2.view;

namespace FlightSimulator2.model
{

    public class FeaturesGraphM : INotifyPropertyChanged
    {

        // index of selected feature
        private static int indexOfFeature;
        private List<DataPoint> regPoints, regPoints30;
        public static int IndexOfFeature
        {
            get { return indexOfFeature; }
            set
            {
                indexOfFeature = value;
            }
        }

        public List<DataPoint> RegPoints
        {
            get
            {
                return regPoints;
            }

            set
            {
                regPoints = value;
                NotifyPropertyChanged(nameof(RegPoints));
            }
        }

        public List<DataPoint> RegPoints30
        {
            get
            {
                return regPoints30;
            }

            set
            {
                regPoints30 = value;
                NotifyPropertyChanged(nameof(RegPoints30));
            }
        }

        // index of correlated feature
        private static int indexOfCorrelatedFeature;
        public static int IndexOfCorrelatedFeature
        {
            get { return indexOfCorrelatedFeature; }
            set
            {
                indexOfCorrelatedFeature = value;
            }
        }

        // this function updates the lists of points
        // using thread
        public void updateFeaturesList()
        {
            bool isConnected = true;
            long currentLine = 0;
            Line reg_line = new Line();
            new Thread(delegate ()
            {

                while (isConnected)
                {
                    Thread.Sleep(100);
                    if (Client.client_instance.currentFlightState() == null) { continue; }
                    string[] flightsInsturment;
                    flightsInsturment = Client.client_instance.currentFlightState().Split(',');



                    if (Client.client_instance.getCurrentLine() != -99)
                    {
                        // get current line number
                        currentLine = Client.client_instance.getCurrentLine();

                    }
                    // if user takes the control bar reverse
                    if (M_Points.Count != 0 && M_Points.Last().X > currentLine)
                    {
                        // clear the lists
                        M_Points.Clear();
                        M_CorrelatedPoints.Clear();
                        M_LineReg.Clear();
                    }
                    DataPoint feature = new DataPoint(currentLine, Convert.ToDouble(flightsInsturment[indexOfFeature]));
                    DataPoint correlated = new DataPoint(currentLine, Convert.ToDouble(flightsInsturment[indexOfCorrelatedFeature]));

                    M_Points.Add(feature);
                    M_CorrelatedPoints.Add(correlated);

                    regPoints30.Add(new DataPoint(feature.Y, correlated.Y));
                    if (regPoints30.Count() > 50)
                    {
                        regPoints.Add(regPoints30[0]);
                        regPoints30.RemoveAt(0);
                    }


                    if (M_Points.Count > 2)
                    {
                        M_LineReg.Clear();
                        reg_line.linear_regg(M_Points, M_CorrelatedPoints, M_Points.Count());

                        //Console.WriteLine(M_Points[0].X + " , " + reg_line.f(M_Points[0].Y));
                        M_LineReg.Add(new DataPoint(M_Points[0].X, reg_line.f(M_Points[0].Y)));
                        //Console.WriteLine(currentLine + " , " + reg_line.f(M_CorrelatedPoints[M_CorrelatedPoints.Count - 1].Y));
                        M_LineReg.Add(new DataPoint(currentLine, reg_line.f(M_CorrelatedPoints[M_CorrelatedPoints.Count - 1].Y)));
                    }


                    // M_LineReg.Add(feature);
                }
            }).Start();
        }

        // name of correlated feature
        private string correlatdeF;
        public string CorrelatedF
        {
            get { return correlatdeF; }
            set
            {
                correlatdeF = value;
                NotifyPropertyChanged(nameof(CorrelatedF));
            }
        }

        // list of all the feaures names by order of given csv file 
        private List<string> listOfFeaturesNames;
        public List<string> ListOfFeaturesNames
        {
            get { return listOfFeaturesNames; }
            set
            {
                listOfFeaturesNames = value;
                NotifyPropertyChanged(nameof(ListOfFeaturesNames));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // list of correlated points
        private List<DataPoint> M_correlatedPoints;
        public List<DataPoint> M_CorrelatedPoints
        {
            get { return M_correlatedPoints; }
            set
            {
                M_correlatedPoints = value;
                NotifyPropertyChanged(nameof(M_CorrelatedPoints));
            }
        }

        // list of points Reg
        private List<DataPoint> M_lineReg;
        public List<DataPoint> M_LineReg
        {
            get { return M_lineReg; }
            set
            {
                M_lineReg = value;
                NotifyPropertyChanged(nameof(M_LineReg));
            }
        }

        // list of points
        private List<DataPoint> M_points;
        public List<DataPoint> M_Points
        {
            get { return M_points; }
            set
            {
                M_points = value;
                NotifyPropertyChanged(nameof(M_Points));
            }
        }

        // function handles the event when user clicks 
        // on item in the list
        public void FeatureSelected(int selectedIndex)
        {
            M_Points.Clear();
            M_CorrelatedPoints.Clear();
            regPoints.Clear();
            regPoints30.Clear();
            indexOfFeature = selectedIndex;
            string feature = listOfFeaturesNames[selectedIndex];
            int index = 0;
            string correlatedF = getCorreltadFeature(feature);
            // find the index of the feature
            for (int i = 0; i < listOfFeaturesNames.Count; i++)
            {
                if (correlatedF == listOfFeaturesNames[i]) index = i;
            }
            indexOfCorrelatedFeature = index;
        }

        public void NotifyPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        // init according to xml file
        public void initFeaturesList()
        {
            this.M_Points = new List<DataPoint>();
            this.M_correlatedPoints = new List<DataPoint>();
            this.M_LineReg = new List<DataPoint>();

            listOfFeaturesNames = new List<string> { "aileron", "elevator", "rudder", "flaps", "slats", "speedbrake", "throttle", "throttle", "engine-pump", "engine-pump", // flight features according to XML file
                    "electric-pump", "electric-pump", "external-power", "APU-generator", "latitude-deg", "longitude-deg", "altitude-ft", "roll-deg", "pitch-deg",
                    "heading-deg", "side-slip-deg", "airspeed-kt", "glideslope", "vertical-speed-fps", "airspeed-indicator_indicated-speed-kt", "altimeter_indicated-altitude-ft",
                    "altimeter_pressure-alt-ft", "attitude-indicator_indicated-pitch-deg", "attitude-indicator_indicated-roll-deg", "attitude-indicator_internal-pitch-deg",
                    "attitude-indicator_internal-roll-deg", "encoder_indicated-altitude-ft", "encoder_pressure-alt-ft", "gps_indicated-altitude-ft", "gps_indicated-ground-speed-kt",
                    "gps_indicated-vertical-speed", "indicated-heading-deg", "magnetic-compass_indicated-heading-deg", "slip-skid-ball_indicated-slip-skid",
                    "turn-indicator_indicated-turn-rate", "vertical-speed-indicator_indicated-speed-fpm", "engine_rpm"
            };
        }

        // find correlated feature according to Learn Normal 
        public string getCorreltadFeature(string feature)
        {
            if (feature == "aileron") return "slip-skid-ball_indicated-slip-skid";
            if (feature == "elevator") return "altimeter_indicated-altitude-ft";
            if (feature == "rudder") return "aileron";
            if (feature == "flaps") return "aileron";
            if (feature == "slats") return "aileron";
            if (feature == "speedbrake") return "aileron";
            if (feature == "throttle") return "engine_rpm";
            if (feature == "throttle1") return "aileron";
            if (feature == "engine-pump") return "aileron";
            if (feature == "engine-pump1") return "aileron";
            if (feature == "electric-pump") return "aileron";
            if (feature == "electric-pump1") return "aileron";
            if (feature == "external-power") return "aileron";
            if (feature == "APU-generator") return "aileron";
            if (feature == "latitude-deg") return "aileron";
            if (feature == "longitude-deg") return "aileron";
            if (feature == "altitude-ft") return "airspeed-indicator_indicated-speed-kt";
            if (feature == "roll-deg") return "attitude-indicator_internal-roll-deg";
            if (feature == "pitch-deg") return "attitude-indicator_internal-pitch-deg";
            if (feature == "heading-deg") return "indicated-heading-deg";
            if (feature == "side-slip-deg") return "airspeed-kt";
            if (feature == "airspeed-kt") return "airspeed-indicator_indicated-speed-kt";
            if (feature == "glideslope") return "vertical-speed-fps";
            if (feature == "vertical-speed-fps") return "gps_indicated-vertical-speed";
            if (feature == "airspeed-indicator_indicated-speed-kt") return "gps_indicated-ground-speed-kt";
            if (feature == "altimeter_indicated-altitude-ft") return "altimeter_pressure-alt-ft";
            if (feature == "altimeter_pressure-alt-ft") return "encoder_pressure-alt-ft";
            if (feature == "attitude-indicator_indicated-pitch-deg") return "attitude-indicator_internal-pitch-deg";
            if (feature == "attitude-indicator_indicated-roll-deg") return "attitude-indicator_internal-roll-deg";
            if (feature == "attitude-indicator_internal-pitch-deg") return "gps_indicated-vertical-speed";
            if (feature == "attitude-indicator_internal-roll-deg") return "turn-indicator_indicated-turn-rate";
            if (feature == "encoder_indicated-altitude-ft") return "encoder_pressure-alt-ft";
            if (feature == "encoder_pressure-alt-ft") return "gps_indicated-altitude-ft";
            if (feature == "gps_indicated-altitude-ft") return "gps_indicated-ground-speed-kt";
            if (feature == "gps_indicated-ground-speed-kt") return "slip-skid-ball_indicated-slip-skid";
            if (feature == "gps_indicated-vertical-speed") return "vertical-speed-indicator_indicated-speed-fpm";
            if (feature == "indicated-heading-deg") return "engine_rpm";
            if (feature == "magnetic-compass_indicated-heading-deg") return "turn-indicator_indicated-turn-rate";
            if (feature == "slip-skid-ball_indicated-slip-skid") return "turn-indicator_indicated-turn-rate";
            if (feature == "turn-indicator_indicated-turn-rate") return "vertical-speed-indicator_indicated-speed-fpm";
            if (feature == "vertical-speed-indicator_indicated-speed-fpm") return "engine_rpm";
            if (feature == "engine_rpm") return "aileron";
            return "aileron";
        }


        public FeaturesGraphM()
        {
            regPoints = new List<DataPoint>();
            regPoints30 = new List<DataPoint>();
            initFeaturesList();
            updateFeaturesList();
        }
    }
}