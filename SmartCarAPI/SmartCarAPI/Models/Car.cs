using Microsoft.EntityFrameworkCore;

namespace SmartCarAPI.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public int OldPrice { get; set; }
        public int NewPrice { get; set; }
        public int KmAmount { get; set; }
        public int YearBought { get; set; }
        public string Photo { get; set; }

    }
}
