using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HMonPatient.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SessionPage : Page
    {
        private SerialDevice serialPort = null;
        DataWriter dataWriteObject = null;
        DataReader dataReaderObject = null;

        private ObservableCollection<DeviceInformation> listOfDevices;
        private CancellationTokenSource ReadCancellationTokenSource;
        private List<Measurement> Measurements;

        public SessionPage()
        {
            this.InitializeComponent();
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                listOfDevices = new ObservableCollection<DeviceInformation>();
                string aqs = SerialDevice.GetDeviceSelector();
                var dis = await DeviceInformation.FindAllAsync(aqs);

                for (int i = 0; i < dis.Count; i++)
                {
                    listOfDevices.Add(dis[i]);
                }
            }
            catch(Exception ex){ }
            try
            {
                //serialPort = await SerialDevice.FromIdAsync("\\\\USB#VID_10C4&PID_EA60#0001#{6e0d1e0-8089-11d0-9ce4-08003e301f73}");
                serialPort = await SerialDevice.FromIdAsync(listOfDevices[0].Id);
                if (serialPort == null) return;

                // Configure serial settings
                serialPort.WriteTimeout = TimeSpan.FromMilliseconds(500);
                serialPort.ReadTimeout = TimeSpan.FromMilliseconds(2000);
                serialPort.BaudRate = 9600;
                serialPort.Parity = SerialParity.None;
                serialPort.StopBits = SerialStopBitCount.One;
                serialPort.DataBits = 8;
                serialPort.Handshake = SerialHandshake.None;

                // Create cancellation token object to close I/O operations when closing the device
                ReadCancellationTokenSource = new CancellationTokenSource();

                Listen();

                dataWriteObject = new DataWriter(serialPort.OutputStream);
                StartUp();
            }
            catch (Exception ex)
            {
                Data.Text = ex.ToString();
            }
        }

        private async void StartUp()
        {
            ConnectButton.Visibility = Visibility.Collapsed;
            await WriteAsync("RS");
            await Task.Delay(1500);
            await WriteAsync("CM");
            await Task.Delay(500);

            await WriteAsync("RS");
            await Task.Delay(1500);
            await WriteAsync("CM");
            await Task.Delay(2500);

            Data.Text = "";

            Test test = new Test(this);
        }

        private async Task WriteAsync(string command)
        {
            Task<UInt32> storeAsyncTask;
                // Load the text from the sendText input text box to the dataWriter object
                //dataWriteObject.WriteString(sendText.Text);
                dataWriteObject.WriteString(command + "\r\n");

                // Launch an async task to complete the write operation
                storeAsyncTask = dataWriteObject.StoreAsync().AsTask();

                UInt32 bytesWritten = await storeAsyncTask;
        }

        private async void Listen()
        {
            try
            {
                if (serialPort != null)
                {
                    dataReaderObject = new DataReader(serialPort.InputStream);
                    Measurements = new List<Measurement>();
                    // keep reading the serial input
                    while (true)
                    {
                        await ReadAsync(ReadCancellationTokenSource.Token);
                    }
                }
            }
            catch (TaskCanceledException tce)
            {
                CloseDevice();
            }
            catch (Exception ex)
            {
              
            }
            finally
            {
                // Cleanup once complete
                if (dataReaderObject != null)
                {
                    dataReaderObject.DetachStream();
                    dataReaderObject = null;
                }
            }
        }

        private async Task ReadAsync(CancellationToken cancellationToken)
        {
            Task<UInt32> loadAsyncTask;

            uint ReadBufferLength = 1024;

            // If task cancellation was requested, comply
            cancellationToken.ThrowIfCancellationRequested();

            // Set InputStreamOptions to complete the asynchronous read operation when one or more bytes is available
            dataReaderObject.InputStreamOptions = InputStreamOptions.Partial;

            using (var childCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                // Create a task object to wait for data on the serialPort.InputStream
                loadAsyncTask = dataReaderObject.LoadAsync(ReadBufferLength).AsTask(childCancellationTokenSource.Token);

                // Launch the task and wait
                UInt32 bytesRead = await loadAsyncTask;
                if (bytesRead > 0)
                {
                    try {
                        string[] data = dataReaderObject.ReadString(bytesRead).Split('\t');

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
                                pulse, rotations, speed/10, power, distance, burned, time1, time2, reachedpower
                            ));
                        Data.Text = Measurements[Measurements.Count - 1].ToString();
                    }
                    catch (Exception e) { }
                }
            }
        }

        private void CloseDevice()
        {
            if (serialPort != null)
            {
                serialPort.Dispose();
            }
            serialPort = null;
            listOfDevices.Clear();
        }

        public async void askData()
        {
            await WriteAsync("ST");
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
            await WriteAsync("CM ");
            await Task.Delay(400);
            await WriteAsync("PW " + resistance.ToString());
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
    }
}
