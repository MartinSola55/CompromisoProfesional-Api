
namespace CompromisoProfesional_Api.Models.DAO.User
{
    public class GetOneRequest
    {
        public int Id { get; set; }
    }

    public class GetOneResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
