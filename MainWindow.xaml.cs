using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.Json;

namespace DataBindingWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        //List<Doctor> users = new List<Doctor>();
        public Doctor doctor { get; set; } = new Doctor();

        //List<Patient> patients = new List<Patient>( );
        public Patient patient { get; set; } = new Patient();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            countOfFiles( );
        }

        private void Add_User(object sender, RoutedEventArgs e)
        {
            if (PasswordLine.Text == ConfirmPassword.Text)
            {
                doctor.Id = doctor.GenerateId( );
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

                string json = JsonSerializer.Serialize(doctor);
                StreamWriter sr = File.CreateText($"{doctor.Id}.JSON");
                sr.Write(json);
                sr.Close( );
                MessageBox.Show($"Пользователь {doctor.Id} добавлен");
                updateCurrantUser( );
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
            }

            countOfFiles( );
        }

        private void countOfFiles ()
        {
            string [ ] allFiles = Directory.GetFiles(Directory.GetCurrentDirectory( ));

            UsersCount.Content = allFiles.Count(f => System.IO.Path.GetFileName(f).StartsWith("D_"));
            PatientsCount.Content = allFiles.Count(f => System.IO.Path.GetFileName(f).StartsWith("P_"));
        }

        private async void Enter_User (object sender, RoutedEventArgs e)
        {
            if (File.Exists($"{FindId.Text}.json"))
            {
                string path= $"{FindId.Text}.json";
                using(FileStream sr=new FileStream  (path, FileMode.OpenOrCreate))
                {
                    Doctor? restoredDoctor = await JsonSerializer.DeserializeAsync<Doctor>(sr);
                    if (restoredDoctor?.Password == FindPassword.Text)
                    {
                        doctor.Id = restoredDoctor.Id;
                        doctor.Name = restoredDoctor.Name;
                        doctor.LastName = restoredDoctor.LastName;
                        doctor.MiddleName = restoredDoctor.MiddleName;
                        doctor.Specialisation = restoredDoctor.Specialisation;
                        doctor.Password = restoredDoctor.Password;

                        MessageBox.Show("Успешный вход");
                        updateCurrantUser( );
                    }
                    else
                    {
                        MessageBox.Show("Пароль неверный");
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Пользователя с указанным идентификатором не найдено");
            }
        }

        public void updateCurrantUser ()
        {
            CurrentId.Content = doctor.Id;
            CurrentName.Content=doctor.Name;
            CurrentLastName.Content = doctor.LastName;
            CurrentMiddleName.Content = doctor.MiddleName;
            CurrentSpecialisation.Content = doctor.Specialisation;
           
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
                
           

            countOfFiles( );
        }

        private async void Appointment (object sender, RoutedEventArgs e)
        {
            if (File.Exists($"{_patientID.Text}.json"))
            {
                string path = $"{_patientID.Text}.json";
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    Patient? restoredPatient = await JsonSerializer.DeserializeAsync<Patient>(fs);

                    patient.Id = restoredPatient.Id;
                    patient.Name = restoredPatient.Name;
                    patient.LastName = restoredPatient.LastName;
                    patient.MiddleName = restoredPatient.MiddleName;
                    patient.Birthday = restoredPatient.Birthday;
                }

                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

                string json = JsonSerializer.Serialize(patient);
                StreamWriter sr = File.CreateText($"{patient.Id}.JSON");
                sr.Write(json);
                sr.Close( );
                MessageBox.Show($"Прием пациента {patient.Id} зафиксирован");
                updateCurrantUser( );
            }
            else
            {
                MessageBox.Show("Пациента с указанным идентификатором не найдено");
            }
        }

        private async void Search_Patient (object sender, RoutedEventArgs e)
        {
            if (File.Exists($"{findId.Text}.json"))
            {
                patient.Id = findId.Text;
                string path = $"{findId.Text}.json";
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    Patient? restoredPatient = await JsonSerializer.DeserializeAsync<Patient>(fs);

                    patient.Name = restoredPatient.Name;
                    patient.LastName = restoredPatient.LastName;
                    patient.MiddleName = restoredPatient.MiddleName;
                    patient.Birthday = restoredPatient.Birthday;
                    MessageBox.Show($"Фамилия: {patient.Name}\n Имя: {patient.LastName}\n Отчество: {patient.MiddleName} \nДата рождения: {patient.Birthday}");
                }

            }
            else
            {
                MessageBox.Show("Пациента с указанным идентификатором не найдено");
            }
        }

        private async void Change_Patient (object sender, RoutedEventArgs e)
        {
            
                string path = $"{patient.Id}.json";
            if (!File.Exists(path)) MessageBox.Show("Файл не нашел");
            else
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    Patient? restoredPatient = await JsonSerializer.DeserializeAsync<Patient>(fs);

                    patient.LastAppointment = restoredPatient.LastAppointment;
                    patient.LastDoctor = restoredPatient.LastDoctor;
                    patient.Diagnosis = restoredPatient.Diagnosis;
                    patient.Recomendations = restoredPatient.Recomendations;

                }
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

                string json = JsonSerializer.Serialize(patient);
                StreamWriter sr = File.CreateText($"{patient.Id}.JSON");
                sr.Write(json);
                sr.Close( );
                MessageBox.Show($"Изменения пациента {patient.Id} зафиксированы");

            }
            
            

        }
       
    }
}