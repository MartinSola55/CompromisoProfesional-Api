using System.ComponentModel.DataAnnotations;

namespace CompromisoProfesional_Api.Models
{
    public class Profession : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;


        public List<Employee> Employees { get; set; } = [];

        public List<SuggestedPrice> SuggestedPrices { get; set; } = [];
    }
}
