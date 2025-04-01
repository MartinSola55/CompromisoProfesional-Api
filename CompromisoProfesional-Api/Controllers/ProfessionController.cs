using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CompromisoProfesional_Api.Models.DTO;
using CompromisoProfesional_Api.Services;
using CompromisoProfesional_Api.Models.DTO.Profession;
using CompromisoProfesional_Api.Models.Constants;

namespace CompromisoProfesional_Api.Controllers
{
    [Authorize(Policy = Policies.Admin)]
    public class ProfessionController(ProfessionService professionService) : BaseController
    {
        private readonly ProfessionService _professionService = professionService;

        #region CRUD
        [HttpPost]
        public async Task<BaseResponse<GetAllResponse>> GetAll([FromBody] GetAllRequest rq)
        {
            return await _professionService.GetAll(rq);
        }

        [HttpGet]
        public async Task<BaseResponse<GetOneResponse>> GetOne([FromQuery] GetOneRequest rq)
        {
            return await _professionService.GetOne(rq);
        }

        [HttpPost]
        public async Task<BaseResponse<CreateResponse>> Create([FromBody] CreateRequest rq)
        {
            return await _professionService.Create(rq);
        }

        [HttpPost]
        public async Task<BaseResponse> Update([FromBody] UpdateRequest rq)
        {
            return await _professionService.Update(rq);
        }

        [HttpPost]
        public async Task<BaseResponse> Delete([FromBody] DeleteRequest rq)
        {
            return await _professionService.Delete(rq);
        }
        #endregion
    }
}
