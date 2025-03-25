using Microsoft.AspNetCore.Authorization;

namespace CompromisoProfesional_Api.Security
{
    public class AuthorizeRolesAttribute(params string[] roles) : IAuthorizationRequirement
    {
        public string Roles { get; } = string.Join(",", roles);
    }
}
