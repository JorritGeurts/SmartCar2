using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SmartCar.Models
{
    public class Severities : ObservableObject
    {
        private int id;
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        public string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public double amount;
        public double Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }
    }
}
