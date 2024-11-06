using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCarAPI.Models
{
    public class CarSeverity
    {
        public int Id { get; set; }

        // Foreign key for Car
        public int CarId { get; set; } // Just the ID

        // Foreign key for Severity
        public int SeverityId { get; set; } // Just the ID

        // Foreign key for Damage
        public int DamageId { get; set; } // Just the ID
    }

}
