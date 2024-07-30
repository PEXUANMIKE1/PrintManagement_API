using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto.Generators;
using PrintManagerment_API.Application.Handle.HandleEmail;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.Mapper;
using PrintManagerment_API.Application.Payload.RequestModels.UserRequests;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataUsers;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.Enumerates;
using PrintManagerment_API.Doman.InterfaceRepositories;
using PrintManagerment_API.Doman.Validations;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
        private readonly IBaseRepository<Team> _baseTeamRepository;
        private readonly IConfiguration _configuration;
        private readonly UserConverter _userConverter;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public AuthService(IBaseRepository<User> baseUserRepository, IBaseRepository<Role> baseRoleRepository, IBaseRepository<ConfirmEmail> baseConfirmEmailRepository,
            IBaseRepository<RefreshToken> baseRefreshTokenRepository, IBaseRepository<Permissions> basePermissionsRepository, IConfiguration configuration,
            UserConverter userConverter, IEmailService emailService, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IBaseRepository<Team> baseTeamRepository)
        {
            _baseUserRepository = baseUserRepository;
            _baseRoleRepository = baseRoleRepository;
            _baseConfirmEmailRepository = baseConfirmEmailRepository;
            _baseRefreshTokenlRepository = baseRefreshTokenRepository;
            _basePermissionRepository = basePermissionsRepository;
            _configuration = configuration;
            _userConverter = userConverter;
            _emailService = emailService;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _baseTeamRepository = baseTeamRepository;
        }
        #endregion

        #region Private Methods
        private string GenerateCodeActive()
        {
            var str = "code_" + DateTime.Now.Ticks.ToString();
            return str;
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInHours"], out int tokenValidityInHours);
            var expirationUTC = DateTime.UtcNow.AddHours(tokenValidityInHours);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: expirationUTC,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new Byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }
        #endregion

        #region Public Methods
        public async Task<ResponseObject<DataResponseLogin>> GetJwtTokenAsync(User user)
        {
            try
            {
                var permissions = await _basePermissionRepository.GetAllAsync(x=>x.UserId == user.Id);
                var roles = await _baseRoleRepository.GetAllAsync();
                var TeamName = "";
                if(user.TeamId.HasValue)
                {
                    var team = await _baseTeamRepository.GetByIdAsync(user.TeamId.Value);
                    if(team == null)
                    {
                        return new ResponseObject<DataResponseLogin>
                        {
                            Status = StatusCodes.Status200OK,
                            Message = "Có lỗi xảy ra",
                            Data = null
                        };
                    }

                    TeamName = team.Name;
                }
                var authCliams = new List<Claim>
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("UserName", user.UserName),
                    new Claim("Email", user.Email),
                    new Claim("FullName", user.FullName),
                    new Claim("PhoneNumber", user.PhoneNumber),
                    new Claim("Team", TeamName),
                    new Claim("Avatar", user.Avatar),
                };
                foreach(var permission in permissions)
                {
                    foreach(var role in roles)
                    {
                        if(permission.RoleId == role.Id)
                        {
                            authCliams.Add(new Claim("Permission", role.RoleName));
                        }
                    }
                }
                var userRole = await _userRepository.GetRolesOfUserAsync(user);
                foreach(var item in userRole)
                {
                    authCliams.Add(new Claim(ClaimTypes.Role, item));
                }
                var jwtToken = GetToken(authCliams);
                var refreshToken = GenerateRefreshToken();
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidity"], out int refreshTokenValidity);
                RefreshToken rf = new RefreshToken
                {
                    ExpiryTime = DateTime.UtcNow.AddHours(refreshTokenValidity),
                    UserId = user.Id,
                    Token = refreshToken
                };
                rf = await _baseRefreshTokenlRepository.CreateAsync(rf);
                return new ResponseObject<DataResponseLogin>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Tạo token thành công",
                    Data = new DataResponseLogin
                    {
                        AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        RefreshToken = refreshToken
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseLogin>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error" + ex.Message,
                    Data = null
                };
            }
        }
        public async Task<ResponseObject<DataResponseLogin>> Login(Request_Login request)
        {
            try
            {
                var userLog = await _baseUserRepository.GetAsync(x => x.UserName.Equals(request.UserName));
                if (userLog == null)
                {
                    return new ResponseObject<DataResponseLogin>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "User name không chính xác!",
                        Data = null
                    };
                }
                if (userLog.IsActive == false) 
                {
                    return new ResponseObject<DataResponseLogin>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Tài khoản này đã bị ban!",
                        Data = null
                    };
                }
                var confirmEmail = await _baseConfirmEmailRepository.GetAsync(x=>x.UserId == userLog.Id);
                if (confirmEmail.IsConfirmed == false)
                {
                    return new ResponseObject<DataResponseLogin>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Tài khoản chưa được xác thực email!",
                        Data = null
                    };
                }
                bool checkPass = BCryptNet.Verify(request.Password,userLog.Password);
                if (!checkPass)
                {
                    return new ResponseObject<DataResponseLogin>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mật khẩu không chính xác!",
                        Data = null
                    };
                }
                var Token = await GetJwtTokenAsync(userLog);
                return new ResponseObject<DataResponseLogin>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Đăng nhập thành công!",
                    Data = new DataResponseLogin
                    {
                        AccessToken = Token.Data.AccessToken,
                        RefreshToken = Token.Data.RefreshToken,
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseLogin>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Đăng nhập thất bại!\nError: " + ex.StackTrace,
                    Data = null
                };
            }
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
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var User = new User
                        {
                            IsActive = true,
                            CreateTime = DateTime.Now,
                            UserName = request.UserName,
                            Password = BCryptNet.HashPassword(request.Password),
                            FullName = request.FullName,
                            DateOfBirth = request.DateOfBirth,
                            Email = request.Email,
                            Avatar = "avt-default.png",
                            PhoneNumber = request.PhoneNumber,
                            TeamId = null,
                        };
                        User = await _baseUserRepository.CreateAsync(User);
                        //add role mặc định cho tài khoản: Employee
                        //await _userRepository.AddListRoleForUserAsync(User, new List<string> { "Employee" });
                        await _userRepository.ChangeRoleForUserAsync(User, "Employee");
                        //tạo đối tượng bảng confirmEmail lưu vào db
                        ConfirmEmail confirmEmail = new ConfirmEmail
                        {
                            ConfirmCode = GenerateCodeActive(),
                            IsConfirmed = false,
                            UserId = User.Id,
                            ExpiryTime = DateTime.Now,
                        };
                        confirmEmail = await _baseConfirmEmailRepository.CreateAsync(confirmEmail);
                        var message = new EmailMessage(new string[] { request.Email }, "Xác nhận email đăng ký tài khoản", $"Mã xác nhận email của bạn: {confirmEmail.ConfirmCode}");
                        //send confirm code về mail 
                        var responseMessage = _emailService.SendEmail(message);
                        scope.Complete();
                        return new ResponseObject<DataResponseUser>
                        {
                            Status = StatusCodes.Status201Created,
                            Message = $"Mã xác nhận email đã được gửi! Vui lòng kiểm tra email",
                            Data = await _userConverter.EntityDTO(User)
                        };
                    }
                    catch (Exception ex)
                    {
                        scope.Dispose();
                        return new ResponseObject<DataResponseUser>
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Message = "Error: " + ex.StackTrace,
                            Data = null
                        };
                    }
                }
                
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
            try
            {
                var rfToken = await _baseRefreshTokenlRepository.GetAsync(x => x.Token.Equals(request.RefreshToken));
                if (rfToken.ExpiryTime < DateTime.UtcNow)
                {
                    return new ResponseObject<DataResponseLogin>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Refesh Token đã hết hạn",
                        Data = null
                    };
                }
                var user = await _baseUserRepository.GetByIdAsync(rfToken.UserId);
                var newAccessToken = await GetJwtTokenAsync(user);
                return new ResponseObject<DataResponseLogin>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tạo mới token thành công",
                    Data = new DataResponseLogin
                    {
                        AccessToken = newAccessToken.Data.AccessToken,
                        RefreshToken = newAccessToken.Data.RefreshToken
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ResponseObject<DataResponseUser>> ChangePassword(int userId, Request_ChangePassword request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            try
            {
                if(!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                var user = await _baseUserRepository.GetByIdAsync(userId);
                var checkPass = BCryptNet.Verify(request.OldPassword, user.Password);
                if (!checkPass)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mật khẩu không chính xác !",
                        Data = null
                    };
                }
                if (request.newPassword.Equals(request.OldPassword))
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mật khẩu mới trùng với mật khẩu cũ! Vui lòng thay đổi",
                        Data = null
                    };
                }
                if (!request.newPassword.Equals(request.ConfirmPassword))
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mật khẩu xác nhận không trùng khớp!",
                        Data = null
                    };
                }
                user.Password = BCryptNet.HashPassword(request.newPassword);
                user.UpdateTime = DateTime.UtcNow;
                await _baseUserRepository.UpdateAsync(user);
                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Đổi mật khẩu thành công!",
                    Data = await _userConverter.EntityDTO(user)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Có lỗi trong quá trình xử lý\n Erorr: " + ex.StackTrace,
                    Data = null
                };
            }
        }
        public async Task<string> ConfirmCreateNewPassword(Request_ForgotPasswordCreateNew request)
        {
            try
            {
                var confirm = await _baseConfirmEmailRepository.GetAsync(x => x.ConfirmCode.Equals(request.ConfirmCode));
                if (confirm == null)
                {
                    return "Mã xác nhận không chính xác!";
                }
                if (confirm.ExpiryTime < DateTime.UtcNow)
                {
                    return "Mã xác nhận đã hết hạn! Vui lòng gửi lại";
                }
                if (!request.NewPassword.Equals(request.ConfirmPassword))
                {
                    return "Mật khẩu xác nhận không trùng khớp";
                }
                var user = await _baseUserRepository.GetByIdAsync(u => u.Id == confirm.UserId);
                user.Password = BCryptNet.HashPassword(request.NewPassword);
                user.UpdateTime = DateTime.UtcNow;
                await _baseUserRepository.UpdateAsync(user);
                return "Thay đổi mật khẩu thành công!";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.StackTrace;
            }
        }
        public async Task<string> ConfirmRegisterAccount(string confirmCode)
        {
            try
            {
                var code = await _baseConfirmEmailRepository.GetAsync(x => x.ConfirmCode.Equals(confirmCode));
                if (code == null)
                {
                    return "Mã xác nhận không hợp lệ!";
                }
                if (code.IsConfirmed == true)
                {
                    return "Bạn đã xác nhận email này rồi!";
                }
                code.IsConfirmed = true;
                await _baseConfirmEmailRepository.UpdateAsync(code);
                return "Xác nhận đăng ký tài khoản thành công. Bạn có thể đăng nhập vào hệ thống!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> ForgotPassword(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(email);
                if (user == null)
                {
                    return "Email không tồn tại!";
                }
                ConfirmEmail confirmEmail = new ConfirmEmail
                {
                    ConfirmCode = GenerateCodeActive(),
                    ExpiryTime = DateTime.UtcNow.AddMinutes(3),
                    UserId = user.Id,
                    IsConfirmed = false,
                };
                confirmEmail = await _baseConfirmEmailRepository.CreateAsync(confirmEmail);
                var message = new EmailMessage(new string[] { user.Email }, "Mã xác nhận quên mật khẩu!", $"Mã xác nhận quên mật khẩu của bạn(3p): {confirmEmail.ConfirmCode}");
                var send = _emailService.SendEmail(message);
                return "Đã gửi mã xác nhận quên mật khẩu! Vui lòng kiểm tra hòm thư của bạn";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.StackTrace;
            }
        }
        public async Task<ResponseObject<object>> ChangeRoleForUser(int userId, string role)
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<object>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<object>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Người dùng không đủ quyền để sử dụng chức năng này",
                        Data = null
                    };
                }
                var user = await _baseUserRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    return new ResponseObject<object>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Người dùng không tồn tại hoặc đã bị xóa!",
                        Data = null
                    };
                }
                await _userRepository.ChangeRoleForUserAsync(user,role);

                return new ResponseObject<object>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Thay đổi quyền người dùng thành công",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<object>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error:"+ ex.StackTrace,
                    Data = null
                };
            }
        }
        public async Task<ResponseObject<IEnumerable<Role>>> GetAllRole()
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<IEnumerable<Role>>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<IEnumerable<Role>>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Người dùng không đủ quyền để sử dụng chức năng này",
                        Data = null
                    };
                }
                var roles = await _baseRoleRepository.GetAllAsync();
                if (roles == null)
                {
                    return new ResponseObject<IEnumerable<Role>>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Danh sách trống!",
                        Data = null
                    };
                }
                return new ResponseObject<IEnumerable<Role>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Lấy danh sách roles thành công",
                    Data = roles
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<IEnumerable<Role>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Error:" + ex.StackTrace,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<IEnumerable<string>>> GetRoleByIdUser(int userId)
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<IEnumerable<string>>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<IEnumerable<string>>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Người dùng không đủ quyền để sử dụng chức năng này",
                        Data = null
                    };
                }
                var user = await _baseUserRepository.GetByIdAsync(userId);
                var roles = await _userRepository.GetRolesOfUserAsync(user);
                return new ResponseObject<IEnumerable<string>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Lấy danh sách roles thành công",
                    Data = roles
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<IEnumerable<string>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Error:" + ex.StackTrace,
                    Data = null
                };
            }
        }

        #endregion

    }
}
