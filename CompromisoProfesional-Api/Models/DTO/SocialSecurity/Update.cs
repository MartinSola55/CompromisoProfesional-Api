namespace CompromisoProfesional_Api.Models.DTO.SocialSecurity
{
    public class UpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string CUIT { get; set; } = null!;
    }
}