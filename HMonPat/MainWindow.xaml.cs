using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net.Security;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HMonPat
{
    public partial class MainWindow : Window
    {
        int age, weight;
        public int maxHF;
        double VO2;
        string name, hometown, id;
        bool isFemale;
        private Client ServerConnection;

        static SerialPort _serialPort;
        private List<Measurement> Measurements;

        private TcpClient client;
        private SslStream stream;
        private string PatientId;
        private string SessionId;

        public MainWindow(TcpClient client, SslStream stream)
        {
            this.client = client;
            this.stream = stream;
            InitializeComponent();
            this.DataContext = this;
            this.ServerConnection = new Client(stream);

            if(age < 25) { maxHF = 210; }
            else if (age < 35) { maxHF = 200; }
            else if (age < 40) { maxHF = 190; }
            else if (age < 45) { maxHF = 180; }
            else if (age < 50) { maxHF = 170; }
            else if (age < 55) { maxHF = 160; }
            else { maxHF = 150; }

            Measurements = new List<Measurement>();
        }

        private void Read(string message)
        {
            try
            {
                string[] data = message.Split('\t');

                int pulse, rotations, speed, power, reachedpower, time1, time2;
                double distance, burned;
                Int32.TryParse(data[0], out pulse);
                Int32.TryParse(data[1], out rotations);
                Int32.TryParse(data[2], out speed);
                Double.TryParse(data[3], out distance);
                Int32.TryParse(data[4], out power);
                Double.TryParse(data[5], out burned);
                Int32.TryParse(data[7], out reachedpower);

                string[] time = data[6].Split(':');
                Int32.TryParse(time[0], out time1);
                Int32.TryParse(time[1], out time2);

                Measurements.Add(new Measurement(
                        //pulse, 
                        135,
                        rotations, speed / 10, power, distance, burned, time1, time2, reachedpower
                    ));
                Data.Text = Measurements[Measurements.Count - 1].ToString();
            }
            catch (Exception e) { }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _serialPort = new SerialPort("COM3")
                {
                    BaudRate = 9600,
                    Parity = Parity.None,
                    StopBits = StopBits.One,
                    DataBits = 8,
                    Handshake = Handshake.None,
                    ReadTimeout = 2000,
                    WriteTimeout = 500
                };
                _serialPort.Open();
                StartUp();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
            }

            DataPacket dp = new DataPacket();
            dp.Data = new DataMessage();
            dp.CommandCode = DataPacket.CREATE_SESSION;
            dp.Data.PatientId = this.PatientId;
            ServerConnection.Write(dp.Serialize());
            dp = DataPacket.Deserialize(ServerConnection.Read());
            this.SessionId = dp.SessionId;
            this.SessionID.Text = this.SessionId;
        }

        private void StartUp()
        {
            ConnectButton.Visibility = Visibility.Collapsed;
            _serialPort.WriteLine("RS");
            _serialPort.ReadLine();
            Thread.Sleep(100);
            _serialPort.WriteLine("CM");
            _serialPort.ReadLine();

            Data.Text = "";

            Test test = new Test(this);
        }

        public async void askData()
        {
            _serialPort.WriteLine("ST");
            string response = "";
            try
            {
                response = _serialPort.ReadLine();
                Console.WriteLine(response);
                Read(response);
            }
            catch (Exception ex) { }
        }

        public void SetTimeBox(string time)
        {
            if(!Dispatcher.CheckAccess())
            {
                this.TimeBox.Dispatcher.BeginInvoke((Action)(() => this.TimeBox.Text = time));
            }

        }

        public int getLastPulse()
        {
            return Measurements[Measurements.Count - 1].Pulse;
        }

        public int getLastRPM()
        {
            return Measurements[Measurements.Count - 1].Rotations;
        }

        public int getLastResistance()
        {
            return Measurements[Measurements.Count - 1].Power;
        }

        public async void setResistance(int resistance)
        {
            _serialPort.WriteLine("CM");
            _serialPort.ReadLine();
            await Task.Delay(400);
            _serialPort.WriteLine("PW " + resistance.ToString());
            _serialPort.ReadLine();
        }

        public void setStateText(string text)
        {
            StateText.Foreground = new SolidColorBrush(Colors.Black);
            StateText.Text = text;
        }

        public void EmergencyBreak()
        {
            Feedback.Text = "";
            StateText.Text = "EMERGENCY BREAK";
            StateText.Foreground = new SolidColorBrush(Colors.Red);
        }

        public void setFeedbackText(string text)
        {
            Feedback.Text = text;
        }

        public void CloseSerial()
        {
            _serialPort.Close();
        }

        private void createHardTest()
        {
            Measurements.Add(new Measurement(122, 54, 75, 80, 30, 30, 2, 0, 80));
            Measurements.Add(new Measurement(128, 55, 76, 125, 45, 40, 3, 0, 125));
            Measurements.Add(new Measurement(134, 56, 77, 150, 60, 50, 4, 0, 150));
            Measurements.Add(new Measurement(134, 57, 78, 150, 65, 60, 4, 15, 150));
            Measurements.Add(new Measurement(131, 58, 79, 150, 70, 70, 4, 30, 150));
            Measurements.Add(new Measurement(133, 59, 80, 150, 75, 80, 4, 45, 150));
            Measurements.Add(new Measurement(134, 58, 79, 150, 80, 85, 5, 0, 150));
            Measurements.Add(new Measurement(131, 55, 76, 150, 85, 90, 5, 15, 150));
            Measurements.Add(new Measurement(132, 52, 73, 150, 90, 95, 5, 30, 150));
            Measurements.Add(new Measurement(132, 57, 78, 150, 95, 100, 5, 45, 150));
            Measurements.Add(new Measurement(131, 54, 75, 150, 100, 105, 6, 0, 150));
        }

        private void CreatePatientClickEvent(object sender, RoutedEventArgs e)
        {
            CreatePatientWindow window = new CreatePatientWindow();
            DataPacket dp;
            DataMessage dm;

            window.ShowDialog();
            dp = new DataPacket();
            dm = new DataMessage();

            dp.CommandCode = DataPacket.CREATE_PATIENT;
            dm.PatientId = window.id;
            dm.PatientAge = Int32.Parse(window.age);
            dm.PatientName = window.name;
            dm.PatientWeight = Int32.Parse(window.weight);
            dp.Data = dm;
            ServerConnection.Write(dp.Serialize());
        }

        private void PatientLoginClickEvent(object sender, RoutedEventArgs e)
        {
            PatientLoginWindow window = new PatientLoginWindow();
            DataPacket dp = new DataPacket();

            window.ShowDialog();
            if (!window.OK)
                return;

            dp.CommandCode = DataPacket.GET_PATIENT_INFO;
            dp.Data = new DataMessage();
            dp.Data.PatientId = window.PatientId;
            ServerConnection.Write(dp.Serialize());
            dp = DataPacket.Deserialize(ServerConnection.Read());

            if (dp.ErrorCode != 0)
            {
                return;
            }

            this.Name.Text = dp.Data.PatientName;
            this.Age.Text = dp.Data.PatientAge.ToString();
            this.Sex.Text = dp.Data.IsFemale ? "Female" : "Male";
            this.Age.Text = dp.Data.PatientAge.ToString();
            this.ID.Text = dp.Data.PatientId;
            this.Weight.Text = dp.Data.PatientWeight.ToString();
            this.PatientId = window.PatientId;
        }

        private void calculate()
        {
            double averagePower = ((Measurements[Measurements.Count - 5].Power + Measurements[Measurements.Count - 4].Power + 
                Measurements[Measurements.Count - 3].Power +
                Measurements[Measurements.Count - 2].Power + Measurements[Measurements.Count - 1].Power) / 5) * 6.1183;
            double averagePulse = (Measurements[Measurements.Count - 5].Pulse + 
                Measurements[Measurements.Count - 4].Pulse + Measurements[Measurements.Count - 3].Pulse + 
                Measurements[Measurements.Count - 2].Pulse + Measurements[Measurements.Count - 1].Pulse) / 5; 

            if (isFemale)
            {
                VO2 = (1.8 * (averagePower / weight) + 7) * ((147 - age) / (averagePulse - 73));
            }
            else
            {
                VO2 = (1.8 * (averagePower / weight) + 7) * ((137 - age) / (averagePulse - 83));
            }
        }

        private void Send()
        {
            List<DataPacket> dps = new List<DataPacket>();

            foreach(var measurement in this.Measurements)
            {
                DataPacket dp = new DataPacket();
                dp.Data = new DataMessage();

                dp.CommandCode = DataPacket.STORE;
                dp.SessionId = this.SessionId;
                dp.Data.Pulse = measurement.Pulse;
                dp.Data.Resistance = measurement.Power;
                dp.Data.PatientId = this.PatientId;
                dp.Data.Energy = measurement.ReachedPower;
                dp.Data.Speed = measurement.Speed;
                dp.Data.Seconds = measurement.Time.Seconds;
                dp.Data.Minutes = measurement.Time.Minutes;
                dp.Data.RPM = measurement.Rotations;

                ServerConnection.Write(dp.Serialize());
            }
        }

        public void Done()
        {
            CloseSerial();
            calculate();
            this.Send();


            StateText.Text = "Test done.\n";
            VO2 = Math.Round(VO2, 2);
            StateText.Text += "VO2: " + VO2.ToString() + " mL/(kg min)";
        }
    }
}
