namespace CompromisoProfesional_Api.Models.DAO.User
{
    public class UpdateRequest : CreateRequest
    {
        public string Id { get; set; } = null!;
    }

    public class UpdateResponse
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
