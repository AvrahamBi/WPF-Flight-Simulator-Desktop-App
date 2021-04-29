using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ComponentModel;
namespace FlightSimulator2.model
{
static class NativeMethods // Using kernel dll for finding the process! 
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);
        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);
    }

        class anomalyReport // anomally reports class 
        {
            public string first_element;
            public string second_element;
            public string timeStep;
            public string detector_type;
            public anomalyReport(string f1, string f2, string t,string dt)
            {
                first_element = f1;
                second_element = f2;
                timeStep = t;
                detector_type = dt;
            }
        }
/*
 *  Start of model implement!
 */
    class AnomalyDetectorDllM : INotifyPropertyChanged
    {
        // dll methods
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr CreatestringWrapper(string s2); // wrap the string for easy move from c# to c++
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr Creates(IntPtr csv); // create the Detector and detect Anomaly.
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate  IntPtr createTimeSeries(IntPtr csv, int s);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr learnNormal(IntPtr sad, IntPtr csv); // learning  valid flight,
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr anomalyData(IntPtr sad, IntPtr csv); // return the anomaly data (vector)
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate double anomalyLength(IntPtr v); // know how many anomaly there. 
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int anomalyFirstElemnet(IntPtr v, int place); // return the first attribute of the anomaly
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int anomalyScondElemnet(IntPtr v, int place); // return the second attribute of the anomaly.
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int anomalyTime(IntPtr v, int place); //  return the time of the anomaly happand.

        // fields
        public event PropertyChangedEventHandler PropertyChanged;
        string dll_path;
        string learning_flight_path;
        string real_flight_path;
        List<anomalyReport> anomaly_reports;
        int report_size;

        // proporties
       public string Dll_path
        {
            get
            {
                return dll_path;
            }
            set
            {
                dll_path = value;
                NotifyPropertyChanged("Dll_path");
            }
        }
        public string Learning_flight_path
        {
            get
            {
                return learning_flight_path;
            }
            set
            {
                learning_flight_path = value;
                NotifyPropertyChanged("Learning_flight_path");
            }
        }


        //constructor 
        public AnomalyDetectorDllM()
        {
            anomaly_reports = new List<anomalyReport>();
            learning_flight_path = null;
            real_flight_path = null;
            dll_path = null;
        }
        public void Anomalychoosen()
        {
            // clear the reports (for the case the user changed the dll)
            this.report_size = 0;
            this.anomaly_reports.Clear();
            this.real_flight_path = Client.client_instance.From_reg;
            this.real_flight_path += "\\reg_flight.csv";
            if (this.dll_path == null) return;
            // first wrap the string!
            IntPtr pDll = NativeMethods.LoadLibrary(dll_path);
            IntPtr pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "CreatestringWrapper");
            CreatestringWrapper wrap = (CreatestringWrapper)Marshal.GetDelegateForFunctionPointer(
                                                                        pAddressOfFunctionToCall,
                                                                        typeof(CreatestringWrapper));
            IntPtr a = wrap(learning_flight_path);
            // create a csv file!
            IntPtr pDll2 = NativeMethods.LoadLibrary(dll_path);
            IntPtr pAddressOfFunctionToCall2 = NativeMethods.GetProcAddress(pDll2, "createTimeSeries");
            createTimeSeries toCsv = (createTimeSeries)Marshal.GetDelegateForFunctionPointer(
                                                                        pAddressOfFunctionToCall2,
                                                                        typeof(createTimeSeries));
            IntPtr time_series = toCsv(a, 42);

            // create the Detector and learn
            IntPtr pDll3 = NativeMethods.LoadLibrary(dll_path);
            IntPtr pAddressOfFunctionToCall3 = NativeMethods.GetProcAddress(pDll3, "Creates");
            Creates createAndDetect = (Creates)Marshal.GetDelegateForFunctionPointer(
                                                                        pAddressOfFunctionToCall3,
                                                                        typeof(Creates));
            IntPtr detector = createAndDetect(time_series);

            /*
             * now we find the anomaly
             */
            IntPtr anomly_wrapper = wrap(this.real_flight_path);
            IntPtr anomaly_time_series = toCsv(anomly_wrapper, 42);

            // anomaly data
            IntPtr pDll4 = NativeMethods.LoadLibrary(dll_path);
            IntPtr pAddressOfFunctionToCall4 = NativeMethods.GetProcAddress(pDll4, "anomalyData");
            anomalyData anomalyD = (anomalyData)Marshal.GetDelegateForFunctionPointer(
                                                                        pAddressOfFunctionToCall4,
                                                                        typeof(anomalyData));
            IntPtr anomaly_report = anomalyD(detector, anomaly_time_series);

            // find the number of anomaly data
            IntPtr pDll5 = NativeMethods.LoadLibrary(dll_path);
            IntPtr pAddressOfFunctionToCall5 = NativeMethods.GetProcAddress(pDll5, "anomalyLength");
            anomalyLength anomalyLength = (anomalyLength)Marshal.GetDelegateForFunctionPointer(
                                                                        pAddressOfFunctionToCall5,
                                                                        typeof(anomalyLength));
            double n = anomalyLength(anomaly_report);

            // initalize the first and second element dll
            IntPtr pDll6 = NativeMethods.LoadLibrary(dll_path);
            IntPtr pAddressOfFunctionToCall6 = NativeMethods.GetProcAddress(pDll6, "anomalyFirstElemnet");
            anomalyFirstElemnet anomalyFirst = (anomalyFirstElemnet)Marshal.GetDelegateForFunctionPointer(
                                                                        pAddressOfFunctionToCall6, typeof(anomalyFirstElemnet));

            IntPtr pDll7 = NativeMethods.LoadLibrary(dll_path);
            IntPtr pAddressOfFunctionToCall7 = NativeMethods.GetProcAddress(pDll7, "anomalyScondElemnet");
            anomalyScondElemnet anomalyScond = (anomalyScondElemnet)Marshal.GetDelegateForFunctionPointer(
                                                                        pAddressOfFunctionToCall7,
                                                                        typeof(anomalyScondElemnet));

            IntPtr pDll8 = NativeMethods.LoadLibrary(dll_path);
            IntPtr pAddressOfFunctionToCall8 = NativeMethods.GetProcAddress(pDll8, "anomalyTime");
            anomalyTime anomalyTime = (anomalyTime)Marshal.GetDelegateForFunctionPointer(
                                                                        pAddressOfFunctionToCall8,
                                                                      typeof(anomalyTime));

            string[] path_parts;
            path_parts = this.dll_path.Split('\\');
            string detector_name = path_parts[path_parts.Length - 1];
        for (int i = 0; i < n; i++)
            {
                int first_index = anomalyFirst(anomaly_report, i);
                string first = Client.client_instance.xmlPlace(first_index);
                int second_index = anomalyScond(anomaly_report, i);
                string second = Client.client_instance.xmlPlace(second_index);
                int num = anomalyTime(anomaly_report, i);
                string num_time = convertToTime(num);
                anomaly_reports.Add(new anomalyReport(first, second, num_time, detector_name));

            }
            this.report_size = anomaly_reports.Count();
    }

        public int reportSize()
    {
        return this.report_size;
    }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public string convertToTime(int n)
        {
            long x = (long)(n / (100 * 0.001));
            TimeSpan time = TimeSpan.FromSeconds(x);
            string displayTime = time.ToString(@"hh\:mm\:ss\:fff");
            return displayTime;
        }
        public List<anomalyReport> Reports()
        {
            return this.anomaly_reports;
        }
    }
}
