using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using PrintManagerment_API.Application.Handle.HandleEmail;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.Mappers;
using PrintManagerment_API.Application.Payload.RequestModels.DeliveryRequests;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataBill;
using PrintManagerment_API.Application.Payload.ResponseModels.DataDelivery;
using PrintManagerment_API.Application.Payload.ResponseModels.DataDesign;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.ImplementServices
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IBaseRepository<Delivery> _baseDeliveryRepository;
        private readonly IBaseRepository<Project> _baseProjectRepository;
        private readonly IBaseRepository<Customer> _baseCustomerRepository;
        private readonly INotificationService _notificationService;
        private readonly IBaseRepository<Team> _baseTeamRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DeliveryConverter _deliveryConverter;
        private readonly BillDeliveryConverter _billDeliveryConverter;
        private readonly IEmailService _emailService;
        public DeliveryService(IBaseRepository<Delivery> baseDeliveryRepository, IBaseRepository<Project> baseProjectRepository,
            IHttpContextAccessor httpContextAccessor, IBaseRepository<Team> baseTeamRepository, IEmailService emailService,
            INotificationService notificationService, IBaseRepository<Customer> baseCustomerRepository,DeliveryConverter deliveryConverter,
            BillDeliveryConverter billDeliveryConverter)
        {
            _baseDeliveryRepository = baseDeliveryRepository;
            _baseProjectRepository = baseProjectRepository;
            _httpContextAccessor = httpContextAccessor;
            _baseTeamRepository = baseTeamRepository;
            _baseCustomerRepository = baseCustomerRepository;
            _deliveryConverter = deliveryConverter;
            _billDeliveryConverter = billDeliveryConverter;
            _emailService = emailService;
            _notificationService = notificationService;
        }
        public async Task<ResponseObject<DataResponseDelivery>> CreateDelivery(Request_Delivery request)
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                int idUser = int.Parse(currentUser.FindFirst("Id").Value);
                var team = await _baseTeamRepository.GetAsync(x=>x.Name.Equals("Delivery"));
                if (team.ManagerId != idUser)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Chỉ có trưởng phòng vận chuyển mới có thể sử dụng chức năng này!",
                        Data = null
                    };
                }
                var project = await _baseProjectRepository.GetByIdAsync(request.ProjectId);
                if (project == null)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Dự án không tồn tại hoặc bị xóa!",
                        Data = null
                    };
                }
                if (project.ProjectStatus != Doman.Enumerates.ConstantEnums.ProjectStatus.Completed)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Sản phẩm chưa hoàn thành, không thể giao hàng!",
                        Data = null
                    };
                }
                var customer = await _baseCustomerRepository.GetByIdAsync(project.CustomerId);
                Delivery delivery = new Delivery
                {
                    ShippingMethodId = 1,
                    CustomerId = project.CustomerId,
                    DeliverId = request.DeliverId,
                    ProjectId = request.ProjectId,
                    DeliveryAddress = customer.Address,
                    EstimateDeliveryTime = request.EstimateDeliveryTime,
                    ActualDeliveryTime = null,
                    DeliveryStatus = Doman.Enumerates.ConstantEnums.DeliveryStatus.Pending
                };
                await _baseDeliveryRepository.CreateAsync(delivery);
                return new ResponseObject<DataResponseDelivery>
                {
                    Status = StatusCodes.Status201Created,
                    Message = "Tạo đơn giao hàng thành công!",
                    Data = await _deliveryConverter.EntityDTO(delivery)
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseDelivery>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error:" + ex.StackTrace,
                    Data = null
                };
            }

        }

        public Task<ResponseObject<IEnumerable<DataResponseDelivery>>> getAllDelivery()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObject<DataResponseDelivery>> getDeliveryById(int deliveryId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseObject<DataResponseBillDelivery>> GetBillDeliveryById(int deliveryId)
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseBillDelivery>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                var idUser = int.Parse(currentUser.FindFirst("Id").Value);
                var teamOfUser = currentUser.FindFirst("Team").Value;
                if (currentUser.IsInRole("Admin") || teamOfUser.Equals("Delivery"))
                {
                    var billDeliverys = await _baseDeliveryRepository.GetByIdAsync(deliveryId);

                    return new ResponseObject<DataResponseBillDelivery>
                    {
                        Status = StatusCodes.Status200OK,
                        Message = "Lấy danh sách hóa đơn vận chuyển thành công!",
                        Data = await _billDeliveryConverter.EntityDTO(billDeliverys)
                    };
                }
                else
                {
                    return new ResponseObject<DataResponseBillDelivery>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền sử dụng chức năng này!",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseBillDelivery>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error:" + ex.StackTrace,
                    Data = null
                };
            }

        }

        public async Task<ResponseObject<DataResponseDelivery>> getDeliveryByProjectId(int projectId)
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                var project = await _baseProjectRepository.GetByIdAsync(projectId);
                if (project == null)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Dự án không tồn tại hoặc bị xóa!",
                        Data = null
                    };
                }
                var delivery = await _baseDeliveryRepository.GetAsync(x => x.ProjectId == projectId);
                if(delivery == null)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Đơn giao hàng không tồn tại!",
                        Data = null
                    };
                }
                return new ResponseObject<DataResponseDelivery>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Lấy đơn giao hàng thành công!",
                    Data = await _deliveryConverter.EntityDTO(delivery)
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseDelivery>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error:" + ex.StackTrace,
                    Data = null
                };
            }

        }

        public async Task<ResponseObject<IEnumerable<DataResponseDelivery>>> getDeliveryOfUser()
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<IEnumerable<DataResponseDelivery>>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                var idUser = int.Parse(currentUser.FindFirst("Id").Value);
                var team = await _baseTeamRepository.GetAsync(x => x.Name.Equals("Delivery"));
                if (currentUser.IsInRole("Admin") || team.ManagerId == idUser)
                {
                    var Alldelivery = await _baseDeliveryRepository.GetAllAsync();
                    var AlldeliveryDTO = new List<DataResponseDelivery>();
                    foreach (var item in Alldelivery)
                    {
                        AlldeliveryDTO.Add(await _deliveryConverter.EntityDTO(item));
                    }

                    return new ResponseObject<IEnumerable<DataResponseDelivery>>
                    {
                        Status = StatusCodes.Status200OK,
                        Message = "Lấy danh sách hóa đơn vận chuyển thành công!",
                        Data = AlldeliveryDTO.OrderByDescending(x => x.Id)
                    };
                }
                var deliverys = await _baseDeliveryRepository.GetAllAsync(x => x.DeliverId == idUser);
                if (deliverys == null)
                {
                    return new ResponseObject<IEnumerable<DataResponseDelivery>>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Danh sách trống!",
                        Data = null
                    };
                }
                var billdelivery = new List<DataResponseDelivery>();
                foreach (var delivery in deliverys)
                {
                    billdelivery.Add(await _deliveryConverter.EntityDTO(delivery));
                }

                return new ResponseObject<IEnumerable<DataResponseDelivery>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Lấy danh sách hóa đơn vận chuyển thành công!",
                    Data = billdelivery.OrderByDescending(x => x.Id)
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<IEnumerable<DataResponseDelivery>>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error:" + ex.StackTrace,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<string>> UpdateStatusDelivery(int deliveryId, string status)
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
                var idUser = int.Parse(currentUser.FindFirst("Id").Value);
                var teamUser = currentUser.FindFirst("Team").Value;
                var team = await _baseTeamRepository.GetAsync(x => x.Name.Equals("Delivery"));
                if (team.ManagerId.Value == idUser || !teamUser.Equals("Delivery"))
                {
                    return new ResponseObject<string>
                    {
                        Status = StatusCodes.Status200OK,
                        Message = "Bạn không có quyền thực hiện chức năng này!",
                        Data = null
                    };
                }
                var delivery = await _baseDeliveryRepository.GetByIdAsync(deliveryId);
                var msg = "";
                var project = await _baseProjectRepository.GetByIdAsync(delivery.ProjectId);
                if (status.Equals("Xác nhận giao hàng"))
                {
                    delivery.DeliveryStatus = Doman.Enumerates.ConstantEnums.DeliveryStatus.InTransit;
                    await _baseDeliveryRepository.UpdateAsync(delivery);
                    msg = "Đơn hàng đang trên đường giao đến tay bạn.";
                }
                else if (status.Equals("Đã trả lại"))
                {
                    delivery.DeliveryStatus = Doman.Enumerates.ConstantEnums.DeliveryStatus.Returned;
                    delivery.ActualDeliveryTime = DateTime.Now;
                    await _baseDeliveryRepository.UpdateAsync(delivery);
                    msg = "Giao hàng không thành công. Khách hàng trả lại đơn!";
                    await _notificationService.Notification(team.ManagerId.Value, msg, $"/shipping-management");
                    await _notificationService.Notification(project.EmployeeId,msg, $"/shipping-management");
                }
                else if (status.Equals("Từ chối nhận"))
                {
                    delivery.DeliveryStatus = Doman.Enumerates.ConstantEnums.DeliveryStatus.Refused;
                    delivery.ActualDeliveryTime = DateTime.Now;
                    await _baseDeliveryRepository.UpdateAsync(delivery);
                    msg = "Giao hàng không thành công. Khách hàng từ chối nhận đơn!";
                    await _notificationService.Notification(team.ManagerId.Value,msg, $"/shipping-management");
                    await _notificationService.Notification(project.EmployeeId, msg, $"/shipping-management");
                }
                else if (status.Equals("Đã giao hàng"))
                {
                    delivery.DeliveryStatus = Doman.Enumerates.ConstantEnums.DeliveryStatus.Delivered;
                    delivery.ActualDeliveryTime = DateTime.Now;
                    await _baseDeliveryRepository.UpdateAsync(delivery);
                    msg = "Giao hàng thành công.";
                    await _notificationService.Notification(team.ManagerId.Value, msg, $"/shipping-management");
                    await _notificationService.Notification(project.EmployeeId, msg, $"/shipping-management");
                }
                var customer = await _baseCustomerRepository.GetByIdAsync(delivery.CustomerId);
                var message = new EmailMessage(new string[] { customer.Email }, "Thông báo thông tin đơn hàng", msg);
                var responseMessage = _emailService.SendEmail(message);
                return new ResponseObject<string>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Thực hiện thao tác thành công!",
                    Data = null
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error:" + ex.StackTrace,
                    Data = null
                };
            }
        }
    }
}
