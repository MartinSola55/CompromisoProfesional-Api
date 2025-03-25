using System.ComponentModel.DataAnnotations;

namespace CompromisoProfesional_Api.Models
{
    public class Provider : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public string ProviderType { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string CUIT { get; set; } = null!;
        public decimal Debt { get; set; } = 0;


        public List<Expense> Expenses { get; set; } = [];
    }
}
