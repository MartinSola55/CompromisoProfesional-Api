namespace CompromisoProfesional_Api.Models.DTO.User
{
    public class UpdatePasswordRequest
    {
        public int? Id { get; set; }
        public string Password { get; set; } = null!;
    }
}
