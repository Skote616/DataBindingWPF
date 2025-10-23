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
using System.Xml.Linq;

namespace DataBindingWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChangeInfoPage.xaml
    /// </summary>
    public partial class ChangeInfoPage :Page
    {
        Patient tpatient;
        public ChangeInfoPage (Patient patient)
        {
            tpatient = patient;
            DataContext = patient;
            InitializeComponent( );
        }


        string name, lastname, middlename;
        DateTime birthday;

        private async void Change_Patient (object sender, RoutedEventArgs e)
        {

            string path = $"{tpatient.Id}.json";
            if (!File.Exists(path)) MessageBox.Show("Файл не нашел");
            else
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    Patient? restoredPatient = await JsonSerializer.DeserializeAsync<Patient>(fs);

                    name = restoredPatient.Name;
                    lastname = restoredPatient.LastName;
                    middlename = restoredPatient.MiddleName;
                    birthday = restoredPatient.Birthday;
                    /* patient.LastAppointment = restoredPatient.LastAppointment;
                     patient.LastDoctor = restoredPatient.LastDoctor;
                     patient.Diagnosis = restoredPatient.Diagnosis;
                     patient.Recomendations = restoredPatient.Recomendations;*/

                }
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

                string json = JsonSerializer.Serialize(tpatient);
                StreamWriter sr = File.CreateText($"{tpatient.Id}.JSON");
                sr.Write(json);
                sr.Close( );
                MessageBox.Show($"Изменения пациента {tpatient.Id} зафиксированы");
                //NavigationService.Navigate(new MainPage());
            }
        }



        private async void Return (object sender, RoutedEventArgs e)
        {
            if (name != null)
            {
                string path = $"{tpatient.Id}.json";
                if (!File.Exists(path)) MessageBox.Show("Файл не нашел");
                else
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        Patient? restoredPatient = await JsonSerializer.DeserializeAsync<Patient>(fs);

                        tpatient.Name = name;
                        tpatient.LastName = lastname;
                        tpatient.MiddleName = middlename;
                        tpatient.Birthday = birthday;
                        /* patient.LastAppointment = restoredPatient.LastAppointment;
                         patient.LastDoctor = restoredPatient.LastDoctor;
                         patient.Diagnosis = restoredPatient.Diagnosis;
                         patient.Recomendations = restoredPatient.Recomendations;*/

                    }
                    JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

                    string json = JsonSerializer.Serialize(tpatient);
                    StreamWriter sr = File.CreateText($"{tpatient.Id}.JSON");
                    sr.Write(json);
                    sr.Close( );
                    MessageBox.Show($"Изменения пациента {tpatient.Id} возвращены");

                }
            }
            else { MessageBox.Show("Изменений не было, чтобы их отменять"); }

        }

        private void GoBack (object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
