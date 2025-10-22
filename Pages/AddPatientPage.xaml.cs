using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace DataBindingWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPatientPage.xaml
    /// </summary>
    public partial class AddPatientPage : Page
    {
        Patient patient=new Patient();
        public AddPatientPage()
        {
            DataContext = patient;
            InitializeComponent();
        }

        private void Add_Patient (object sender, RoutedEventArgs e)
        { 
                patient.Id = patient.GenerateIdPatient( );
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(patient);
                StreamWriter sr = File.CreateText($"{patient.Id}.JSON");
                sr.Write(json);
                sr.Close( );
                MessageBox.Show($"Пациент {patient.Id} добавлен");
                NavigationService.GoBack( );
        }

        


    }
}
