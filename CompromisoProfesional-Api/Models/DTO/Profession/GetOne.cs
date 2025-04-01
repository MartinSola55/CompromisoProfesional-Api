
namespace CompromisoProfesional_Api.Models.DTO.Profession
{
    public class GetOneRequest
    {
        public int Id { get; set; }
    }

    public class GetOneResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public List<Item> SuggestedPrices { get; set; } = [];

        public class Item
        {
            public int Id { get; set; }
            public string SocialSecurityName { get; set; } = null!;
            public decimal Price { get; set; }
            public DateTime UpdatedAt { get; set; }
        }
    }
}
