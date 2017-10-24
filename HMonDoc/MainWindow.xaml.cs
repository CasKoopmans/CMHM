using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections;

namespace HMonDoc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TcpClient client;
        private SslStream stream;

        private int[] time1, time2;
        private ChartValues<double> distance, burned;
        private ChartValues<int> pulse, rotations, speed, power, reachedpower;
        string[] tstamps;

        public MainWindow(TcpClient client, SslStream stream)
        {
            this.client = client;
            this.stream = stream;
            InitializeComponent();
            this.DataContext = this;
            getData();
            Graph();
        }

        private void getData()
        {
            time1 = new int[] { 00, 00, 00, 00, 00, 00, 01, 01, 01 };
            time2 = new int[] { 00, 10, 20, 30, 40, 50, 00, 10, 20 };
            tstamps = new string[time1.Length];

            for(int i = 0; i < time1.Length; i++)
            {
                SimpleTime time = new SimpleTime(time1[i], time2[i]);
                tstamps[i] = time.ToString();
            }
            
            pulse = new ChartValues<int>{ 100, 103, 105, 108, 115, 116, 120, 130, 132 };
            rotations = new ChartValues<int> { 70, 68, 70, 72, 79, 78, 73, 74, 75 };
            speed = new ChartValues<int> { 30, 30, 30, 30, 30, 30, 30, 30, 30 };
            power = new ChartValues<int> { 50, 50, 50, 50, 50, 50, 50, 50, 50 };
            reachedpower = new ChartValues<int> { 40, 40, 40, 40, 40, 40, 40, 40, 40 };
            distance = new ChartValues<double> { 10, 20, 30, 40, 50, 60, 70, 80, 90 };
            burned = new ChartValues<double> { 20, 30, 40, 50, 60, 70, 80, 90, 99 };
        }

        #region Graph stuff
        private void Graph()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Heartrate (BPM)",
                    Values = pulse,
                    PointGeometry = null,
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "RPM",
                    Values = rotations,
                    PointGeometry = null,
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Speed (km/h)",
                    Values = speed,
                    PointGeometry = null,
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Distance (km)",
                    Values = distance,
                    PointGeometry = null,
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Resistance",
                    Values = power,
                    PointGeometry = null,
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Energy (kJ)",
                    Values = burned,
                    PointGeometry = null,
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Energy (W)",
                    Values = reachedpower,
                    PointGeometry = null,
                    LineSmoothness = 0
                }
            };
            
            Labels = tstamps;
            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        #endregion
    }
}
