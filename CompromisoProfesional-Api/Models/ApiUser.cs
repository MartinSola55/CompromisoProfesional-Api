using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompromisoProfesional_Api.Models
{
    public class ApiUser : IdentityUser
    {
        public string RoleId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }


        [ForeignKey("RoleId")]
        public virtual IdentityRole Role { get; set; } = null!;
    }
}