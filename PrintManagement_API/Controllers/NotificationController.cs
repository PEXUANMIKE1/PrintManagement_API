using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintManagerment_API.Application.Constants;
using PrintManagerment_API.Application.InterfaceServices;

namespace PrintManagement_API.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllNotifyOfUser()
        {
            return Ok(await _notificationService.GetAllNotifyOfUser());
        }
        [HttpPut]
        public async Task<IActionResult> MarkAsSeenById(int notifyId)
        {
            return Ok(await _notificationService.MarkAsSeenById(notifyId));
        }
        [HttpPut]
        public async Task<IActionResult> MarkAllAsSeenOfUser()
        {
            return Ok(await _notificationService.MarkAllAsSeenOfUser());
        }
    }
}
