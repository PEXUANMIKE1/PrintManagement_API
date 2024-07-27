using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintManagerment_API.Application.Constants;
using PrintManagerment_API.Application.InterfaceServices;

namespace PrintManagement_API.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;
        public BillController(IBillService billService)
        {
            _billService = billService;
        }
        [HttpGet]
        public async Task<IActionResult> GetBillByProjectId(int projectId)
        {
            return Ok(await _billService.GetBillByProjectId(projectId));
        }
        [HttpPost]
        public async Task<IActionResult> CreateBill(int projectId)
        {
            return Ok(await _billService.CreateBill(projectId));
        }
    }
}
