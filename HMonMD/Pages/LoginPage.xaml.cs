using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HMonMD.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public static LoginPage LoginPageInstance;
        public Client Client;

        public LoginPage()
        {
            this.InitializeComponent();
            LoginPage.LoginPageInstance = this;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (connection()){
                this.Frame.Navigate(typeof(MainPage), this);
            } else ErrorMessage.Text = "Can't establish connection";
        }

        private bool connection()
        {
            Client = new Client();
            try {
                Client.Connect(Hostname.Text, Int32.Parse(Portnumber.Text));
            } catch(Exception e) {
                return false;
            }
            return true;
        }
    }
}
