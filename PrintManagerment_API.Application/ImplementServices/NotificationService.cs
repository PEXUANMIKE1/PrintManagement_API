using Microsoft.AspNetCore.Http;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.Mappers;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataNotification;
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
    public class NotificationService : INotificationService
    {
        private readonly IBaseRepository<Notification> _baseNotificationRepository;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NotifyConverter _notifyConverter;

        public NotificationService(IBaseRepository<Notification> baseNotificationRepository, IBaseRepository<User> baseUserRepository,
            IHttpContextAccessor contextAccessor, NotifyConverter notifyConverter)
        {
            _baseNotificationRepository = baseNotificationRepository;
            _baseUserRepository = baseUserRepository;
            _httpContextAccessor = contextAccessor;
            _notifyConverter = notifyConverter;
        }

        public async Task<ResponseObject<IEnumerable<DataResponseNotify>>> GetAllNotifyOfUser()
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<IEnumerable<DataResponseNotify>>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                int idUser = int.Parse(currentUser.FindFirst("Id").Value);
                var user = await _baseUserRepository.GetByIdAsync(idUser);
                var Notifications = await _baseNotificationRepository.GetAllAsync(x => x.UserId == idUser);
                if (Notifications.Count() == 0)
                {
                    return new ResponseObject<IEnumerable<DataResponseNotify>>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Danh sách trống",
                        Data = null
                    };
                }
                var NotificationsDTO = new List<DataResponseNotify>();
                foreach (var item in Notifications)
                {
                    NotificationsDTO.Add(_notifyConverter.EntityDTO(item));
                }
                var NotificationsDTORevert = NotificationsDTO.OrderByDescending(x=>x.Id);
                return new ResponseObject<IEnumerable<DataResponseNotify>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Lấy danh sách thông báo thành công",
                    Data = NotificationsDTORevert
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<IEnumerable<DataResponseNotify>>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error: " + ex.StackTrace,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<string>> MarkAllAsSeenOfUser()
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
                var notify = await _baseNotificationRepository.GetAllAsync(x=>x.UserId == idUser);
                foreach (var item in notify)
                {
                    item.IsSeen = true;
                }
                await _baseNotificationRepository.UpdateAsync(notify);
                
                return new ResponseObject<string>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Xem tin thành công",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "error:" + ex.StackTrace,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<string>> MarkAsSeenById(int notifyId)
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
                var notify = await _baseNotificationRepository.GetByIdAsync(notifyId);
                notify.IsSeen = true;
                await _baseNotificationRepository.UpdateAsync(notify);
                return new ResponseObject<string>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Xem tin thành công",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "error:"+ex.StackTrace,
                    Data = null
                };
            }
        }

        public async Task Notification(int userId, string Message, string link)
        {
            try
            {
                var user = await _baseUserRepository.GetByIdAsync(userId);
                if (user != null)
                {
                    Notification notify = new Notification
                    {
                        UserId = userId,
                        Content = Message,
                        Link = link,
                        CreateTime = DateTime.Now,
                        IsSeen = false,
                    };
                    await _baseNotificationRepository.CreateAsync(notify);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
