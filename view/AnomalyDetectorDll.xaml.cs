using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlightSimulator2.viewModel;
using FlightSimulator2.model;
namespace FlightSimulator2.view
{
    /// <summary>
    /// Interaction logic for AnomalyDetectorDll.xaml
    /// </summary>
    public partial class AnomalyDetectorDll : UserControl
    {
        AnomalyDetectorDllVM vm_detector;
        int x = 0;
        List<User> items;
        public AnomalyDetectorDll()
        {
            InitializeComponent();
            this.vm_detector = new AnomalyDetectorDllVM(new AnomalyDetectorDllM());
            DataContext = vm_detector;
            items = new List<User>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".dll";
            // dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                this.vm_detector.VM_Dll_path = filename;
                dllFullPath.Text = filename;
            }
        }

        private void startDetect_Click(object sender, RoutedEventArgs e)
        {
            lvUsers.ItemsSource = null;
            List<anomalyReport> reports = new List<anomalyReport>();
            this.vm_detector.detect();
            reports = this.vm_detector.reports();
            int reports_number = reports.Count();
           // List<User>
             items = new List<User>();
            for (int i = 0; i < reports_number; i++)
            {
                ++x;
                items.Add(new User() { First = reports[i].first_element, Second = reports[i].second_element, Time = reports[i].timeStep,Detect = reports[i].detector_type});

            }
            lvUsers.ItemsSource = items;

        }

        private void normal_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".csv";
            // dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                this.vm_detector.VM_Learning_flight_path = filename;
                normalFlightFullPath.Text = filename;
            }
        }
    }
    public class User
    {
        public string First { get; set; }

        public string Second { get; set; }

        public string Time { get; set; }

        public string Detect { get; set; }
    }
}
