using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HMonPat
{
    /// <summary>
    /// Interaction logic for PatientLoginWindow.xaml
    /// </summary>
    public partial class PatientLoginWindow : Window
    {
        public bool OK { get; set; }
        public string PatientId { get; set; }

        public PatientLoginWindow()
        {
            InitializeComponent();
            this.OK = false;
        }

        private void CancelClickEvent(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoginClickEvent(object sender, RoutedEventArgs e)
        {
            this.OK = true;
            this.PatientId = this.PatientIdBox.Text;
            this.Close();
        }
    }
}
