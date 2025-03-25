using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompromisoProfesional_Api.Models
{
    public class Expense : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public decimal Amount { get; set; }
        public string InvoiceKey { get; set; } = null!;


        [ForeignKey("ProviderId")]
        public Provider Provider { get; set; } = null!;
        public List<ExpenseSupply> ExpenseSupplies { get; set; } = [];
        public List<ExpenseAppliance> ExpenseAppliances { get; set; } = [];
    }
}
