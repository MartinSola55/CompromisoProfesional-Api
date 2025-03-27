namespace CompromisoProfesional_Api.Models.DAO.User
{
    public class UpdateRequest : CreateRequest
    {
        public int Id { get; set; }
    }

    public class UpdateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
