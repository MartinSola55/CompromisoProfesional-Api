using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CompromisoProfesional_Api.Models.DTO;
using CompromisoProfesional_Api.Services;
using CompromisoProfesional_Api.Models.DTO.User;
using CompromisoProfesional_Api.Models.Constants;

namespace CompromisoProfesional_Api.Controllers
{
    public class UserController(UserService userService) : BaseController
    {
        private readonly UserService _userService = userService;

        #region Combos
        [HttpGet]
        public async Task<BaseResponse<BaseComboResponse>> GetComboRoles()
        {
            return await _userService.GetComboRoles();
        }

        [HttpGet]
        public async Task<BaseResponse<BaseComboResponse>> GetComboEmployees()
        {
            return await _userService.GetComboEmployees();
        }
        #endregion

        #region CRUD

        [HttpPost]
        public async Task<BaseResponse<GetAllResponse>> GetAll([FromBody] GetAllRequest rq)
        {
            return await _userService.GetAll(rq);
        }

        [HttpGet]
        public async Task<BaseResponse<GetOneResponse>> GetOneById([FromQuery] GetOneRequest rq)
        {
            return await _userService.GetOneById(rq);
        }

        [HttpPost]
        [Authorize(Policy = Policies.Admin)]
        public async Task<BaseResponse<CreateResponse>> Create([FromBody] CreateRequest rq)
        {
            return await _userService.Create(rq);
        }

        [HttpPost]
        public async Task<BaseResponse<UpdateResponse>> Update([FromBody] UpdateRequest rq)
        {
            return await _userService.Update(rq);
        }

        [HttpPost]
        [Authorize(Policy = Policies.Admin)]
        public async Task<BaseResponse> Delete([FromBody] DeleteRequest rq)
        {
            return await _userService.Delete(rq);
        }

        #endregion

        #region Methods
        [HttpPost]
        public async Task<BaseResponse> UpdatePassword([FromBody] UpdatePasswordRequest rq)
        {
            return await _userService.UpdatePassword(rq);
        }
        #endregion
    }
}
