using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompromisoProfesional_Api.Models
{
    public class ExpenseSupply : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public int SupplyId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        [ForeignKey("ExpenseId")]
        public Expense Expense { get; set; } = null!;

        [ForeignKey("SupplyId")]
        public Supply Supply { get; set; } = null!;
    }
}
