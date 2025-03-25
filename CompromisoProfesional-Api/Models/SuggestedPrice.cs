using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompromisoProfesional_Api.Models
{
    public class SuggestedPrice : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public int ProfessionId { get; set; }
        public int SocialSecurityId { get; set; }
        public decimal Price { get; set; }


        [ForeignKey("ProfessionId")]
        public Profession Profession { get; set; } = null!;

        [ForeignKey("SocialSecurityId")]
        public SocialSecurity SocialSecurity { get; set; } = null!;
    }
}
