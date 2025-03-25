using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompromisoProfesional_Api.Models
{
    public class PatientSupply : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int SupplyId { get; set; }
        public int Quantity { get; set; }
        public DateTime DeliveryDate { get; set; }


        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } = null!;

        [ForeignKey("SupplyId")]
        public Supply Supply { get; set; } = null!;
    }
}
