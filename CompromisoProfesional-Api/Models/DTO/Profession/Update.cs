namespace CompromisoProfesional_Api.Models.DTO.Profession
{
    public class UpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
    }
}