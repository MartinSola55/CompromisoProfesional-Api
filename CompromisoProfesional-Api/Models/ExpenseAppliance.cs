using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompromisoProfesional_Api.Models
{
    public class ExpenseAppliance : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public int ApplianceId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        [ForeignKey("ExpenseId")]
        public Expense Expense { get; set; } = null!;

        [ForeignKey("ApplianceId")]
        public Appliance Appliance { get; set; } = null!;
    }
}
