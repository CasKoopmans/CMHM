using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HMonMD.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public class Records
    {
        public string Name
        {
            get;
            set;
        }
        public double Amount
        {
            get;
            set;
        }
    }

public sealed partial class SessionPage : Page
    {
        public static SessionPage SessionPageInstance;
        private List<Records> heartRateChart, RPMChart, SpeedChart, DistanceChart, ResistanceChart, EnergyKJChart, EnergyWChart;

        #region checkboxHandlers
        public void checkboxes()
        {
            CBHeartrate.IsChecked = true;
            CBRPM.IsChecked = true;
            CBSpeed.IsChecked = true;
            CBDistance.IsChecked = true;
            CBResistance.IsChecked = true;
            CBEnergyKJ.IsChecked = true;
            CBEnergyW.IsChecked = true;
        }

        private void CBHeartrate_Unchecked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { (lineChart.Series[0] as LineSeries).Visibility = Windows.UI.Xaml.Visibility.Collapsed; }
        private void CBHeartrate_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { (lineChart.Series[0] as LineSeries).Visibility = Windows.UI.Xaml.Visibility.Visible; }

        private void CBRPM_Unchecked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { (lineChart.Series[1] as LineSeries).Visibility = Windows.UI.Xaml.Visibility.Collapsed; }
        private void CBRPM_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { (lineChart.Series[1] as LineSeries).Visibility = Windows.UI.Xaml.Visibility.Visible; }

        private void CBSpeed_Unchecked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { (lineChart.Series[2] as LineSeries).Visibility = Windows.UI.Xaml.Visibility.Collapsed; }
        private void CBSpeed_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { (lineChart.Series[2] as LineSeries).Visibility = Windows.UI.Xaml.Visibility.Visible; }

        private void CBDistance_Unchecked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { (lineChart.Series[3] as LineSeries).Visibility = Windows.UI.Xaml.Visibility.Collapsed; }
        private void CBDistance_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { (lineChart.Series[3] as LineSeries).Visibility = Windows.UI.Xaml.Visibility.Visible; }

        private void CBResistance_Unchecked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { (lineChart.Series[4] as LineSeries).Visibility = Windows.UI.Xaml.Visibility.Collapsed; }
        private void CBResistance_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { (lineChart.Series[4] as LineSeries).Visibility = Windows.UI.Xaml.Visibility.Visible; }

        private void CBEnergyKJ_Unchecked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { (lineChart.Series[5] as LineSeries).Visibility = Windows.UI.Xaml.Visibility.Collapsed; }
        private void CBEnergyKJ_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { (lineChart.Series[5] as LineSeries).Visibility = Windows.UI.Xaml.Visibility.Visible; }

        private void CBEnergyW_Unchecked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { (lineChart.Series[6] as LineSeries).Visibility = Windows.UI.Xaml.Visibility.Collapsed; }
        private void CBEnergyW_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { (lineChart.Series[6] as LineSeries).Visibility = Windows.UI.Xaml.Visibility.Visible; }
        #endregion

        public SessionPage()
        {
            this.InitializeComponent();
            SessionPage.SessionPageInstance = this;
            LoadChart();
            updateChart();
            checkboxes();
            //updateSessionInfo();
        }

        /*public void updateSessionInfo(Point p)
        {
            Heartrate.Text = p.heartRate.ToString();
            RPM.Text = p.RPM.ToString();
            Speed.Text = p.speed.ToString();
            Distance.Text = p.distance.ToString();
            Resistance.Text = p.resistance.ToString();
            EnergyKJ.Text = p.energyKJ.ToString();
            EnergyW.Text = p.energyW.ToString();
            TimeSpan t = TimeSpan.FromSeconds(p.time);
            Time.Text = t.ToString(@"mm\:ss");
        }*/

        public void updateChart() {
            UpdateHeartRate("00:00", 150);
            UpdateRPM("00:00", 50);
            UpdateSpeed("00:00", 30);
            UpdateDistance("00:00", 00);
            UpdateResistance("00:00", 22);
            UpdateEnergyKJ("00:00", 100);
            UpdateEnergyW("00:00", 50);

            UpdateHeartRate("00:10", 160);
            UpdateRPM("00:10", 52);
            UpdateSpeed("00:10", 32);
            UpdateDistance("00:10", 10);
            UpdateResistance("00:10", 22);
            UpdateEnergyKJ("00:10", 110);
            UpdateEnergyW("00:10", 52);

            UpdateHeartRate("00:20", 155);
            UpdateRPM("00:20", 55);
            UpdateSpeed("00:20", 35);
            UpdateDistance("00:20", 10);
            UpdateResistance("00:20", 22);
            UpdateEnergyKJ("00:20", 105);
            UpdateEnergyW("00:20", 51);

            UpdateHeartRate("00:25", 155);
            UpdateRPM("00:25", 55);
            UpdateSpeed("00:25", 35);
            UpdateDistance("00:25", 10);
            UpdateResistance("00:25", 22);
            UpdateEnergyKJ("00:25", 105);
            UpdateEnergyW("00:25", 51);

            UpdateHeartRate("00:30", 155);
            UpdateRPM("00:30", 55);
            UpdateSpeed("00:30", 35);
            UpdateDistance("00:30", 10);
            UpdateResistance("00:30", 22);
            UpdateEnergyKJ("00:30", 105);
            UpdateEnergyW("00:30", 51);

            UpdateHeartRate("00:35", 155);
            UpdateRPM("00:35", 55);
            UpdateSpeed("00:35", 35);
            UpdateDistance("00:35", 10);
            UpdateResistance("00:35", 22);
            UpdateEnergyKJ("00:35", 105);
            UpdateEnergyW("00:35", 51);
        }

        private void LoadChart()
        {
            heartRateChart = new List<Records>();
            (lineChart.Series[0] as LineSeries).ItemsSource = heartRateChart;
            RPMChart = new List<Records>();
            (lineChart.Series[1] as LineSeries).ItemsSource = RPMChart;
            SpeedChart = new List<Records>();
            (lineChart.Series[2] as LineSeries).ItemsSource = SpeedChart;
            DistanceChart = new List<Records>();
            (lineChart.Series[3] as LineSeries).ItemsSource = DistanceChart;
            ResistanceChart = new List<Records>();
            (lineChart.Series[4] as LineSeries).ItemsSource = ResistanceChart;
            EnergyKJChart = new List<Records>();
            (lineChart.Series[5] as LineSeries).ItemsSource = EnergyKJChart;
            EnergyWChart = new List<Records>();
            (lineChart.Series[6] as LineSeries).ItemsSource = EnergyWChart;
        }

        private void UpdateHeartRate(string time, double variable)
        {
            heartRateChart.Add(new Records() { Name = time, Amount = variable });
        }
        private void UpdateRPM(string time, double variable)
        {
            RPMChart.Add(new Records() { Name = time, Amount = variable });
        }
        private void UpdateSpeed(string time, double variable)
        {
            SpeedChart.Add(new Records() { Name = time, Amount = variable });
        }
        private void UpdateDistance(string time, double variable)
        {
            DistanceChart.Add(new Records() { Name = time, Amount = variable });
        }
        private void UpdateResistance(string time, double variable)
        {
            ResistanceChart.Add(new Records() { Name = time, Amount = variable });
        }
        private void UpdateEnergyKJ(string time, double variable)
        {
            EnergyKJChart.Add(new Records() { Name = time, Amount = variable });
        }
        private void UpdateEnergyW(string time, double variable)
        {
            EnergyWChart.Add(new Records() { Name = time, Amount = variable });
        }
    }
}
