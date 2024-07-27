using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintManagerment_API.Application.Constants;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.RequestModels.DeliveryRequests;

namespace PrintManagement_API.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _service;
        public DeliveryController(IDeliveryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDelivery([FromBody] Request_Delivery request)
        {
            return Ok(await _service.CreateDelivery(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetBillDeliveryById(int deliveryId)
        {
            return Ok(await _service.GetBillDeliveryById(deliveryId));
        }
        [HttpGet]
        public async Task<IActionResult> getDeliveryOfUser()
        {
            return Ok(await _service.getDeliveryOfUser());
        }
        [HttpGet]
        public async Task<IActionResult> GetDeliveryByProjectId(int projectId)
        {
            return Ok(await _service.getDeliveryByProjectId(projectId));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStatusDelivery(int deliveryId, string status)
        {
            return Ok(await _service.UpdateStatusDelivery(deliveryId,status));
        }
    }
}
