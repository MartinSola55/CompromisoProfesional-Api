using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompromisoProfesional_Api.Models
{
    public class Evolution : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int PatientId { get; set; }
        public string Descripcion { get; set; } = null!;
        public int? Hours { get; set; }


        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; } = null!;

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } = null!;
    }
}
