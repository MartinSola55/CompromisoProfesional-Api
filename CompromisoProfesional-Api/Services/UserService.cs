using Microsoft.EntityFrameworkCore;
using CompromisoProfesional_Api.DAL.DB;
using CompromisoProfesional_Api.Models;
using CompromisoProfesional_Api.Models.Constants;
using CompromisoProfesional_Api.Models.DTO;
using CompromisoProfesional_Api.Models.DTO.User;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CompromisoProfesional_Api.Services
{
    public class UserService(APIContext context, TokenService tokenService, AuthService authService)
    {
        private readonly APIContext _db = context;
        private readonly Token _token = tokenService.GetToken();
        private readonly AuthService _authService = authService;

        #region Combos
        public async Task<BaseResponse<BaseComboResponse>> GetComboRoles()
        {
            var response = new BaseResponse<BaseComboResponse>
            {
                Data = new BaseComboResponse
                {
                    Items = await _db.Role
                    .Select(x => new BaseComboResponse.Item
                    {
                        Id = x.Id,
                        Description = x.Name
                    })
                    .ToListAsync()
                }
            };
            return response;
        }

        public async Task<BaseResponse<BaseComboResponse>> GetComboEmployees()
        {
            var query = _db
                .User
                .Include(x => x.Role)
                .Where(x => x.Role.Name == Roles.EMPLOYEE)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.Name)
                .AsQueryable();

            var response = new BaseResponse<BaseComboResponse>
            {
                Data = new BaseComboResponse
                {
                    Items = await query
                    .Select(x => new BaseComboResponse.Item
                    {
                        Id = x.Id,
                        Description = x.LastName + ", " + x.Name
                    })
                    .ToListAsync()
                }
            };
            return response;
        }
        #endregion

        #region CRUD
        public async Task<BaseResponse<GetAllResponse>> GetAll(GetAllRequest rq)
        {
            var query = _db.User.AsQueryable();

            query = FilterQuery(query, rq);
            query = OrderQuery(query, rq);

            if (!_authService.IsAdmin())
                query = query.Where(x => x.Id == _token.UserId);

            if (rq.Roles.Count > 0)
            {
                var roles = await _db.Role
                    .Where(x => rq.Roles.Contains(x.Name))
                    .Select(x => x.Id)
                    .ToListAsync();

                query = query.Where(x => roles.Contains(x.Role.Id));
            }

            var response = new BaseResponse<GetAllResponse>
            {
                Data = new GetAllResponse
                {
                    Users = await query.Select(x => new GetAllResponse.Item
                    {
                        Id = x.Id,
                        Name = x.Name,
                        LastName = x.LastName,
                        Email = x.Email,
                        CreatedAt = x.CreatedAt,
                        Role = x.Role.Name
                    })
                    .Skip((rq.Page - 1) * Pagination.DefaultPageSize)
                    .Take(Pagination.DefaultPageSize)
                    .ToListAsync()
                }
            };
            return response;
        }

        public async Task<BaseResponse<GetOneResponse>> GetOneById(GetOneRequest rq)
        {
            var response = new BaseResponse<GetOneResponse>();
            var user = await _db
                .User
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == rq.Id);

            if (user == null)
                return response.SetError(Messages.Error.EntityNotFound("Usuario"));

            response.Data = new GetOneResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName,
                RoleName = user.Role.Name,
                CreatedAt = user.CreatedAt
            };

            return response;
        }

        public async Task<BaseResponse<CreateResponse>> Create(CreateRequest rq)
        {
            var response = new BaseResponse<CreateResponse>();

            var role = await _db.Role
               .Where(x => x.Name == rq.RoleName)
               .FirstOrDefaultAsync();

            if (role == null)
                return response.SetError(Messages.Error.EntityNotFound("Rol"));

            // Create user and assign role
            User user = new()
            {
                Email = rq.Email,
                Name = rq.Name,
                LastName = rq.LastName,
                RoleId = role.Id
            };

            // Validate request
            if (!ValidateFields(user))
                return response.SetError(Messages.Error.FieldsRequired());

            if (string.IsNullOrEmpty(rq.Email) || !ValidateEmail(rq.Email))
                return response.SetError(Messages.Error.InvalidEmail());

            if (string.IsNullOrEmpty(rq.Password) || !_authService.ValidatePassword(rq.Password))
                return response.SetError(Messages.Error.InvalidPassword());

            if (await _db.User.AnyAsync(x => x.Email == rq.Email))
                return response.SetError(Messages.Error.DuplicateEmail());

            // Save changes
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return response.SetError(Messages.Error.Exception());
            }

            response.Message = Messages.CRUD.EntityCreated("Usuario");
            response.Data = new CreateResponse
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName,
                Role = role.Name
            };

            return response;
        }

        public async Task<BaseResponse<UpdateResponse>> Update(UpdateRequest rq)
        {
            var response = new BaseResponse<UpdateResponse>();

            if (!_authService.IsAdmin() && rq.Id != _token.UserId)
                return response.SetError(Messages.Error.Unauthorized());

            var user = await _db
                .User
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == rq.Id);

            if (user == null)
                return response.SetError(Messages.Error.EntityNotFound("Usuario"));

            var role = await _db.Role
                .FirstOrDefaultAsync(x => x.Name == rq.RoleName);

            if (role == null)
                return response.SetError(Messages.Error.EntityNotFound("Rol"));

            if (_authService.IsAdmin())
            {
                // Validate email
                if (!ValidateEmail(rq.Email))
                    return response.SetError(Messages.Error.InvalidEmail());

                // Check if the new email is already in use by another user
                if (await _db.User.AnyAsync(x => x.Email.ToLower() == rq.Email.ToLower() && x.Id != rq.Id))
                    return response.SetError(Messages.Error.DuplicateEmail());

                // Update user data
                user.Email = rq.Email;
                user.Name = rq.Name;
                user.LastName = rq.LastName;
            }
            // Update user data
            user.UpdatedAt = DateTime.UtcNow;

            if (!ValidateFields(user))
                return response.SetError(Messages.Error.FieldsRequired());

            // Save changes
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return response.SetError(Messages.Error.Exception());
            }

            response.Message = Messages.CRUD.EntityUpdated("Usuario");
            response.Data = new UpdateResponse
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName,
                Role = role.Name
            };

            return response;
        }

        public async Task<BaseResponse> Delete(DeleteRequest rq)
        {
            var response = new BaseResponse();

            // Retrieve user
            var user = await _db.User.FirstOrDefaultAsync(x => x.Id == rq.Id);

            if (user == null)
                return response.SetError(Messages.Error.EntityNotFound("Usuario"));

            // Delete user
            user.DeletedAt = DateTime.UtcNow;

            // Save changes
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return response.SetError(Messages.Error.Exception());
            }

            response.Message = Messages.CRUD.EntityDeleted("Usuario");
            return response;
        }

        #endregion

        #region Methods
        public async Task<BaseResponse> UpdatePassword(UpdatePasswordRequest rq)
        {
            var response = new BaseResponse();

            if (!_authService.IsAdmin() && rq.Id.HasValue && rq.Id.Value != _token.UserId)
                return response.SetError(Messages.Error.Unauthorized());

            // Validate request
            if (string.IsNullOrEmpty(rq.Password))
                return response.SetError(Messages.Error.FieldsRequired());

            if (!_authService.ValidatePassword(rq.Password))
                return response.SetError(Messages.Error.InvalidPassword());

            // Retrieve user
            var userId = rq.Id ?? _token.UserId;
            var user = await _db
                .User
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                return response.SetError(Messages.Error.EntityNotFound("Usuario"));

            // Update password
            user.PasswordHash = _authService.HashPassword(rq.Password);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return response.SetError(Messages.Error.Exception());
            }

            response.Message = Messages.CRUD.EntityUpdated("Contraseña");
            return response;
        }

        #endregion

        #region Validations

        private static bool ValidateFields(User entity)
        {
            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.LastName) || string.IsNullOrEmpty(entity.Email))
                return false;

            return true;
        }

        private static bool ValidateEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        #endregion

        #region Helpers
        private static IQueryable<User> FilterQuery(IQueryable<User> query, GetAllRequest rq)
        {
            if (rq.DateFrom.HasValue && rq.DateTo.HasValue && rq.DateFrom <= rq.DateTo)
            {
                var dateFromUTC = DateTime.SpecifyKind(rq.DateFrom.Value, DateTimeKind.Utc).Date;
                var dateToUTC = DateTime.SpecifyKind(rq.DateTo.Value, DateTimeKind.Utc).Date;

                query = query.Where(x => x.CreatedAt.Date >= dateFromUTC && x.CreatedAt.Date <= dateToUTC);
            }

            return query;
        }

        private static IQueryable<User> OrderQuery(IQueryable<User> query, PaginateRequest rq)
        {
            return rq.ColumnSort switch
            {
                "createdAt" => rq.SortDirection == SortDirectionCode.ASC ? query.OrderBy(x => x.CreatedAt) : query.OrderByDescending(x => x.CreatedAt),
                _ => query.OrderByDescending(x => x.CreatedAt),
            };
        }
        #endregion
    }
}
