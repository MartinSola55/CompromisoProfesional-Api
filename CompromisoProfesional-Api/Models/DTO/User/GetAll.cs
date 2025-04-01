namespace CompromisoProfesional_Api.Models.DTO.User
{
    public class GetAllRequest : PaginateRequest
    {
        public List<string> Roles { get; set; } = [];
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

    public class GetAllResponse
    {
        public List<Item> Users { get; set; } = [];

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Role { get; set; } = null!;
            public DateTime CreatedAt { get; set; }
        }
    }
}