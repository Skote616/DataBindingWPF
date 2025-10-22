﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingWPF
{
    public class Appointment :INotifyPropertyChanged
    {
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged( );
                }
            }
        }
        private string _doctor_id = "Идентификатор врача";
        public string Doctor_id
        {
            get => _doctor_id;
            set
            {
                if (_doctor_id != value)
                {
                    _doctor_id = value;
                    OnPropertyChanged( );
                }
            }
        }
        private string _diagnosis = "Диагноз";
        public string Diagnosis
        {
            get => _diagnosis;
            set
            {
                if (_diagnosis != value)
                {
                    _diagnosis = value;
                    OnPropertyChanged( );
                }
            }
        }
        private string _recomendations = "Рекомендации";
        public string Recomendations
        {
            get => _recomendations;
            set
            {
                if (_recomendations != value)
                {
                    _recomendations = value;
                    OnPropertyChanged( );
                }
            }
        }
       
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged ([CallerMemberName] string? propName =
        null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
