using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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
    /// Логика взаимодействия для EnterPage.xaml
    /// </summary>
    public partial class EnterPage : Page
    {
       
        Doctor doctor = new Doctor();
        public EnterPage()
        {
            DataContext = doctor;
            InitializeComponent();
        }

        private async void Enter_User(object sender, RoutedEventArgs e)
        {
            if (File.Exists($"D_{FindId.Text}.json"))
            {
                string path = $"D_{FindId.Text}.json";
                using (FileStream sr = new FileStream(path, FileMode.OpenOrCreate))
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
                        NavigationService.Navigate(new MainPage(doctor));
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

        private void toRegistration(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
