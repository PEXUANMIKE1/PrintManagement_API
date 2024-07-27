using Microsoft.AspNetCore.Http;
using PrintManagerment_API.Application.Handle.HandleFile;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.Mapper;
using PrintManagerment_API.Application.Payload.RequestModels.UserRequests;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataTeams;
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
        private readonly IBaseRepository<Team> _baseTeamRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserConverter _userConverter;
        public UserService(IBaseRepository<User> baseUserRepository, IHttpContextAccessor contextAccessor, UserConverter userConverter, IBaseRepository<Team> baseTeamRepository)
        {
            _baseUserRepository = baseUserRepository;
            _contextAccessor = contextAccessor;
            _userConverter = userConverter;
            _baseTeamRepository = baseTeamRepository;
        }

        public async Task<ResponseObject<IEnumerable<DataResponseUser>>> GetAllMemberOfTeamNotManager(string teamName)
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
                var team = await _baseTeamRepository.GetAsync(x => x.Name == teamName);
                if (team == null)
                {
                    return new ResponseObject<IEnumerable<DataResponseUser>>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Phòng ban này đã bị xóa hoặc không tồn tại!",
                        Data = null
                    };
                }
                int idUser = int.Parse(currentUser.FindFirst("Id").Value);

                if(team.ManagerId != idUser)
                {
                    return new ResponseObject<IEnumerable<DataResponseUser>>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = $"Chỉ trưởng phòng {team.Name} mới có thể sử dụng chức năng này!",
                        Data = null
                    };
                }
                var membersOfTeamDTO = new List<DataResponseUser>();
                var membersOfTeam = await _baseUserRepository.GetAllAsync(x => x.TeamId.Value == team.Id);
                foreach (var member in membersOfTeam)
                {
                    if (member.Id != team.ManagerId)
                    {
                        membersOfTeamDTO.Add(await _userConverter.EntityDTO(member));
                    }
                }
                return new ResponseObject<IEnumerable<DataResponseUser>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Lấy dữ liệu team thành công!",
                    Data = membersOfTeamDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<IEnumerable<DataResponseUser>>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null
                };
            }
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

        public async Task<ResponseObject<DataResponseUser>> UpdateUser(Request_UpdateUser request)
        {
            try
            {
                var currentUser = _contextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực!",
                        Data = null
                    };
                }
                int userId = int.Parse(currentUser.FindFirst("Id").Value);
                var user = await _baseUserRepository.GetByIdAsync(userId);

                string oldAvatarFileName = user.Avatar;

                user.FullName = request.FullName;
                user.PhoneNumber = request.PhoneNumber;
                user.DateOfBirth = request.DateOfBirth;

                if(request.Avatar != null && request.Avatar.Length > 0)
                {
                    bool deleteResult = await HandleUploadFile.DeleteFileAsync(oldAvatarFileName);
                    if (!deleteResult)
                    {
                        // Log warning nếu không xóa được file cũ
                        Console.WriteLine($"Cảnh báo: Không thể xóa file avatar cũ: {oldAvatarFileName}");
                    }
                }
                string newAvatarFileName = await HandleUploadFile.WriteFileAsync(request.Avatar);
                if (!string.IsNullOrEmpty(newAvatarFileName))
                {
                    user.Avatar = newAvatarFileName;
                }
                else
                {
                    // Log warning nếu không lưu được file mới
                    Console.WriteLine("Cảnh báo: Không thể lưu file avatar mới");
                }

                user.UpdateTime = DateTime.Now;

                await _baseUserRepository.UpdateAsync(user);
                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Cập nhật thông tin người dùng thành công!",
                    Data = await _userConverter.EntityDTO(user)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message ="Error:" +ex.StackTrace,
                    Data = null
                };
            }
        }
    }
}
