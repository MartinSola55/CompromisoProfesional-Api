using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompromisoProfesional_Api.Models
{
    public class PatientAppliance : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ApplianceId { get; set; }
        public int Quantity { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime? ReturnDate { get; set; }


        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } = null!;

        [ForeignKey("ApplianceId")]
        public Appliance Appliance { get; set; } = null!;
    }
}
