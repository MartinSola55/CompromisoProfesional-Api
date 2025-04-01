using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CompromisoProfesional_Api.Models.DTO;
using CompromisoProfesional_Api.Services;
using CompromisoProfesional_Api.Models.DTO.SocialSecurity;
using CompromisoProfesional_Api.Models.Constants;

namespace CompromisoProfesional_Api.Controllers
{
    [Authorize(Policy = Policies.Admin)]
    public class SocialSecurityController(SocialSecurityService socialSecurityService) : BaseController
    {
        private readonly SocialSecurityService _socialSecurityService = socialSecurityService;

        #region CRUD
        [HttpPost]
        public async Task<BaseResponse<GetAllResponse>> GetAll([FromBody] GetAllRequest rq)
        {
            return await _socialSecurityService.GetAll(rq);
        }

        [HttpGet]
        public async Task<BaseResponse<GetOneResponse>> GetOne([FromQuery] GetOneRequest rq)
        {
            return await _socialSecurityService.GetOne(rq);
        }

        [HttpPost]
        public async Task<BaseResponse<CreateResponse>> Create([FromBody] CreateRequest rq)
        {
            return await _socialSecurityService.Create(rq);
        }

        [HttpPost]
        public async Task<BaseResponse> Update([FromBody] UpdateRequest rq)
        {
            return await _socialSecurityService.Update(rq);
        }

        [HttpPost]
        public async Task<BaseResponse> Delete([FromBody] DeleteRequest rq)
        {
            return await _socialSecurityService.Delete(rq);
        }
        #endregion
    }
}
