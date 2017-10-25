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

namespace HMonDoc
{
    /// <summary>
    /// Interaction logic for OpenSessionWindow.xaml
    /// </summary>
    public partial class OpenSessionWindow : Window
    {
        public bool OkClicked { get; set; }
        public string SessionNumber { get; set; }
        public string PatientNumber { get; set; }
        public OpenSessionWindow()
        {
            InitializeComponent();
        }

        private void OkClickEvent(object sender, EventArgs args)
        {
            this.OkClicked = true;
            this.SessionNumber = this.SessionNumberBox.Text;
            this.PatientNumber = this.PatientNumberBox.Text;
            this.Close();
        }

        private void CancelClickEvent(object sender, EventArgs args)
        {
            this.OkClicked = false;
            this.Close();
        }
    }
}
