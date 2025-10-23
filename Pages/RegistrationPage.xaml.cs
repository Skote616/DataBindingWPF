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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        Doctor doctor = new Doctor();
        public RegistrationPage()
        {
            DataContext = doctor;
            InitializeComponent();
        }

        private void Add_User(object sender, RoutedEventArgs e)
        {
            if (PasswordLine.Text == ConfirmPassword.Text)
            {
                doctor.Id = doctor.GenerateId();
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

                string json = JsonSerializer.Serialize(doctor);
                StreamWriter sr = File.CreateText($"{doctor.Id}.JSON");
                sr.Write(json);
                sr.Close();
                MessageBox.Show($"Пользователь {doctor.Id} добавлен");
                NavigationService.Navigate(new MainPage(doctor));
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
            }

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
