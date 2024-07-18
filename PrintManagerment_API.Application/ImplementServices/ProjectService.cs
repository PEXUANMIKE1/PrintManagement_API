using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.Mappers;
using PrintManagerment_API.Application.Payload.RequestModels.ProjectRequests;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataProject;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PrintManagerment_API.Application.ImplementServices
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectConverter _projectConverter;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBaseRepository<Project> _baseProjectRepository;
        private readonly IBaseRepository<Customer> _baseCustomerRepository;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IBaseRepository<Team> _baseTeamRepository;

        public ProjectService(ProjectConverter projectConverter, IHttpContextAccessor httpContextAccessor, IBaseRepository<Project> baseProjectRepository, 
            IBaseRepository<User> baseUserRepository, IBaseRepository<Team> baseTeamRepository, IBaseRepository<Customer> baseCustomerRepository)
        {
            _projectConverter = projectConverter;
            _httpContextAccessor = httpContextAccessor;
            _baseProjectRepository = baseProjectRepository;
            _baseUserRepository = baseUserRepository;
            _baseTeamRepository = baseTeamRepository;
            _baseCustomerRepository = baseCustomerRepository;
        }

        public async Task<ResponseObject<DataResponseProject>> CreateProject(Request_Project request)
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseProject>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                int idUser = int.Parse(currentUser.FindFirst("Id").Value);
                var user = await _baseUserRepository.GetByIdAsync(idUser);
                if (user.TeamId == null)
                {
                    return new ResponseObject<DataResponseProject>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền sử dụng chức năng này!",
                        Data = null
                    };
                }
                var teamOfUser = await _baseTeamRepository.GetByIdAsync(user.TeamId.Value);
                if (!(currentUser.IsInRole("Employee") && teamOfUser.Name.Equals("Sales")))
                {
                    return new ResponseObject<DataResponseProject>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền sử dụng chức năng này!",
                        Data = null
                    };
                }
                if (string.IsNullOrWhiteSpace(request.CustomerFullName) ||
                    string.IsNullOrWhiteSpace(request.CustomerAddress) ||
                    string.IsNullOrWhiteSpace(request.CustomerPhoneNumber) ||
                    string.IsNullOrWhiteSpace(request.ProjectName))
                {
                    return new ResponseObject<DataResponseProject>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Yêu cầu nhập đầy đủ thông tin!",
                        Data = null
                    };
                }
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        Customer customer = new Customer
                        {
                            FullName = request.CustomerFullName,
                            Address = request.CustomerAddress,
                            PhoneNumber = request.CustomerPhoneNumber,
                        };
                        await _baseCustomerRepository.CreateAsync(customer);
                        Project project = new Project
                        {
                            ProjectName = request.ProjectName,
                            RequestDescriptionFromCustomer = request.RequestDescriptionFromCustomer,
                            StartDate = request.StartDate,
                            ExpectedEndDate = request.ExpectedEndDate,
                            EmployeeId = idUser,
                            CustomerId = customer.Id,
                            ProjectStatus = Doman.Enumerates.ConstantEnums.ProjectStatus.Designing
                        };
                        await _baseProjectRepository.CreateAsync(project);
                        scope.Complete();
                        return new ResponseObject<DataResponseProject>
                        {
                            Status = StatusCodes.Status201Created,
                            Message = "Tạo dự án thành công!",
                            Data = await _projectConverter.EntityDTO(project)
                        };
                    }
                    catch (Exception ex)
                    {
                        scope.Dispose();
                        return new ResponseObject<DataResponseProject>
                        {
                            Status = StatusCodes.Status201Created,
                            Message = "Error" +ex.StackTrace,
                            Data = null
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseProject>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error: " + ex.StackTrace,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<IEnumerable<DataResponseProject>>> GetAllProject()
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<IEnumerable<DataResponseProject>>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                int idUser = int.Parse(currentUser.FindFirst("Id").Value);
                var user = await _baseUserRepository.GetByIdAsync(idUser);
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<IEnumerable<DataResponseProject>>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền sử dụng chức năng này!",
                        Data = null
                    };
                }
                var projects = await _baseProjectRepository.GetAllAsync();
                if (projects.Count() == 0)
                {
                    return new ResponseObject<IEnumerable<DataResponseProject>>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Danh sách trống",
                        Data = null
                    };
                }
                var projectDTO = new List<DataResponseProject>();
                foreach ( var project in projects)
                {
                    projectDTO.Add(await _projectConverter.EntityDTO(project));
                }
                var projectDTORevert = projectDTO.Reverse<DataResponseProject>().ToList();
                return new ResponseObject<IEnumerable<DataResponseProject>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Lấy danh sách tất cả dự án thành công",
                    Data = projectDTORevert
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<IEnumerable<DataResponseProject>>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error: " + ex.StackTrace,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<IEnumerable<DataResponseProject>>> GetAllProjectOfUser()
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<IEnumerable<DataResponseProject>>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                int idUser = int.Parse(currentUser.FindFirst("Id").Value);
                var user = await _baseUserRepository.GetByIdAsync(idUser);
                if (user.TeamId == null)
                {
                    return new ResponseObject<IEnumerable<DataResponseProject>>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền sử dụng chức năng này!",
                        Data = null
                    };
                }
                var teamOfUser = await _baseTeamRepository.GetByIdAsync(user.TeamId.Value);
                if (!(currentUser.IsInRole("Employee") && teamOfUser.Name.Equals("Sales")))
                {
                    return new ResponseObject<IEnumerable<DataResponseProject>>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền sử dụng chức năng này!",
                        Data = null
                    };
                }
                var projects = await _baseProjectRepository.GetAllAsync(x=>x.EmployeeId == idUser);
                if (projects.Count() == 0)
                {
                    return new ResponseObject<IEnumerable<DataResponseProject>>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Danh sách trống",
                        Data = null
                    };
                }
                var projectDTO = new List<DataResponseProject>();
                foreach (var project in projects)
                {
                    projectDTO.Add(await _projectConverter.EntityDTO(project));
                }
                var projectDTORevert = projectDTO.Reverse<DataResponseProject>().ToList();
                return new ResponseObject<IEnumerable<DataResponseProject>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Lấy danh sách tất cả dự án thành công",
                    Data = projectDTORevert
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<IEnumerable<DataResponseProject>>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error: " + ex.StackTrace,
                    Data = null
                };
            }
        }
    }
}
