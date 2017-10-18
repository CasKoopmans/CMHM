using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media;

namespace HMonDoc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TcpClient client;
        private SslStream stream;

        public MainWindow(TcpClient client, SslStream stream)
        {
            this.client = client;
            this.stream = stream;
            InitializeComponent();
            this.DataContext = this;
        }

        private void CBHeartrate_Unchecked(object sender, RoutedEventArgs e)
        {  }
        private void CBHeartrate_Checked(object sender, RoutedEventArgs e)
        {  }

        private void CBRPM_Unchecked(object sender, RoutedEventArgs e)
        { }
        private void CBRPM_Checked(object sender,RoutedEventArgs e)
        {  }

        private void CBSpeed_Unchecked(object sender, RoutedEventArgs e)
        { }
        private void CBSpeed_Checked(object sender, RoutedEventArgs e)
        { }

        private void CBDistance_Unchecked(object sender, RoutedEventArgs e)
        {  }
        private void CBDistance_Checked(object sender, RoutedEventArgs e)
        {  }

        private void CBResistance_Unchecked(object sender, RoutedEventArgs e)
        {  }
        private void CBResistance_Checked(object sender, RoutedEventArgs e)
        {  }

        private void CBEnergyKJ_Unchecked(object sender, RoutedEventArgs e)
        {  }
        private void CBEnergyKJ_Checked(object sender, RoutedEventArgs e)
        {  }

        private void CBEnergyW_Unchecked(object sender, RoutedEventArgs e)
        { }
        private void CBEnergyW_Checked(object sender, RoutedEventArgs e)
        {  }
    }
}
