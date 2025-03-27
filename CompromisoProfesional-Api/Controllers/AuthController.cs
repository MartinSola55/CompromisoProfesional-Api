using Microsoft.AspNetCore.Mvc;
using CompromisoProfesional_Api.Models.DAO.Auth;
using CompromisoProfesional_Api.Models.DAO;
using CompromisoProfesional_Api.Services;

namespace CompromisoProfesional_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        private readonly AuthService _authService = authService;

        [HttpPost]
        public async Task<GenericResponse<LoginResponse>> Login([FromBody] LoginRequest rq)
        {
            return await _authService.Login(rq);
        }


        [HttpPost]
        public GenericResponse Logout()
        {
            return _authService.Logout();
        }
    }
}
