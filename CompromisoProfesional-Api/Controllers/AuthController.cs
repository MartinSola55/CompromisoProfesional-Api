using Microsoft.AspNetCore.Mvc;
using CompromisoProfesional_Api.Models.DTO.Auth;
using CompromisoProfesional_Api.Models.DTO;
using CompromisoProfesional_Api.Services;

namespace CompromisoProfesional_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        private readonly AuthService _authService = authService;

        [HttpPost]
        public async Task<BaseResponse<LoginResponse>> Login([FromBody] LoginRequest rq)
        {
            return await _authService.Login(rq);
        }


        [HttpPost]
        public BaseResponse Logout()
        {
            return _authService.Logout();
        }
    }
}
