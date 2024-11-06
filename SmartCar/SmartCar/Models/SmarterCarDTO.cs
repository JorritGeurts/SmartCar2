using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SmartCar.Models
{
    public class SmarterCarDTO : ObservableObject
    {
        private string tag;
        public string Tag
        {
            get => tag;
            set => SetProperty(ref tag, value);
        }

        private double oldPrice;
        public double OldPrice
        {
            get => oldPrice;
            set => SetProperty(ref oldPrice, value);
        }

        private double newPrice;
        public double NewPrice
        {
            get => newPrice;
            set => SetProperty(ref newPrice, value);
        }

        private int kmAmount;
        public int KmAmount
        {
            get => kmAmount;
            set => SetProperty(ref kmAmount, value);
        }

        private int yearBought;
        public int YearBought
        {
            get => yearBought;
            set => SetProperty(ref yearBought, value);
        }

        private string photo;
        public string Photo
        {
            get => photo;
            set => SetProperty(ref photo, value);
        }
    }
}
