using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
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

            //client = SslConnectionFactory.CreateTcpClient(Hostname.Text, Int32.Parse(Portnumber.Text));
            //stream = SslConnectionFactory.CreateFromClient(client);
            //this.IsLoggedIn = true;
            new MainWindow(null, null).Show();
            this.Close();
        }
    }
}
