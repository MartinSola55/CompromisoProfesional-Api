using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using CompromisoProfesional_Api.Models.Constants;
using CompromisoProfesional_Api.Models.DTO;
using CompromisoProfesional_Api.Models.DTO.Auth;
using CompromisoProfesional_Api.DAL.DB;

namespace CompromisoProfesional_Api.Services
{
    public class AuthService(TokenService tokenService, APIContext context)
    {
        private readonly TokenService _tokenService = tokenService;
        private readonly APIContext _db = context;

        public async Task<BaseResponse<LoginResponse>> Login(LoginRequest rq)
        {
            var response = new BaseResponse<LoginResponse>();

            if (string.IsNullOrEmpty(rq.Email) || string.IsNullOrEmpty(rq.Password))
                return response.SetError(Messages.Error.FieldsRequired(["Email", "Password"]));

            if (!ValidateEmail(rq.Email))
                return response.SetError(Messages.Error.InvalidEmail());

            if (!ValidatePassword(rq.Password))
                return response.SetError(Messages.Error.InvalidPassword());

            var user = await _db
                .User
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == rq.Email);

            if (user == null || !ValidateHashedPassword(rq.Password, user.PasswordHash))
                return response.SetError(Messages.Error.InvalidLogin());

            if (user.Role == null)
                return response.SetError(Messages.Error.UserWithoutRole());

            var expiration = DateTime.Now.AddDays(30);
            var token = _tokenService.GenerateToken(user, user.Role.Name, expiration);

            if (string.IsNullOrEmpty(token))
                return response.SetError(Messages.Error.TokenCreation());

            response.Data = new LoginResponse
            {
                Token = token,
                SessionExpiration = expiration,
                User = new LoginResponse.Item
                {
                    Id = user.Id,
                    Role = user.Role.Name,
                    Name = user.Name,
                    LastName = user.LastName,
                    Email = user.Email,
                }
            };
            return response;
        }

        public BaseResponse Logout()
        {
            var response = new BaseResponse();

            if (_tokenService.GetToken() == null)
                return response.SetError(Messages.Error.ExpiredToken());

            // Could add the token to a blacklist, but for now we just return success
            return response;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool ValidatePassword(string password)
        {
            return new bool[] {
                password.Length >= 8,
                password.Any(char.IsUpper),
                password.Any(char.IsLower),
                password.Any(char.IsDigit),
                password.Any(c => char.IsPunctuation(c) || char.IsSymbol(c))
            }.All(x => x);
        }

        public bool IsAdmin()
        {
            var token = _tokenService.GetToken();
            return token.Role == Roles.ADMIN;
        }

        public bool IsEmployee()
        {
            var token = _tokenService.GetToken();
            return token.Role == Roles.EMPLOYEE;
        }

        #region Private
        private static bool ValidateHashedPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private static bool ValidateEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        #endregion
    }
}
