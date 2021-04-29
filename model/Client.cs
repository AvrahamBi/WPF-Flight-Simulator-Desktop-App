using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Xml;
namespace FlightSimulator2.model
{
    class Client : INotifyPropertyChanged
    {


        TcpClient tcpClient;
        NetworkStream stream;
        PlayerControlBarM control_bar;
        List<string> xml_place;
        // singelton! 
        private static Client client = new Client();
        public static Client client_instance
        {
            get
            {
                if (client == null)
                {
                    client = new Client();
                }
                return client;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        /**Gets and Sets**/
        private String from_reg = @"C:\Users\Omer\source\repos\FlightSimulator2\model";
        public String From_reg
        {
            get
            {
                return from_reg;
            }
            set
            {
                from_reg = value;
                NotifyPropertyChangedtify("From_reg");
            }
        }

        private String ip = "127.0.0.1";
        public String IP
        {
            get
            {
                return ip;
            }
            set
            {
                ip = value;
                NotifyPropertyChangedtify("IP");
            }
        }


        private int port = 5400;
        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
                NotifyPropertyChangedtify("Port");
            }
        }

        private double speedRatio = 1.0;
        public double SpeedRatio
        {
            get
            {
                return speedRatio;
            }
            set
            {
                speedRatio = value;
                NotifyPropertyChangedtify("SpeedRatio");
            }
        }
        private string xml_File = null;
        public string Xml_file
        {
            get
            {
                return xml_File;
            }
            set
            {
                xml_File = value;
                NotifyPropertyChangedtify("Xml_file");
            }
        }


        /**Constractur**/
        private Client()
        {
            this.tcpClient = new TcpClient();
        }


        /**Connect to FG**/
        public void Connect()
        {
            client_instance.tcpClient = new TcpClient();
            //ip and port binding from view
            client_instance.tcpClient.Connect(this.IP, this.Port);
            this.stream = this.tcpClient.GetStream();

            //build the path for reg_flight.csv
            string fileIni = "reg_flight.csv";
            string transIniFullFileName = Path.Combine(this.from_reg, fileIni);
            //start sending CSV to FG
            client.SendCSV(transIniFullFileName);
        }


        public void Disconnect()
        {
            try
            {
                // Close networkStream.
                this.tcpClient.GetStream().Close();
            }// The networkStream was already closed or something else happen.
            catch (Exception) { }
            try
            {
                // Close tcpClient.
                this.tcpClient.Close();
            }// The tcpclnt was already closed or something else happen.
            catch (Exception) { }
            this.tcpClient = null;
        }

        public void SendCSV(String CSVsrc)
        {
            StreamReader csv = new StreamReader(CSVsrc);
            int SpeedTransport = 1;
            control_bar.setFlight(csv); // get the flight data
            control_bar.flightAnalysis(); // analysis the flight data.
            String line_reg = control_bar.getFlightState();
            new Thread(delegate ()
            {

                while (line_reg != null)
                {
                    
                    //read a line from CSV reg
                    line_reg += "\n";
                    Byte[] lineInBytes = System.Text.Encoding.ASCII.GetBytes(line_reg);
                    // Send the message to the connected TcpServer
                    /**Console.WriteLine(line);*/ //try to print the lines of the reg_file 
                    this.stream.Write(lineInBytes, 0, lineInBytes.Length);
                    //read the next line
                    line_reg = control_bar.getFlightState();
                    // line_reg = csv.ReadLine();
                    //sleeping for 100 ms 
                    try
                    {
                        SpeedTransport = Convert.ToInt32(100 / this.speedRatio);
                        Thread.Sleep(SpeedTransport);
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(100);
                    }


                }
                csv.Close();
            }).Start();


        }


        public void Write(string command)
        {
            if (this.tcpClient != null)
            {
                this.stream = this.tcpClient.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] b = asen.GetBytes(command);
                this.stream.Write(b, 0, b.Length);
            }
        }


        public string Read()
        {
            if (this.tcpClient != null)
            {
                // Time out of 10 seconds.
                this.tcpClient.ReceiveTimeout = 10000;
                this.stream.ReadTimeout = 10000;
                // Only if the ReceiveBufferSize not empty so we want to convert the message to string and return it.
                if (this.tcpClient.ReceiveBufferSize > 0)
                {
                    byte[] bb = new byte[this.tcpClient.ReceiveBufferSize];
                    int k = this.stream.Read(bb, 0, 100);
                    string massage = "";
                    for (int i = 0; i < k; i++)
                    {
                        massage += (Convert.ToChar(bb[i]));
                    }
                    return massage;
                }
            }
            return "ERR";
        }


        public void NotifyPropertyChangedtify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public void setControlBar(PlayerControlBarM m)
        {
            this.control_bar = m;
        }

        public string currentFlightState()
        {
            // avraham added this
            if (this.control_bar is null) return null;

            else return this.control_bar.currentState();
        }

        // avraham added this
        public long getCurrentLine()
        {
            int counter = 0;
            if (++counter > 1000)
            {
                int c = 8;
            }

            // avraham added this
            if (this.control_bar is null) return -99;

            
            else return this.control_bar.Current_line;

            
        }
        public void xmlVectorCreate()
        {
                xml_place = new List<string>();
                XmlDocument doc = new XmlDocument();
                doc.Load(this.xml_File + "/playback_small.xml");

                //Display all the names titles.
                XmlNodeList elemList = doc.GetElementsByTagName("name");
                for (int i = 0; i < elemList.Count; i++)
                {
                     xml_place.Add(elemList[i].InnerXml);
                    //  Console.WriteLine(elemList[i].InnerXml); test
               
                 }
             
        }
        public string xmlPlace(int i)
        {
            if (i >= xml_place.Count()) return null;
            return xml_place.ElementAt(i);
        }
        public long numberOfRows()
        {
            return this.control_bar.rowsNumber();
        }
    }

}
