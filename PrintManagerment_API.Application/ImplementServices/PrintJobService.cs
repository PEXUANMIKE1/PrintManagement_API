using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Ocsp;
using PrintManagerment_API.Application.Handle.HandleEmail;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.Mappers;
using PrintManagerment_API.Application.Payload.RequestModels.PrintJobsRequest;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataBill;
using PrintManagerment_API.Application.Payload.ResponseModels.DataPrintJobs;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.ImplementServices
{
    public class PrintJobService : IPrintJobsService
    {
        private readonly IBaseRepository<PrintJobs> _basePrintJobsRepository;
        private readonly IBaseRepository<ResourceForPrintJob> _baseResourceForPrintJobRepository;
        private readonly IBaseRepository<ResourcePropertyDetail> _baseResourcePropertyDetailRepository;
        private readonly IBaseRepository<Bill> _baseBillRepository;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IBaseRepository<Team> _baseTeamRepository;
        private readonly IBaseRepository<Design> _baseDesignRepository;
        private readonly IBaseRepository<Customer> _baseCustomerRepository;
        private readonly IBaseRepository<Project> _baseProjectRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PrintJobConverter _jobConverter;
        private readonly IEmailService _emailService;

        public PrintJobService(IBaseRepository<PrintJobs> basePrintJobsRepository, IHttpContextAccessor httpContextAccessor,
            IBaseRepository<User> baseUserRepository, IBaseRepository<Team> baseTeamRepository, IBaseRepository<Design> baseDesignRepository,
            PrintJobConverter printJobConverter, IBaseRepository<Project> baseProjectRepository,IBaseRepository<ResourceForPrintJob> baseResourceForPrintJobRepository,
            IBaseRepository<ResourcePropertyDetail> baseResourcePropertyDetailRepository, IBaseRepository<Bill> baseBillRepository, IEmailService emailService, IBaseRepository<Customer> baseCustomerRepository)
        {
            _basePrintJobsRepository = basePrintJobsRepository;
            _httpContextAccessor = httpContextAccessor;
            _baseUserRepository = baseUserRepository;
            _baseTeamRepository = baseTeamRepository;
            _baseDesignRepository = baseDesignRepository;
            _jobConverter = printJobConverter;
            _baseProjectRepository = baseProjectRepository;
            _baseResourceForPrintJobRepository = baseResourceForPrintJobRepository;
            _baseResourcePropertyDetailRepository = baseResourcePropertyDetailRepository;
            _baseBillRepository = baseBillRepository;
            _emailService = emailService;
            _baseCustomerRepository = baseCustomerRepository;
        }

        public async Task<ResponseObject<string>> StartPrintJobs(int projectId, List<Request_ConfirmPrintJobs> requests)
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<string>
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
                    return new ResponseObject<string>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Chỉ có quản lý dự án mới có thể sử dụng chức năng này!",
                        Data = null
                    };
                }
                var project = await _baseProjectRepository.GetByIdAsync(projectId);
                if (project.ProjectStatus != Doman.Enumerates.ConstantEnums.ProjectStatus.ConfirmPrint)
                {
                    return new ResponseObject<string>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Có lỗi trong quá trình xử lý!",
                        Data = null
                    };
                }
                var design = await _baseDesignRepository.GetAsync(x=>x.ProjectId == project.Id && x.DesignStatus == Doman.Enumerates.ConstantEnums.DesignStatus.Approved);
                var printjob = await _basePrintJobsRepository.GetAsync(x=>x.DesignId == design.Id);
                var resources = requests.Select(item => new ResourceForPrintJob
                {
                    ResourcePropertyDetailId = item.Id,
                    PrintJobsId = printjob.Id,
                }).ToList();
                await _baseResourceForPrintJobRepository.CreateAsync(resources);
                decimal TotalBill = 0;
                //xóa số lượng trong kho
                foreach(var item in requests)
                {
                    var propertyDetail = await _baseResourcePropertyDetailRepository.GetByIdAsync(item.Id);
                    propertyDetail.Quantity -= item.Quantity;
                    if(propertyDetail.Quantity < 0)
                    {
                        return new ResponseObject<string>
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Message = "Số lượng kho không đủ!",
                            Data = null
                        };
                    }
                    TotalBill += item.Price;
                    await _baseResourcePropertyDetailRepository.UpdateAsync(propertyDetail);
                }
                project.ProjectStatus = Doman.Enumerates.ConstantEnums.ProjectStatus.Printing;
                await _baseProjectRepository.UpdateAsync(project);

                //cập nhật tổng tiền và trạng thái bill
                var bill = await _baseBillRepository.GetAsync(x=>x.ProjectId == project.Id);
                bill.TotalMoney = TotalBill;
                await _baseBillRepository.UpdateAsync(bill);

                return new ResponseObject<string>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Gửi đơn xuống xưởng thành công. Vui lòng chờ hoàn thành in ấn!",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null
                };

            }
        }

        public async Task<ResponseObject<DataResponsePrintJobs>>CreatePrintJobs(int projectId)
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponsePrintJobs>
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
                    return new ResponseObject<DataResponsePrintJobs>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Chỉ có quản lý dự án mới có thể sử dụng chức năng này!",
                        Data = null
                    };
                }
                var project = await _baseProjectRepository.GetByIdAsync(projectId);
                if (project.ProjectStatus != Doman.Enumerates.ConstantEnums.ProjectStatus.Designed)
                {
                    return new ResponseObject<DataResponsePrintJobs>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Có lỗi trong quá trình xử lý!",
                        Data = null
                    };
                }
                var design = await _baseDesignRepository.GetAsync(x => x.ProjectId == projectId && x.DesignStatus == Doman.Enumerates.ConstantEnums.DesignStatus.Approved);
                PrintJobs printJobs = new PrintJobs
                {
                    DesignId = design.Id,
                    PrintJobStatus = Doman.Enumerates.ConstantEnums.PrintJobStatus.Pending
                };
                await _basePrintJobsRepository.CreateAsync(printJobs);

                // cập nhật trạng thái project
                project.ProjectStatus = Doman.Enumerates.ConstantEnums.ProjectStatus.ConfirmPrint;
                await _baseProjectRepository.UpdateAsync(project);

                return new ResponseObject<DataResponsePrintJobs>
                {
                    Status = StatusCodes.Status201Created,
                    Message = "Tạo đơn in ấn thành công",
                    Data = _jobConverter.EntityDTO(printJobs)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponsePrintJobs>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null
                };

            }
        }

        public async Task<ResponseObject<DataResponsePrintJobs>> GetPrintJobsByProjectId(int projectId)
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponsePrintJobs>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                var design = await _baseDesignRepository.GetAsync(x => x.ProjectId == projectId && x.DesignStatus == Doman.Enumerates.ConstantEnums.DesignStatus.Approved);
                var printJobs = await _basePrintJobsRepository.GetAsync(x => x.DesignId == design.Id);
                if (printJobs == null)
                {
                    return new ResponseObject<DataResponsePrintJobs>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Đơn in ấn chưa được tạo!",
                        Data = null
                    };
                }
                return new ResponseObject<DataResponsePrintJobs>
                {
                    Status = StatusCodes.Status201Created,
                    Message = "Lấy đơn in ấn thành công!",
                    Data = _jobConverter.EntityDTO(printJobs)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponsePrintJobs>
                { 
                   Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null
                };

            }
        }

        public async Task<ResponseObject<string>> CompletingPrintJobs(int projectId)
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<string>
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
                    return new ResponseObject<string>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Chỉ có quản lý dự án mới có thể sử dụng chức năng này!",
                        Data = null
                    };
                }
                var project = await _baseProjectRepository.GetByIdAsync(projectId);
                if (project.ProjectStatus != Doman.Enumerates.ConstantEnums.ProjectStatus.Printing)
                {
                    return new ResponseObject<string>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Có lỗi trong quá trình xử lý!",
                        Data = null
                    };
                }
                project.ProjectStatus = Doman.Enumerates.ConstantEnums.ProjectStatus.Completed;
                await _baseProjectRepository.UpdateAsync(project);

                //cập nhật tổng tiền và trạng thái bill
                var bill = await _baseBillRepository.GetAsync(x => x.ProjectId == project.Id);
                bill.BillStatus = Doman.Enumerates.ConstantEnums.BillStatus.Unpaid;
                await _baseBillRepository.UpdateAsync(bill);
                var customer = await _baseCustomerRepository.GetByIdAsync(project.CustomerId);
                var message = new EmailMessage(new string[] { customer.Email }, "Thông báo thông tin đơn hàng", $"Đơn hàng của bạn đã hoàn thành in ấn." +
                    $" Vui lòng chờ đơn vị vận chuyển giao hàng đến cho bạn!" +
                    $"\nMã đơn hàng: {bill.TradingCode}" +
                    $"\nTên đơn hàng: {bill.BillName}" +
                    $"\nTên khách hàng: {customer.FullName}" +
                    $"\nTổng tiền: {bill.TotalMoney} VNĐ\n");
                var responseMessage = _emailService.SendEmail(message);
                return new ResponseObject<string>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Hoàn thành in ấn!",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null
                };

            }
        }
    }
}
