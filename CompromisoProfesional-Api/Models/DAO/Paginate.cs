using CompromisoProfesional_Api.Models.Constants;

namespace CompromisoProfesional_Api.Models.DAO
{
    public class PaginateRequest
    {
        public int Page { get; set; } = 1;
        public string ColumnSort { get; set; } = "createdAt";
        public string SortDirection { get; set; } = SortDirectionCode.DESC;
    }

    public class PaginateResponse
    {
        public int TotalCount { get; set; } = 0;
    }
}
