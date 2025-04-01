using Microsoft.EntityFrameworkCore;
using CompromisoProfesional_Api.DAL.DB;
using CompromisoProfesional_Api.Models;
using CompromisoProfesional_Api.Models.Constants;
using CompromisoProfesional_Api.Models.DTO;
using CompromisoProfesional_Api.Models.DTO.SocialSecurity;
using System.Data;

namespace CompromisoProfesional_Api.Services
{
    public class SocialSecurityService(APIContext context)
    {
        private readonly APIContext _db = context;

        #region CRUD
        public async Task<BaseResponse<GetAllResponse>> GetAll(GetAllRequest rq)
        {
            var response = new BaseResponse<GetAllResponse>();

            var query = _db
                .SocialSecurity
                .Include(x => x.SuggestedPrices)
                .AsQueryable();

            query = OrderQuery(query, rq);

            response.Data = new GetAllResponse
            {
                SocualSecurities = await query
                .Select(x => new GetAllResponse.Item
                {
                    Id = x.Id,
                    Name = x.Name,
                    CUIT = x.CUIT,
                    CreatedAt = x.CreatedAt,
                    SuggestedPrices = x.SuggestedPrices.Select(x => new GetAllResponse.Item.PriceItem
                    {
                        Price = x.Price,
                        LastUpdatedAt = x.UpdatedAt ?? x.CreatedAt
                    }).ToList()
                })
                .Skip((rq.Page - 1) * Pagination.DefaultPageSize)
                .Take(Pagination.DefaultPageSize)
                .ToListAsync()
            };

            return response;
        }

        public async Task<BaseResponse<GetOneResponse>> GetOne(GetOneRequest rq)
        {
            var response = new BaseResponse<GetOneResponse>();

            var socialSecurity = await _db
                .SocialSecurity
                .Where(x => x.Id == rq.Id)
                .Include(x => x.SuggestedPrices)
                .Select(x => new GetOneResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    CUIT = x.CUIT,
                    SuggestedPrices = x.SuggestedPrices.Select(x => new GetOneResponse.Item
                    {
                        Id = x.Id,
                        SocialSecurityName = x.SocialSecurity.Name,
                        Price = x.Price,
                        UpdatedAt = x.UpdatedAt ?? x.CreatedAt
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (socialSecurity == null)
                return response.SetError(Messages.Error.EntityNotFound("Obra social", true));

            response.Data = socialSecurity;

            return response;
        }

        public async Task<BaseResponse<CreateResponse>> Create(CreateRequest rq)
        {
            var response = new BaseResponse<CreateResponse>();

            if (string.IsNullOrEmpty(rq.Name) || string.IsNullOrEmpty(rq.CUIT))
                return response.SetError(Messages.Error.FieldsRequired());

            var socialSecurity = new SocialSecurity
            {
                Name = rq.Name,
                CUIT = rq.CUIT,
            };

            _db.SocialSecurity.Add(socialSecurity);

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
                Id = socialSecurity.Id
            };
            response.Message = Messages.CRUD.EntityCreated("Obra social", true);

            return response;
        }

        public async Task<BaseResponse> Update(UpdateRequest rq)
        {
            var response = new BaseResponse();

            var socialSecurity = await _db
                .SocialSecurity
                .FirstOrDefaultAsync(x => x.Id == rq.Id);

            if (socialSecurity == null)
                return response.SetError(Messages.Error.EntityNotFound("Obra social", true));
            else if (string.IsNullOrEmpty(rq.Name) || string.IsNullOrEmpty(rq.CUIT))
                return response.SetError(Messages.Error.FieldsRequired());

            // Update fields
            socialSecurity.Name = rq.Name;
            socialSecurity.CUIT = rq.CUIT;
            socialSecurity.UpdatedAt = DateTime.UtcNow;

            // Save changes
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return response.SetError(Messages.Error.Exception());
            }

            response.Message = Messages.CRUD.EntityUpdated("Obra social", true);

            return response;
        }

        public async Task<BaseResponse> Delete(DeleteRequest rq)
        {
            var response = new BaseResponse();

            var socialSecurity = await _db
                .SocialSecurity
                .Include(x => x.Patients)
                .Include(x => x.SuggestedPrices)
                .FirstOrDefaultAsync(x => x.Id == rq.Id);

            if (socialSecurity == null)
                return response.SetError(Messages.Error.EntityNotFound("Obra social", true));

            if (socialSecurity.Patients.Count > 0)
                return response.SetError(Messages.Error.EntityWithRelations("obra social", "pacientes", true));

            // Delete related entities
            socialSecurity.SuggestedPrices.ForEach(x => x.DeletedAt = DateTime.UtcNow);

            // Delete
            socialSecurity.DeletedAt = DateTime.UtcNow;

            // Save changes
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return response.SetError(Messages.Error.Exception());
            }

            response.Message = Messages.CRUD.EntityDeleted("Obra social", true);
            return response;
        }
        #endregion

        #region Helpers
        private static IQueryable<SocialSecurity> OrderQuery(IQueryable<SocialSecurity> query, PaginateRequest rq)
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