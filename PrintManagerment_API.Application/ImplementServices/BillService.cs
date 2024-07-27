using Microsoft.AspNetCore.Http;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.Mappers;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataBill;
using PrintManagerment_API.Application.Payload.ResponseModels.DataDesign;
using PrintManagerment_API.Application.Payload.ResponseModels.DataUsers;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Collections.Specialized.BitVector32;

namespace PrintManagerment_API.Application.ImplementServices
{
    public class BillService : IBillService
    {
        private readonly IBaseRepository<Bill> _baseBillRepository;
        private readonly IBaseRepository<Project> _baseProjectRepository;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IBaseRepository<Team> _baseTeamRepository;
        private readonly BillConverter _billConverter;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BillService(IBaseRepository<Bill> baseBillRepository, IBaseRepository<Project> baseProjectRepository,
            BillConverter billConverter, IHttpContextAccessor httpContextAccessor, IBaseRepository<User> baseUserRepository,
            IBaseRepository<Team> baseTeamRepository)
        {
            _baseBillRepository = baseBillRepository;
            _baseProjectRepository = baseProjectRepository;
            _billConverter = billConverter;
            _httpContextAccessor = httpContextAccessor;
            _baseUserRepository = baseUserRepository;
            _baseTeamRepository = baseTeamRepository;
        }


        //tạo mã đơn hàng(trading code) 32 số
        private string GenerateTradingCode()
        {
            return Guid.NewGuid().ToString();
        }

        public async Task<ResponseObject<DataResponseBill>>CreateBill(int projectId)
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseBill>
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
                    return new ResponseObject<DataResponseBill>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Chỉ có quản lý dự án này mới có thể sử dụng chức năng này!",
                        Data = null
                    };
                }
                var project = await _baseProjectRepository.GetByIdAsync(projectId);
                if (project.EmployeeId != idUser)
                {
                    return new ResponseObject<DataResponseBill>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Chỉ có quản lý dự án này mới có thể sử dụng chức năng này!",
                        Data = null
                    };
                }
                if (await _baseBillRepository.GetAsync(x => x.ProjectId == projectId) != null)
                {
                    return new ResponseObject<DataResponseBill>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Hóa đơn của dự án đã được tạo rồi. Bạn không thể tạo thêm hóa đơn!",
                        Data = null
                    };
                }
                Bill bill = new Bill
                {
                    BillName = project.ProjectName,
                    BillStatus = Doman.Enumerates.ConstantEnums.BillStatus.Pending,
                    TotalMoney = 0,
                    ProjectId = projectId,
                    CustomerId = project.CustomerId,
                    EmployeeId = project.EmployeeId,
                    TradingCode = GenerateTradingCode(),
                    CreateTime = project.StartDate,
                    UpdateTime = DateTime.Now,
                };
                await _baseBillRepository.CreateAsync(bill);
                return new ResponseObject<DataResponseBill>
                {
                    Status = StatusCodes.Status201Created,
                    Message = "Tạo hóa đơn thành công!",
                    Data = await _billConverter.EntityDTO(bill)
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseBill>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error:" + ex.StackTrace,
                    Data = null
                };
            }

        }

        public async Task<ResponseObject<DataResponseBill>>GetBillByProjectId(int projectId)
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseBill>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                var bill = await _baseBillRepository.GetAsync(x => x.ProjectId == projectId);
                if(bill == null)
                {
                    return new ResponseObject<DataResponseBill>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Hóa đơn không tồn tại!",
                        Data = null
                    };
                }
                return new ResponseObject<DataResponseBill>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Lấy dữ liệu hóa đơn thành công!",
                    Data = await _billConverter.EntityDTO(bill)
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseBill>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error:" + ex.StackTrace,
                    Data = null
                };
            }

        }
    
        
    }
}
