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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HMonPatient.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            HamburberBox.SelectedItem = Home;
            ContentFrame.Navigate(typeof(HomePage), this);
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            Split.IsPaneOpen = !Split.IsPaneOpen;
        }

        private void HamburgerBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Home.IsSelected) ContentFrame.Navigate(typeof(HomePage), this);
            else if (Session.IsSelected) ContentFrame.Navigate(typeof(SessionPage), this);
            else if (TestPage.IsSelected) ContentFrame.Navigate(typeof(TestPage), this);
        }
    }
}
