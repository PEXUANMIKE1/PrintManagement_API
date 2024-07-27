using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintManagerment_API.Application.Constants;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.RequestModels.UserRequests;

namespace PrintManagement_API.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Request_Register request)
        {
            return Ok(await _authService.Register(request));
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(string confirmCode)
        {
            return Ok(await _authService.ConfirmRegisterAccount(confirmCode));
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Request_Login request)
        {
            return Ok(await _authService.Login(request));
        }
        [HttpPost]
        public async Task<IActionResult> RenewAccessToken([FromBody] Request_RefreshToken request)
        {
            return Ok(await _authService.RenewAccessToken(request));
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //lấy dữ liệu token
        public async Task<IActionResult> ChangePassword([FromBody] Request_ChangePassword request)
        {
            int idUser = int.Parse(HttpContext.User.FindFirst("Id").Value);//lấy id từ token
            return Ok(await _authService.ChangePassword(idUser, request));
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            return Ok(await _authService.ForgotPassword(email));
        }
        [HttpPut]
        public async Task<IActionResult> ConfirmCreateNewPassword([FromBody] Request_ForgotPasswordCreateNew request)
        {
            return Ok(await _authService.ConfirmCreateNewPassword(request));
        }
        [HttpPut]
        public async Task<IActionResult> ChangeRoleForUser(int userId, string role)
        {
            return Ok(await _authService.ChangeRoleForUser(userId,role));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            return Ok(await _authService.GetAllRole());
        }
    }
}
