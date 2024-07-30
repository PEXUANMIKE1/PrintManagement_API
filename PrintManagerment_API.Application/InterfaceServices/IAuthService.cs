using PrintManagerment_API.Application.Payload.RequestModels.UserRequests;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataUsers;
using PrintManagerment_API.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.InterfaceServices
{
    public interface IAuthService
    {
        Task<ResponseObject<DataResponseLogin>>Login(Request_Login request);
        Task<ResponseObject<DataResponseUser>>Register(Request_Register request);
        Task<string> ConfirmRegisterAccount(string confirmCode);
        Task<ResponseObject<DataResponseLogin>> GetJwtTokenAsync(User user);
        Task<ResponseObject<DataResponseLogin>> RenewAccessToken(Request_RefreshToken request);
        Task<ResponseObject<DataResponseUser>> ChangePassword(int userId, Request_ChangePassword request);
        Task<string> ForgotPassword(string email);//gửi mã xác nhận về email
        Task<string> ConfirmCreateNewPassword(Request_ForgotPasswordCreateNew request); //nhập mã xác nhận và tạo mới mk
        Task<ResponseObject<object>> ChangeRoleForUser(int userId, string role);
        Task<ResponseObject<IEnumerable<Role>>> GetAllRole();
        Task<ResponseObject<IEnumerable<string>>> GetRoleByIdUser(int userId);
    }
}
