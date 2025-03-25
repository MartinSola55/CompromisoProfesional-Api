using System.ComponentModel.DataAnnotations;

namespace CompromisoProfesional_Api.Models
{
    public class Supply : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Stock { get; set; } = 0;


        public List<ExpenseSupply> ExpenseSupplies { get; set; } = [];

        public List<PatientSupply> PatientSupplies { get; set; } = [];
    }
}
