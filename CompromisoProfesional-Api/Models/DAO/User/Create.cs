namespace CompromisoProfesional_Api.Models.DAO.User
{
    public class CreateRequest
    {
        public string? Password { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

    public class CreateResponse
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
