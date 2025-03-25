namespace CompromisoProfesional_Api.Models
{
    public class Token
    {
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
