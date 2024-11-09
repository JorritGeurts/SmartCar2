using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCar.Models
{
    public class Severities : ObservableObject
    {
        private int id;
        private string name;
        private double amount;
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public double Amount { get => amount; set => SetProperty(ref amount, value); }
    }
}
