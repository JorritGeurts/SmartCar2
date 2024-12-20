﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCar.Models
{
    public class DamageEntry : ObservableObject
    {
        private int id;
        public int Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
                OnPropertyChanged(nameof(id));
            }
        }

        private string tag;
        public string Tag
        {
            get => tag;
            set
            {
                SetProperty(ref tag, value);
                OnPropertyChanged(nameof(tag));
            }
        }


        private string damageType;
        public string DamageType
        {
            get => damageType;
            set
            {
                SetProperty(ref damageType, value);
                OnPropertyChanged(nameof(IsDamageTypeSelected));
            }
        }

        private string severity;
        public string Severity
        {
            get => severity;
            set => SetProperty(ref severity, value);
        }

        private DamageTypes selectedDamageType;
        public DamageTypes SelectedDamageType
        {
            get => selectedDamageType;
            set
            {
                SetProperty(ref selectedDamageType, value);
                OnPropertyChanged(nameof(IsDamageTypeSelected));
            }
        }


        private Severities selectedSeverity;
        public Severities SelectedSeverity
        {
            get => selectedSeverity;
            set => SetProperty(ref selectedSeverity, value);
        }

        public bool IsDamageTypeSelected => SelectedDamageType != null;
    }
}
