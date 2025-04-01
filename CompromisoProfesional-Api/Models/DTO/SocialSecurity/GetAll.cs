namespace CompromisoProfesional_Api.Models.DTO.SocialSecurity
{
    public class GetAllRequest : PaginateRequest
    {

    }

    public class GetAllResponse
    {
        public List<Item> SocualSecurities { get; set; } = [];

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string CUIT { get; set; } = null!;
            public DateTime CreatedAt { get; set; }
            public List<PriceItem> SuggestedPrices { get; set; } = [];

            public class PriceItem
            {
                public decimal Price { get; set; }
                public DateTime LastUpdatedAt { get; set; }
            }
        }
    }
}