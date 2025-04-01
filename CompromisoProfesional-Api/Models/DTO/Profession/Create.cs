namespace CompromisoProfesional_Api.Models.DTO.Profession
{
    public class CreateRequest
    {
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
    }

    public class CreateResponse
    {
        public int Id { get; set; }
    }
}