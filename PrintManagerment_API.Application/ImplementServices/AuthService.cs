using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Crypto.Generators;
using PrintManagerment_API.Application.Handle.HandleEmail;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.Mapper;
using PrintManagerment_API.Application.Payload.RequestModels.UserRequests;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataUsers;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using PrintManagerment_API.Doman.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace PrintManagerment_API.Application.ImplementServices
{
    public class AuthService : IAuthService
    {
        #region Private Members
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IBaseRepository<Role> _baseRoleRepository;
        private readonly IBaseRepository<ConfirmEmail> _baseConfirmEmailRepository;
        private readonly IBaseRepository<RefreshToken> _baseRefreshTokenlRepository;
        private readonly IBaseRepository<Permissions> _basePermissionRepository;
        private readonly IConfiguration _configuration;
        private readonly UserConverter _userConverter;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        #endregion

        #region Constructor
        public AuthService(IBaseRepository<User> baseUserRepository, IBaseRepository<Role> baseRoleRepository, IBaseRepository<ConfirmEmail> baseConfirmEmailRepository,
            IBaseRepository<RefreshToken> baseRefreshTokenRepository, IBaseRepository<Permissions> basePermissionsRepository, IConfiguration configuration,
            UserConverter userConverter, IEmailService emailService, IUserRepository userRepository)
        {
            _baseUserRepository = baseUserRepository;
            _baseRoleRepository = baseRoleRepository;
            _baseConfirmEmailRepository = baseConfirmEmailRepository;
            _baseRefreshTokenlRepository = baseRefreshTokenRepository;
            _configuration = configuration;
            _userConverter = userConverter;
            _emailService = emailService;
            _userRepository = userRepository;
        }
        #endregion

        #region Public Methods
        public async Task<ResponseObject<DataResponseLogin>> GetJwtTokenAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseObject<DataResponseLogin>> Login(Request_Login request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseObject<DataResponseUser>> Register(Request_Register request)
        {
            try
            {
                if(!ValidateInput.IsValidEmail(request.Email))
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Email không hợp lệ!",
                        Data = null
                    };
                }
                if (!ValidateInput.IsValidPhoneNumber(request.PhoneNumber))
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Số điện thoại không hợp lệ!",
                        Data = null
                    };
                }
                if(await _userRepository.GetUserByPhoneNumber(request.PhoneNumber) != null)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Phone number đã tồn tại",
                        Data = null
                    };
                }
                if (await _userRepository.GetUserByEmail(request.Email) != null)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Email đã tồn tại",
                        Data = null
                    };
                }
                if (await _userRepository.GetUserByUserName(request.UserName) != null)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "User name đã tồn tại",
                        Data = null
                    };
                }
                var User = new User
                {
                    IsActive = false,// Phải xác thực email thì tài khoản mới được hoạt động
                    CreateTime = DateTime.Now,
                    UserName = request.UserName,
                    Password = BCryptNet.HashPassword(request.Password),
                    FullName = request.FullName,
                    DateOfBirth = request.DateOfBirth,
                    Email = request.Email,
                    Avatar = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.freepik.com%2Ffree-photos-vectors" +
                                "%2Favatar&psig=AOvVaw0o8g_fkfqKoiSN2civWqmb&ust=1720599142929000&source" +
                                "=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCMDBkLvBmYcDFQAAAAAdAAAAABAE",
                    PhoneNumber = request.PhoneNumber,
                };
                User = await _baseUserRepository.CreateAsync(User);
                //add role mặc định cho tài khoản: User
                //await _userRepository.AddRoleForUserAsync(User, new List<string> { "User" });
                //tạo đối tượng bảng confirmEmail lưu vào db
                ConfirmEmail confirmEmail = new ConfirmEmail
                {
                    ConfirmCode = GenerateCodeActive(),
                    IsConfirmed = false,
                    UserId = User.Id,
                    ExpiryTime = DateTime.Now.AddMinutes(3),
                };
                confirmEmail = await _baseConfirmEmailRepository.CreateAsync(confirmEmail);
                var message = new EmailMessage(new string[] { request.Email }, "Xác nhận email đăng ký tài khoản",$"Mã xác nhận email của bạn (tồn tại 3 phút): {confirmEmail.ConfirmCode}");
                //send confirm code về mail 
                var responseMessage = _emailService.SendEmail(message);

                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status201Created,
                    Message = $"Mã xác nhận email đã được gửi! Vui lòng kiểm tra email",
                    Data = _userConverter.EntityDTO(User)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error: " + ex.StackTrace,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<DataResponseLogin>> RenewAccessToken(Request_RefreshToken request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseObject<DataResponseUser>> ChangePassword(int userId, Request_ChangePassword request)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ConfirmCreateNewPassword(Request_ForgotPasswordCreateNew request)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ConfirmRegisterAccount(string confirmCode)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ForgotPassword(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<string> AddRoleForUser(int userId, List<string> roles)
        {
            throw new NotImplementedException();
        }

        public async Task<string> DeleteRoleForUser(int userId, List<string> roles)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private string GenerateCodeActive()
        {
            var str = "code_"+DateTime.Now.Ticks.ToString();
            return str;
        }
        #endregion
    }
}
