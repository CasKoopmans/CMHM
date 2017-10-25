using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections;
using HMonServer;
using System.ComponentModel;

namespace HMonDoc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TcpClient client;
        private SslStream stream;

        private string SessionNumber;
        private string PatientNumber;
        private bool IsFemale;

        private int[] time1, time2;
        private ChartValues<double> distance, burned, speed, power;
        private ChartValues<int> pulse, rotations, reachedpower;
        private string[] tstamps;
        private AbstractClient ServerConnection;

        public MainWindow(TcpClient client, SslStream stream)
        {
            this.client = client;
            this.stream = stream;
            InitializeComponent();
            this.ServerConnection = new Client(stream);
            this.DataContext = this;
            this.Graph();
        }

        private void OpenClickEvent(object sender, EventArgs args)
        {
            OpenSessionWindow osw;
            DataPacket dp;

            osw = new OpenSessionWindow();
            osw.ShowDialog();

            if (!osw.OkClicked)
                return;

            this.PatientNumber = osw.PatientNumber;
            this.SessionNumber = osw.SessionNumber;
            dp = new DataPacket();
            dp.CommandCode = 10;
            dp.Data = new DataMessage();
            dp.Data.PatientId = osw.PatientNumber;
            ServerConnection.Write(dp.ToString());
            dp = DataPacket.Deserialize(ServerConnection.Read());
            this.Name.Text = dp.Data.PatientName;
            this.Age.Text = dp.Data.PatientAge.ToString();
            this.Sex.Text = dp.Data.IsFemale ? "Female" : "Male";
            this.Age.Text = dp.Data.PatientAge.ToString();
            this.ID.Text = dp.Data.PatientId;
            this.Weight.Text = dp.Data.PatientWeight.ToString();
            this.IsFemale = dp.Data.IsFemale;
            this.getData();
        }

        private List<DataMessage> getAllData()
        {
            DataPacket dp;
            string raw;

            dp = new DataPacket();
            dp.Data = new DataMessage();
            dp.SessionId = this.SessionNumber;
            dp.Data.PatientId = this.PatientNumber;
            dp.CommandCode = 2;

            ServerConnection.Write(dp.Serialize());
            raw = ServerConnection.Read();
            List<DataMessage> many = DataPacket.DeserializeMany(raw);
            foreach(var x in many)
            {
                Console.Write("Data: ");
                Console.Write(x.Minutes + ":" + x.Seconds + "\n");
            }

            return many;
        }

        private double calculate(List<DataMessage> data)
        {
            double pwr, pulse, vo2;

            pwr = 0.0;
            pulse = 0.0;
            for(int idx = 5; idx > 0; idx--)
            {
                pwr += data[data.Count - idx].Resistance;
                pulse += data[data.Count - idx].Pulse;
            }

            pwr /= 5;
            pulse /= 5;
            pwr *= 6.1183;

            if(IsFemale)
            {
                vo2 = (1.8 * (pwr / Int32.Parse(Weight.Text)) + 7) * ((147 - Int32.Parse(Age.Text)) / (pulse - 73));
            } else
            {
                vo2 = (1.8 * (pwr / Int32.Parse(Weight.Text)) + 7) * ((137 - Int32.Parse(Age.Text)) / (pulse - 83));
            }

            return vo2;
        }

        private void getData()
        {
            List<DataMessage> data = this.getAllData();
            time1 = new int[data.Count];
            time2 = new int[data.Count];
            /*pulse = new ChartValues<int>();
            rotations = new ChartValues<int>();
            speed = new ChartValues<double>();
            power = new ChartValues<double>();
            reachedpower = new ChartValues<int>();
            distance = new ChartValues<double>();
            burned = new ChartValues<double>();*/
            tstamps = new string[time1.Length];

            SeriesCollection[0].Values.Clear();
            SeriesCollection[1].Values.Clear();
            SeriesCollection[2].Values.Clear();
            SeriesCollection[3].Values.Clear();
            SeriesCollection[4].Values.Clear();

            int idx = 0;
            foreach(var x in data) {
                time1[idx] = x.Minutes;
                time2[idx] = x.Seconds;
                SeriesCollection[0].Values.Add(x.Pulse);
                SeriesCollection[1].Values.Add(x.RPM);
                SeriesCollection[2].Values.Add((double)x.Speed);
                SeriesCollection[3].Values.Add((double)x.Energy);
                SeriesCollection[4 ].Values.Add((double)x.Resistance);
                idx++;
            }

            this.Labels = new string[data.Count];
            for(int i = 0; i < time1.Length; i++)
            {
                SimpleTime time = new SimpleTime(time1[i], time2[i]);
                Labels[i] = time.ToString();
            }

            VO2.Text = this.calculate(data).ToString();

        }

        #region Graph stuff
        private void Graph()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Heartrate (BPM)",
                    Values = new ChartValues<int>{ },
                    PointGeometry = null,
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "RPM",
                    Values = new ChartValues<int>{ },
                    PointGeometry = null,
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Speed (km/h)",
                    Values = new ChartValues<double>{ },
                    PointGeometry = null,
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Resistance",
                    Values = new ChartValues<double>{ },
                    PointGeometry = null,
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Energy (W)",
                    Values = new ChartValues<double>{ },
                    PointGeometry = null,
                    LineSmoothness = 0
                },

            };
            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        #endregion
    }
}
