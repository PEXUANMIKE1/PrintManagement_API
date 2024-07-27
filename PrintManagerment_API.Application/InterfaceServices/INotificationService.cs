using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.InterfaceServices
{
    public interface INotificationService
    {
        Task Notification(int userId, string Message, string link);
        Task<ResponseObject<IEnumerable<DataResponseNotify>>> GetAllNotifyOfUser();
        Task<ResponseObject<string>> MarkAsSeenById(int notifyId);
        Task<ResponseObject<string>> MarkAllAsSeenOfUser();
    }
}
