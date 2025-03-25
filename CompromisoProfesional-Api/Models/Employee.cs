using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompromisoProfesional_Api.Models
{
    public class Employee : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public int ProfessionId { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string LicenseNumber { get; set; } = null!;
        public string CUIT { get; set; } = null!;
        public string CBU { get; set; } = null!;
        public string TaxCondition { get; set; } = null!;
        public decimal? PricePerHour { get; set; }


        [ForeignKey("ProfessionId")]
        public Profession Profession { get; set; } = null!;

        public List<Evolution> Evolutions { get; set; } = [];
    }
}