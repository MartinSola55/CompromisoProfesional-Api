namespace CompromisoProfesional_Api.Models.DTO.SocialSecurity
{
    public class CreateRequest
    {
        public string Name { get; set; } = null!;
        public string CUIT { get; set; } = null!;
    }

    public class CreateResponse
    {
        public int Id { get; set; }
    }
}