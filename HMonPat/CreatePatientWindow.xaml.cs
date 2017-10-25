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
    /// Interaction logic for CreatePatientWindow.xaml
    /// </summary>
    public partial class CreatePatientWindow : Window
    {
        public bool isFemale { get; set; }
        public string name { get; set; }
        public string age { get; set; }
        public string weight { get; set; }
        public string id { get; set; }

        public CreatePatientWindow()
        {
            InitializeComponent();
        }

        private void CreatePatient_Click(object sender, RoutedEventArgs e)
        {
            if (GenderSelect.SelectedItem == null) { ErrorMessage.Text = "Select your gender.";  return; }
            else if (GenderSelect.SelectedItem == f) { isFemale = true; ErrorMessage.Text = ""; }
            else if (GenderSelect.SelectedItem == m) { isFemale = false; ErrorMessage.Text = ""; }

            if (PatientName.Text == "" || PatientAge.Text == "" || PatientWeight.Text == "" || PatientID.Text == "") { ErrorMessage.Text = "Fill in all details."; return; }
            else {
                name = PatientName.Text;
                age = PatientAge.Text;
                weight = PatientWeight.Text;
                id = PatientID.Text;
            }
        }
    }
}
