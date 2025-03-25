namespace CompromisoProfesional_Api.Services
{
    public class ServiceContainer
    {
        public static void AddServices(IServiceCollection services)
        {
            // General
            services.AddScoped<TokenService>();
            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();
        }
    }
}
