using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using PrintManagerment_API.Application.Handle.HandleFile;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.Mappers;
using PrintManagerment_API.Application.Payload.RequestModels.NewFolder;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataDesign;
using PrintManagerment_API.Application.Payload.ResponseModels.DataProject;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.ImplementServices
{
    public class DesignService : IDesignService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IBaseRepository<Team> _baseTeamRepository;
        private readonly IBaseRepository<Project> _baseProjectRepository;
        private readonly IBaseRepository<Design> _baseDesignRepository;
        private readonly DesignConverter _designConverter; 
        private readonly IBaseRepository<Notification> _baseNotificationRepository;
        private readonly INotificationService _notificationService;
        public DesignService(IHttpContextAccessor httpContextAccessor, IBaseRepository<User> baseUserRepository, IBaseRepository<Team> baseTeamRepository,
            IBaseRepository<Project> baseProjectRepository, IBaseRepository<Design> baseDesignRepository, DesignConverter designConverter,
            IBaseRepository<Notification> baseNotificationRepository, INotificationService notificationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _baseUserRepository = baseUserRepository;
            _baseTeamRepository = baseTeamRepository;
            _baseProjectRepository = baseProjectRepository;
            _baseDesignRepository = baseDesignRepository;
            _designConverter = designConverter;
            _baseNotificationRepository = baseNotificationRepository;
            _notificationService = notificationService;
        }

        public async Task<ResponseObject<DataResponseDesign>> AddDesignInProject(Request_Design request)
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                int idUser = int.Parse(currentUser.FindFirst("Id").Value);
                var user = await _baseUserRepository.GetByIdAsync(idUser);
                var teamOfUser = await _baseTeamRepository.GetByIdAsync(user.TeamId.Value);
                if (!(currentUser.IsInRole("Designer") && teamOfUser.Name.Equals("Technical")))
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền sử dụng chức năng này!",
                        Data = null
                    };
                }
                var project = await _baseProjectRepository.GetByIdAsync(request.ProjectId);
                if (project == null)
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy dự án hoặc đã bị xóa!",
                        Data = null
                    };
                }
                var design = await _baseDesignRepository.GetAsync(x=>x.ProjectId == request.ProjectId);
                if(design != null)
                {
                    if(design.DesignStatus == Doman.Enumerates.ConstantEnums.DesignStatus.Approved)
                    {
                        return new ResponseObject<DataResponseDesign>
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Message = "Đã có thiết kế được duyệt. Bạn không thể thêm thiết kế mới!",
                            Data = null
                        };
                    }
                }
                if (request.FilePath == null)
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Bạn chưa chọn File ảnh!",
                        Data = null
                    };
                }
                //upload file design
                string filePathDesign = await HandleUploadFile.WriteFileAsync(request.FilePath);

                Design designRes = new Design
                {
                    ProjectId = request.ProjectId,
                    DesginerId = idUser,
                    DesignTime = DateTime.Now,
                    FilePath = filePathDesign,
                    DesignStatus = Doman.Enumerates.ConstantEnums.DesignStatus.NotYetApproved,
                    ApproverId = project.EmployeeId
                };
                await _baseDesignRepository.CreateAsync(designRes);
                return new ResponseObject<DataResponseDesign>
                {
                    Status = StatusCodes.Status201Created,
                    Message = "Thêm thiết kế thành công!",
                    Data = await _designConverter.EntityDTO(designRes)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseDesign>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error:" + ex.StackTrace,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<IEnumerable<DataResponseDesign>>> GetDesignsOfProject(int projectId)
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<IEnumerable<DataResponseDesign>>
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
                    var teamOfUser = await _baseTeamRepository.GetByIdAsync(user.TeamId.Value);
                    if (!(currentUser.IsInRole("Designer") && teamOfUser.Name.Equals("Technical")))
                    {
                        if(!(currentUser.IsInRole("Employee") && teamOfUser.Name.Equals("Sales")))
                        {
                            if (!teamOfUser.Name.Equals("Delivery"))
                            {
                                return new ResponseObject<IEnumerable<DataResponseDesign>>
                                {
                                    Status = StatusCodes.Status403Forbidden,
                                    Message = "Bạn không có quyền sử dụng chức năng này!",
                                    Data = null
                                };
                            }
                        }
                    }
                }
                var designs = await _baseDesignRepository.GetAllAsync(x=>x.ProjectId == projectId);
                if(designs.Count() == 0)
                {
                    return new ResponseObject<IEnumerable<DataResponseDesign>>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Danh sách trống!",
                        Data = null
                    };
                }
                var designsDTO = new List<DataResponseDesign>();
                foreach (var design in designs)
                {
                    designsDTO.Add(await _designConverter.EntityDTO(design));
                }
                var revertDesigns = designsDTO.Reverse<DataResponseDesign>().ToList();
                return new ResponseObject<IEnumerable<DataResponseDesign>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Lấy danh sách thành công!",
                    Data = revertDesigns
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<IEnumerable<DataResponseDesign>>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error:" + ex.StackTrace,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<DataResponseDesign>> ApproveDesign(int projectId,int designId, string action)
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                int idUser = int.Parse(currentUser.FindFirst("Id").Value);
                var user = await _baseUserRepository.GetByIdAsync(idUser);
                var teamOfUser = await _baseTeamRepository.GetByIdAsync(user.TeamId.Value);
                if (!(currentUser.IsInRole("Employee") && teamOfUser.Name.Equals("Sales")))
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Chỉ có quản lý dự án này mới có thể sử dụng chức năng này!",
                        Data = null
                    };
                }
                var project = await _baseProjectRepository.GetByIdAsync(projectId);
                if (project.EmployeeId != idUser)
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Chỉ có quản lý dự án này mới có thể sử dụng chức năng này!",
                        Data = null
                    };
                }
                var design = await _baseDesignRepository.GetByIdAsync(designId);
                if(design == null)
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy bản thiết kế hoặc đã bị xóa!",
                        Data = null
                    };
                }
                if(design.DesignStatus == Doman.Enumerates.ConstantEnums.DesignStatus.Rejected)
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Thiết kế này đã bị từ chối, không thể thao tác tiếp!",
                        Data = null
                    };
                }
                if (design.DesignStatus == Doman.Enumerates.ConstantEnums.DesignStatus.Approved)
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Thiết kế này đã được duyệt, không thể thao tác tiếp!",
                        Data = null
                    };
                }

                if (action.Equals("Approve"))
                { //phê duyệt
                    design.DesignStatus = Doman.Enumerates.ConstantEnums.DesignStatus.Approved;
                    await _notificationService.Notification(design.DesginerId, "Thiết kế của bạn đã được duyệt", $"/projects-design/{projectId}");
                    await _baseDesignRepository.UpdateAsync(design);
                    project.ProjectStatus = Doman.Enumerates.ConstantEnums.ProjectStatus.Designed;
                    await _baseProjectRepository.UpdateAsync(project);

                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status200OK,
                        Message = "Duyệt thiết kế thành công!",
                        Data = null
                    };
                }
                else if (action.Equals("Reject"))
                { //từ chối
                    design.DesignStatus = Doman.Enumerates.ConstantEnums.DesignStatus.Rejected;
                    await _notificationService.Notification(design.DesginerId, "Thiết kế của bạn đã bị từ chối", $"/projects-design/{projectId}");
                    await _baseDesignRepository.UpdateAsync(design);
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status200OK,
                        Message = "Từ chối thiết kế thành công!",
                        Data = null
                    };
                }
                else
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Sai dữ liệu!",
                        Data = null
                    };
                }
                
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseDesign>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error:" + ex.StackTrace,
                    Data = null
                };
            }
        }
    }
}
