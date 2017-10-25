using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
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

namespace HMonDoc
{
    public partial class LoginWindow : Window, INotifyPropertyChanged
    {
        private bool _isloggedin;

        public bool IsLoggedIn
        {
            get { return _isloggedin; }
            set { _isloggedin = value;
                OnPropertyChanged("IsLoggedIn");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public LoginWindow()
        {
            this.IsLoggedIn = false;
            InitializeComponent();
            this.DataContext = this;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            TcpClient client;
            SslStream stream;

            try
            {
                string host = Regex.Replace(this.Hostname.Text, @"\t|\n|\r", "");
                string port = Regex.Replace(this.Portnumber.Text, @"\t|\n|\r", "");
                client = SslConnectionFactory.CreateTcpClient(host, Int32.Parse(port));
                stream = SslConnectionFactory.CreateFromClient(client);
                this.IsLoggedIn = true;
                new MainWindow(client, stream).Show();
                this.Close();
            } catch(Exception ex)
            {
                this.Status.Text = ex.Message;
            }
            

        }
    }
}
