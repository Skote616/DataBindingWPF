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
    /// Логика взаимодействия для Appointment.xaml
    /// </summary>
    public partial class AppointmentPage : Page
    {
        Appointment appointment = new Appointment();
        Patient patient;
        public AppointmentPage(Patient pat)
        {
            patient = pat;
            DataContext = appointment;
            InitializeComponent();
        }

       
        private async void addAppointment(object sender, RoutedEventArgs e)
        {
           
                if (File.Exists($"D_{patient.Id}.json"))
                {
                    string path = $"D_{patient.Id}.json";
                    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        Patient? restoredPatient = await JsonSerializer.DeserializeAsync<Patient>(fs);

                        patient.Id = restoredPatient.Id;
                        patient.Name = restoredPatient.Name;
                        patient.LastName = restoredPatient.LastName;
                        patient.MiddleName = restoredPatient.MiddleName;
                        patient.Birthday = restoredPatient.Birthday;
                        patient.AppointmentStories = restoredPatient.AppointmentStories;
                    }

                    patient.AppointmentStories.Add(appointment);    

                    JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

                    string json = JsonSerializer.Serialize(patient);
                    StreamWriter sr = File.CreateText($"{patient.Id}.JSON");
                    sr.Write(json);
                    sr.Close();
                    MessageBox.Show($"Прием пациента {patient.Id} зафиксирован");
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Пациента с указанным идентификатором не найдено");
                }
           
        }
    }
}
