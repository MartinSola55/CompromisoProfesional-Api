using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompromisoProfesional_Api.Models
{
    public class Patient : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string DNI { get; set; } = null!;
        public int SocialSecurityId { get; set; }
        public string MedicalHistoryKey { get; set; } = null!;
        public DateTime CompanyEntryDate { get; set; }
        public DateTime? CompanyExitDate { get; set; }
        public DateTime LastRenovationDate { get; set; }
        public int? RenovationPeriod { get; set; }


        [ForeignKey("SocialSecurityId")]
        public SocialSecurity SocialSecurity { get; set; } = null!;

        public List<Evolution> Evolutions { get; set; } = [];

        public List<PatientAppliance> PatientAppliances { get; set; } = [];

        public List<PatientSupply> PatientSupplies { get; set; } = [];
    }
}