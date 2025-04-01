using Microsoft.EntityFrameworkCore;
using CompromisoProfesional_Api.DAL.DB;
using CompromisoProfesional_Api.Models;
using CompromisoProfesional_Api.Models.Constants;
using CompromisoProfesional_Api.Models.DTO;
using CompromisoProfesional_Api.Models.DTO.Profession;
using System.Data;

namespace CompromisoProfesional_Api.Services
{
    public class ProfessionService(APIContext context)
    {
        private readonly APIContext _db = context;

        #region CRUD
        public async Task<BaseResponse<GetAllResponse>> GetAll(GetAllRequest rq)
        {
            var response = new BaseResponse<GetAllResponse>();

            var query = _db
                .Profession
                .AsQueryable();

            query = OrderQuery(query, rq);

            response.Data = new GetAllResponse
            {
                Professions = await query
                .Select(x => new GetAllResponse.Item
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    CreatedAt = x.CreatedAt
                })
                .Skip((rq.Page - 1) * Pagination.DefaultPageSize)
                .Take(Pagination.DefaultPageSize)
                .ToListAsync()
            };

            return response;
        }

        public async Task<BaseResponse<GetOneResponse>> GetOne(GetOneRequest rq)
        {
            return new BaseResponse<GetOneResponse>
            {
                Data = await _db
                    .Profession
                    .Where(x => x.Id == rq.Id)
                    .Include(x => x.SuggestedPrices)
                        .ThenInclude(x => x.SocialSecurity)
                    .Select(x => new GetOneResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Type = x.Type,
                        CreatedAt = x.CreatedAt,
                        SuggestedPrices = x.SuggestedPrices.Select(x => new GetOneResponse.Item
                        {
                            Id = x.Id,
                            SocialSecurityName = x.SocialSecurity.Name,
                            Price = x.Price,
                            UpdatedAt = x.UpdatedAt ?? x.CreatedAt
                        }).ToList()
                    })
                    .FirstOrDefaultAsync()
            };
        }

        public async Task<BaseResponse<CreateResponse>> Create(CreateRequest rq)
        {
            var response = new BaseResponse<CreateResponse>();

            if (string.IsNullOrEmpty(rq.Name) || string.IsNullOrEmpty(rq.Type))
                return response.SetError(Messages.Error.FieldsRequired());

            var profession = new Profession
            {
                Name = rq.Name,
                Type = rq.Type,
            };

            _db.Profession.Add(profession);

            // Save changes
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return response.SetError(Messages.Error.Exception());
            }

            response.Data = new CreateResponse
            {
                Id = profession.Id
            };
            response.Message = Messages.CRUD.EntityCreated("Profesión", true);

            return response;
        }

        public async Task<BaseResponse> Update(UpdateRequest rq)
        {
            var response = new BaseResponse();

            var profession = await _db
                .Profession
                .FirstOrDefaultAsync(x => x.Id == rq.Id);

            if (profession == null)
                return response.SetError(Messages.Error.EntityNotFound("Profesión"));
            else if (string.IsNullOrEmpty(rq.Name) || string.IsNullOrEmpty(rq.Type))
                return response.SetError(Messages.Error.FieldsRequired());

            // Update fields
            profession.Name = rq.Name;
            profession.Type = rq.Type;
            profession.UpdatedAt = DateTime.UtcNow;

            // Save changes
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return response.SetError(Messages.Error.Exception());
            }

            response.Message = Messages.CRUD.EntityUpdated("Profesión");

            return response;
        }

        public async Task<BaseResponse> Delete(DeleteRequest rq)
        {
            var response = new BaseResponse();

            var profession = await _db
                .Profession
                .Include(x => x.SuggestedPrices)
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.Id == rq.Id);

            if (profession == null)
                return response.SetError(Messages.Error.EntityNotFound("Profesión"));

            // Remove related entities
            profession.Employees.ForEach(x => x.DeletedAt = DateTime.UtcNow);
            profession.SuggestedPrices.ForEach(x => x.DeletedAt = DateTime.UtcNow);

            // Delete profession
            profession.DeletedAt = DateTime.UtcNow;

            // Save changes
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return response.SetError(Messages.Error.Exception());
            }

            response.Message = Messages.CRUD.EntityDeleted("Profesión");
            return response;
        }
        #endregion

        #region Helpers
        private static IQueryable<Profession> OrderQuery(IQueryable<Profession> query, PaginateRequest rq)
        {
            return rq.ColumnSort switch
            {
                "name" => rq.SortDirection == SortDirectionCode.ASC ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name),
                "createdAt" => rq.SortDirection == SortDirectionCode.ASC ? query.OrderBy(x => x.CreatedAt) : query.OrderByDescending(x => x.CreatedAt),
                _ => query.OrderByDescending(x => x.Name),
            };
        }
        #endregion
    }
}