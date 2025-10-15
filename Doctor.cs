using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingWPF
{
    public class Doctor : INotifyPropertyChanged
    {
        private string _id = "Идентификатор пользователя";
        public string Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _name = "Фамилия пользователя";
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _lastname = "Имя пользователя";
        public string LastName
        {
            get => _lastname;
            set
            {
                if (_lastname != value)
                {
                    _lastname = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _middlename = "Отчество пользователя";
        public string MiddleName
        {
            get => _middlename;
            set
            {
                if (_middlename != value)
                {
                    _middlename = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _specialisation = "Специальность пользователя";
        public string Specialisation
        {
            get => _specialisation;
            set
            {
                if (_specialisation != value)
                {
                    _specialisation = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _password = "Пароль";
        public string Password
        {
            get => _password;
            set
            {
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propName =
        null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public string GenerateId()
        {
            Random rnd = new Random();
            string id = "D_";

            while (id.Length < 7)
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
