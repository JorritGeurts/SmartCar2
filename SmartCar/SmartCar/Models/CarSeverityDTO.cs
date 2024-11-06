using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace SmartCar.Models
{
    public class CarSeverityDTO
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int SeverityId { get; set; }
        public int DamageId { get; set; }
    }
}
