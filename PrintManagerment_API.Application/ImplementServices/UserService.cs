using Microsoft.AspNetCore.Http;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.Mapper;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataUsers;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.ImplementServices
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserConverter _userConverter;
        public UserService(IBaseRepository<User> baseUserRepository, IHttpContextAccessor contextAccessor, UserConverter userConverter)
        {
            _baseUserRepository = baseUserRepository;
            _contextAccessor = contextAccessor;
            _userConverter = userConverter;
        }

        public async Task<ResponseObject<IEnumerable<DataResponseUser>>>GetAllUser()
        {
            try
            {
                var currentUser = _contextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<IEnumerable<DataResponseUser>>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực!",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<IEnumerable<DataResponseUser>>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không đủ quyền hạn để sử dụng chức năng này!",
                        Data = null
                    };
                }
                var allUser = await _baseUserRepository.GetAllAsync();
                if (allUser == null)
                {
                    return new ResponseObject<IEnumerable<DataResponseUser>>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Danh sách trống!",
                        Data = null
                    };
                }

                var allUserDTO = new List<DataResponseUser>();
                foreach (var user in allUser)
                {
                    allUserDTO.Add(await _userConverter.EntityDTO(user));
                }
                var reversedList = allUserDTO.Reverse<DataResponseUser>().ToList();
                return new ResponseObject<IEnumerable<DataResponseUser>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Lấy dữ liệu thành công!",
                    Data = reversedList
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<IEnumerable<DataResponseUser>>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error:" + ex.StackTrace,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<IEnumerable<DataResponseUser>>>GetAllUserOfTeam(int teamId)
        {
            try
            {
                var currentUser = _contextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<IEnumerable<DataResponseUser>>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực!",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<IEnumerable<DataResponseUser>>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không đủ quyền hạn để sử dụng chức năng này!",
                        Data = null
                    };
                }
                var allUser = await _baseUserRepository.GetAllAsync(x=>x.TeamId==teamId);
                var allUserDTO = new List<DataResponseUser>();
                if (allUser == null)
                {
                    return new ResponseObject<IEnumerable<DataResponseUser>>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Danh sách trống!",
                        Data = null
                    };
                }
                foreach (var user in allUser)
                {
                    allUserDTO.Add(await _userConverter.EntityDTO(user));
                }
                var reversedList = allUserDTO.Reverse<DataResponseUser>().ToList();
                return new ResponseObject<IEnumerable<DataResponseUser>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Lấy danh sách thành công!",
                    Data = reversedList
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<IEnumerable<DataResponseUser>>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error:" + ex.StackTrace,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<DataResponseUser>>GetUserById(int userId)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực!",
                        Data = null
                    };
                }
                var user = await _baseUserRepository.GetByIdAsync(userId);
                var userDTO = await _userConverter.EntityDTO(user);
                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Thực hiện thao tác thành công",
                    Data = userDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseUser> 
                { 
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}
