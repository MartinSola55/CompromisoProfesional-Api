using System.ComponentModel.DataAnnotations;

namespace CompromisoProfesional_Api.Models
{
    public class Appliance : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Stock { get; set; } = 0;
        public bool IsOwned { get; set; } = false;


        public List<ExpenseAppliance> ExpenseAppliances { get; set; } = [];

        public List<PatientAppliance> PatientAppliances { get; set; } = [];
    }
}
