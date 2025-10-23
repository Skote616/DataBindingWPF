using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
=======
using System.IO;
using System.Linq;
>>>>>>> 68bfe7522069bb4544432cb3b01b2ae64a25edc3
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
<<<<<<< HEAD
        private Appointment _appointment = new Appointment();
        private Patient _patient;
        private ObservableCollection<Appointment> _appointmentStories = new ObservableCollection<Appointment>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public Appointment Appointment
        {
            get => _appointment;
            set
            {
                _appointment = value;
                OnPropertyChanged(nameof(Appointment));
            }
        }

        public Patient Patient
        {
            get => _patient;
            set
            {
                _patient = value;
                OnPropertyChanged(nameof(Patient));
            }
        }

        public ObservableCollection<Appointment> AppointmentStories
        {
            get => _appointmentStories;
            set
            {
                _appointmentStories = value;
                OnPropertyChanged(nameof(AppointmentStories));
            }
        }

        public AppointmentPage(Patient pat)
        {
            InitializeComponent();
            Patient = pat;
            AppointmentStories.Clear();
            foreach (Appointment ap in pat.AppointmentStories)
            {
                AppointmentStories.Add(ap);
            }
            DataContext = this;
        }

        

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private async void addAppointment(object sender, RoutedEventArgs e)
        {

            if (File.Exists($"{Patient.Id}.json"))
            {
                string path = $"{Patient.Id}.json";
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    Patient? restoredPatient = await JsonSerializer.DeserializeAsync<Patient>(fs);

                    Patient.Id = restoredPatient.Id;
                    Patient.Name = restoredPatient.Name;
                    Patient.LastName = restoredPatient.LastName;
                    Patient.MiddleName = restoredPatient.MiddleName;
                    Patient.Birthday = restoredPatient.Birthday;
                    Patient.AppointmentStories = restoredPatient.AppointmentStories;
                }
                if (File.Exists($"D_{Appointment.Doctor_id}.json"))
                {
                    string docpath = $"D_{Appointment.Doctor_id}.json";
                    using (FileStream fs = new FileStream(docpath, FileMode.OpenOrCreate))
                    {
                        Patient? restoredDoctor = await JsonSerializer.DeserializeAsync<Patient>(fs);

                        string name = restoredDoctor.Name;
                        string lastname = restoredDoctor.LastName;
                        string middlename = restoredDoctor.MiddleName;
                        Appointment.Doctor_Fio = $"{name} {lastname} {middlename}";
                    }

                }
                else
                {
                    MessageBox.Show("Такого врача не существует");
                }
                Patient.AppointmentStories.Add(Appointment);

                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

                string json = JsonSerializer.Serialize(Patient);
                StreamWriter sr = File.CreateText($"{Patient.Id}.JSON");
                sr.Write(json);
                sr.Close();
                MessageBox.Show($"Прием пациента {Patient.Id} зафиксирован");
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Пациента с указанным идентификатором не найдено");
            }
        }

                private void Back(object sender, RoutedEventArgs e)
                {
                    NavigationService.GoBack();
                }

    
=======
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
>>>>>>> 68bfe7522069bb4544432cb3b01b2ae64a25edc3
    }
}
