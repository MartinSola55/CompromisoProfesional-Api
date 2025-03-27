using System.ComponentModel.DataAnnotations;

namespace CompromisoProfesional_Api.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;

    }
}
