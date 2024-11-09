using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SmartCar.Models
{
    public class CarSeverityDTO : ObservableObject
    {
        private int id;
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private int carId;
        public int CarId
        {
            get => carId;
            set => SetProperty(ref carId, value);
        }
        private int severityId;
        public int SeverityId
        {
            get => severityId;
            set => SetProperty(ref severityId, value);
        }

        private int damageId;

        public int DamageId
        {
            get => damageId;
            set => SetProperty(ref damageId, value);
        }

        private SmarterCarDTO smarterCar;
        public SmarterCarDTO SmarterCar
        {
            get => smarterCar;
            set => SetProperty(ref smarterCar, value);
        }

        private DamageTypes damageTypes;
        public DamageTypes DamageTypes
        {
            get => damageTypes;
            set => SetProperty(ref damageTypes, value);
        }

        private Severities severities;
        public Severities Severities
        {
            get => severities;
            set => SetProperty(ref severities, value);
        }

    }
}
