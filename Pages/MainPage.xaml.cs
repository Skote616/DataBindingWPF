using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public ObservableCollection<Patient> _patients = new( );
        public Doctor currentDoctor = new( );

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Patient> Patients
        {
            get => _patients;
            set
            {
                _patients = value;
                OnPropertyChanged( );
            }
        }

        public Doctor CurrentDoctor
        {
            get => currentDoctor;
            set
            {
                currentDoctor = value;
                OnPropertyChanged( );
            }
        }

        public MainPage (Doctor doctor)
        {
            CurrentDoctor = doctor;
            InitializeComponent( );
            DataContext = this;
            LoadAllPatients( );
        }

        protected virtual void OnPropertyChanged ([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void toAddPatient (object sender, RoutedEventArgs e)
        {
           NavigationService.Navigate(new AddPatientPage( ));

            if (List.SelectedItem != null)
            {
                NavigationService.Navigate(new AddPatientPage( ));
            }
            else
            { MessageBox.Show("Пациент для изменения не выбран"); }

        }

        private void toAppointment(object sender, RoutedEventArgs e)
        {
            if (List.SelectedItem != null)
            {
                NavigationService.Navigate(new AppointmentPage((Patient) List.SelectedItem));
            }
            else
            { MessageBox.Show("Пациент для приема не выбран"); }
        }
        private void toChangeInfo(object sender, RoutedEventArgs e)
        {
            if (List.SelectedItem != null)
            {
                NavigationService.Navigate(new ChangeInfoPage((Patient) List.SelectedItem));
            }
            else
            { MessageBox.Show("Пациент для изменения не выбран"); }
            NavigationService.Navigate(new ChangeInfoPage((Patient) List.SelectedItem));

        }

        public async void LoadAllPatients()
        {

            string [ ] patientFiles = Directory.GetFiles(Directory.GetCurrentDirectory( ), "P_*.json");

            foreach (string filePath in patientFiles)
            {
                
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        Patient? restoredPatient = await JsonSerializer.DeserializeAsync<Patient>(fs);
                        if (restoredPatient != null)
                        {
                            Patients.Add(restoredPatient);
                        }
                    }
                
               
            }

            MessageBox.Show($"Загружено пациентов: {Patients.Count}");
            
        }
    }
}
