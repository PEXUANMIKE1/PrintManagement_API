using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.Mapper;
using PrintManagerment_API.Application.Payload.Mappers;
using PrintManagerment_API.Application.Payload.RequestModels.TeamRequests;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataTeams;
using PrintManagerment_API.Application.Payload.ResponseModels.DataUsers;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.ImplementServices
{
    public class TeamService : ITeamService
    {
        private readonly TeamConverter _converter;
        private readonly IUserRepository _userRepository;
        private readonly IBaseRepository<Team> _baseTeamRepository;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IHttpContextAccessor _contextAccessor;


        public TeamService(TeamConverter converter, IBaseRepository<Team> baseTeamRepository,
            IHttpContextAccessor contextAccessor, IBaseRepository<User> baseUserRepository, IUserRepository userRepository)
        {
            _converter = converter;
            _baseTeamRepository = baseTeamRepository;
            _contextAccessor = contextAccessor;
            _baseUserRepository = baseUserRepository;
            _userRepository = userRepository;
        }

        public async Task<ResponseObject<IEnumerable<DataResponseTeam>>> GetAllTeam()
        {
            try
            {
                var currentUser = _contextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<IEnumerable<DataResponseTeam>>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực!",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<IEnumerable<DataResponseTeam>>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không đủ quyền hạn để sử dụng chức năng này!",
                        Data = null
                    };
                }
                var teams = await _baseTeamRepository.GetAllAsync();
                var teamsDTO = new List<DataResponseTeam>();
                foreach (var item in teams)
                {
                    teamsDTO.Add(await _converter.EntityDTO(item));
                }
                var teamsDTORevert = teamsDTO.Reverse<DataResponseTeam>().ToList();
                return new ResponseObject<IEnumerable<DataResponseTeam>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Thực hiện thao tác thành công",
                    Data = teamsDTORevert
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<IEnumerable<DataResponseTeam>>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<ResponseObject<DataResponseTeam>> CreateTeam(Request_Team request)
        {
            try
            {
                var currentUser = _contextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực!",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không đủ quyền hạn để sử dụng chức năng này!",
                        Data = null
                    };
                }
                if (await _baseTeamRepository.GetAsync(x=>x.Name.ToLower().Equals(request.Name.ToLower())) != null)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Tên phòng ban đã tồn tại!",
                        Data = null
                    };
                }
                if (request.Name.Trim().IsNullOrEmpty()|| request.Description.Trim().IsNullOrEmpty())
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Yêu cầu nhập dữ liệu!",
                        Data = null
                    };
                }
                Team team = new Team
                {
                    Name = request.Name,
                    Description = request.Description,
                    NumberOfMember = 0,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    ManagerId = null

                };
                await _baseTeamRepository.CreateAsync(team);
                return new ResponseObject<DataResponseTeam>
                {
                    Status = StatusCodes.Status201Created,
                    Message = "Tạo phòng ban thành công!",
                    Data = await _converter.EntityDTO(team)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseTeam>
                { 
                    Status = StatusCodes.Status400BadRequest, 
                    Message = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<ResponseObject<object>> DeleteTeamById(int teamId)
        {
            try
            {
                var currentUser = _contextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<object>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực!",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<object>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không đủ quyền hạn để sử dụng chức năng này!",
                        Data = null
                    }; 
                }
                if (await _baseTeamRepository.GetAsync(x => x.Id == teamId) == null)
                {
                    return new ResponseObject<object>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Phòng ban này đã bị xóa hoặc không tồn tại!",
                        Data = null
                    };
                }
                var listUser = await _baseUserRepository.GetAllAsync(x=>x.TeamId == teamId);

                if(listUser != null) 
                {
                    foreach (var user in listUser)
                    {
                        user.TeamId = null;
                    }
                    await _baseUserRepository.UpdateAsync(listUser);
                }

                await _baseTeamRepository.DeleteAsync(teamId);
                return new ResponseObject<object>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Xóa Phòng ban thành công",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<object>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error:" +ex.StackTrace,
                    Data = null
                };
            }
        }
        public async Task<ResponseObject<DataResponseTeam>> GetTeamById(int teamId)
        {
            try
            {
                var currentUser = _contextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực!",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không đủ quyền hạn để sử dụng chức năng này!",
                        Data = null
                    };
                }
                var team = await _baseTeamRepository.GetAsync(x => x.Id == teamId);
                if (team == null)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Phòng ban này đã bị xóa hoặc không tồn tại!",
                        Data = null
                    };
                }
                return new ResponseObject<DataResponseTeam>
                {
                    Status = StatusCodes.Status201Created,
                    Message = "Lấy dữ liệu team thành công!",
                    Data = await _converter.EntityDTO(team)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseTeam>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<ResponseObject<DataResponseTeam>> UpdateTeam(int teamId, Request_Team request)
        {
            try
            {
                var currentUser = _contextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực!",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không đủ quyền hạn để sử dụng chức năng này!",
                        Data = null
                    };
                }
                var team = await _baseTeamRepository.GetAsync(x => x.Id == teamId);
                if (team == null)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Team này đã bị xóa hoặc không tồn tại!",
                        Data = null
                    };
                }
                if (request.Name.Trim().IsNullOrEmpty() || request.Description.Trim().IsNullOrEmpty())
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Yêu cầu nhập dữ liệu!",
                        Data = null
                    };
                }
                //if(team.ManagerId.)
                team.Name = request.Name;
                team.Description = request.Description;
                team.UpdateTime = DateTime.Now;
                team.ManagerId = request.ManagerId;
                //var newManager = await _baseUserRepository.GetByIdAsync(request.ManagerId);
                //await _userRepository.AddRoleForUserAsync(newManager, new List<string> { "Manager" });
                await _baseTeamRepository.UpdateAsync(team);
                return new ResponseObject<DataResponseTeam>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Cập nhật dữ liệu thành công!",
                    Data = await _converter.EntityDTO(team)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseTeam>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<ResponseObject<object>> AddEmployeeInTeam(int userId, int? teamId)
        {
            try
            {
                var currentUser = _contextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<object>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực!",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<object>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không đủ quyền hạn để sử dụng chức năng này!",
                        Data = null
                    };
                }
                var user = await _baseUserRepository.GetByIdAsync(userId);
                if (user.TeamId == teamId)
                {
                    return new ResponseObject<object>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Nhân viên đang ở phong ban này rồi!",
                        Data = null
                    }; 
                }
                if(user.TeamId != null) 
                {
                    var teamOld = await _baseTeamRepository.GetByIdAsync(user.TeamId.Value);
                    if (teamOld.ManagerId == userId)
                    {
                        return new ResponseObject<object>
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Message = "Nhân viên đang làm quản lý phòng ban này. Không thể chuyển sang phòng khác!",
                            Data = null
                        };
                    }
                    teamOld.NumberOfMember -= 1;
                    await _baseTeamRepository.UpdateAsync(teamOld);
                    user.TeamId = null;
                    await _baseUserRepository.UpdateAsync(user);
                }
                if(teamId.HasValue)
                {
                    user.TeamId = teamId;
                    var teamNew = await _baseTeamRepository.GetByIdAsync(teamId.Value);
                    teamNew.NumberOfMember += 1;
                    await _baseTeamRepository.UpdateAsync(teamNew);
                }
                return new ResponseObject<object>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Chuyển nhân viên vào phòng ban thành công!",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<object>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }
        public async Task<ResponseObject<DataResponseTeam>> GetTeamByTeamName(string teamName)
        {
            try
            {
                var currentUser = _contextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực!",
                        Data = null
                    };
                }
                var team = await _baseTeamRepository.GetAsync(x => x.Name == teamName);
                if (team == null)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Phòng ban này đã bị xóa hoặc không tồn tại!",
                        Data = null
                    };
                }
                return new ResponseObject<DataResponseTeam>
                {
                    Status = StatusCodes.Status201Created,
                    Message = "Lấy dữ liệu team thành công!",
                    Data = await _converter.EntityDTO(team)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseTeam>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}
