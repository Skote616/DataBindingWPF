using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataBindingWPF
{
    public class Patient:INotifyPropertyChanged
    {
        private string _id= "Идентификатор пациента";
        public string Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged( );
                }
            }
        }
        private string _name = "Фамилия пациента";
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged( );
                }
            }
        }
        private string _lastname = "Имя пациента";
        public string LastName
        {
            get => _lastname;
            set
            {
                if (_lastname != value)
                {
                    _lastname = value;
                    OnPropertyChanged( );
                }
            }
        }
        private string _middlename = "Отчество пациента";
        public string MiddleName
        {
            get => _middlename;
            set
            {
                if (_middlename != value)
                {
                    _middlename = value;
                    OnPropertyChanged( );
                }
            }
        }
        private DateTime _birthday;
        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    OnPropertyChanged( );
                }
            }
        }

        private string _phone = "Номер телефона";
        public string Phone
        {
            get => _phone;
            set
            {
                if (value != _phone)
                {
                    _phone = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Appointment> AppointmentStories { get; set; } = new( );

        

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged ([CallerMemberName] string? propName =
        null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public string GenerateIdPatient()
        {
            Random rnd = new Random();
            string id = "P_";

            while (id.Length < 9)
            {
                int digit = rnd.Next(0, 10);
                char digitChar = digit.ToString()[0];

                if (!id.Contains(digitChar))
                {
                    id += digitChar;
                }
            }

            return id;
        }
    }
}
