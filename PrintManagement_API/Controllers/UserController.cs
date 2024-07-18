using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintManagerment_API.Application.Constants;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.Response;

namespace PrintManagement_API.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetInforMyself()
        {
            int idUser = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _userService.GetUserById(idUser));
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var currentUser = HttpContext.User;
            if (!currentUser.IsInRole("Admin"))
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new ResponseObject<object>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Người dùng không đủ quyền để sử dụng chức năng này!",
                        Data = null
                    }
                );
            }
            return Ok(await _userService.GetUserById(userId));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _userService.GetAllUser());
        }
        [HttpGet]
        public async Task<IActionResult>GetAllUserOfTeam(int teamId)
        {
            return Ok(await _userService.GetAllUserOfTeam(teamId));
        }
    }
}
