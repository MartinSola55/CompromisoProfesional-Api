using System.ComponentModel.DataAnnotations;

namespace CompromisoProfesional_Api.Models
{
    public class SocialSecurity : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string CUIT { get; set; } = null!;


        public List<Patient> Patients { get; set; } = [];

        public List<SuggestedPrice> SuggestedPrices { get; set; } = [];
    }
}
