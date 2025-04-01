namespace CompromisoProfesional_Api.Models.DTO.Profession
{
    public class GetAllRequest : PaginateRequest
    {

    }

    public class GetAllResponse
    {
        public List<Item> Professions { get; set; } = [];

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Type { get; set; } = null!;
            public DateTime CreatedAt { get; set; }
        }
    }
}