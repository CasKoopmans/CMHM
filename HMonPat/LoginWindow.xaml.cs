using System.ComponentModel;
using System.Net.Security;
using System.Net.Sockets;
using System.Windows;

namespace HMonPat
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, INotifyPropertyChanged
    {
        private bool _isloggedin;

        public bool IsLoggedIn
        {
            get { return _isloggedin; }
            set
            {
                _isloggedin = value;
                OnPropertyChanged("IsLoggedIn");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
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
