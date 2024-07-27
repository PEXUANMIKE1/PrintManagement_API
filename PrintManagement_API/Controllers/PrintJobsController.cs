using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintManagerment_API.Application.Constants;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.RequestModels.PrintJobsRequest;

namespace PrintManagement_API.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class PrintJobsController : ControllerBase
    {
        private readonly IPrintJobsService _printJobsService;
        public PrintJobsController(IPrintJobsService printJobsService)
        {
            _printJobsService = printJobsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPrintJobsByProjectId(int projectId)
        {
            return Ok(await _printJobsService.GetPrintJobsByProjectId(projectId));
        }
        [HttpPost]
        public async Task<IActionResult> CreatePrintJobs(int projectId)
        {
            return Ok(await _printJobsService.CreatePrintJobs(projectId));
        }
        [HttpPost]
        public async Task<IActionResult> StartPrintJobs(int projectId, [FromBody] List<Request_ConfirmPrintJobs> request)
        {
            return Ok(await _printJobsService.StartPrintJobs(projectId,request));
        }
        [HttpPut]
        public async Task<IActionResult> CompletingPrintJobs(int projectId)
        {
            return Ok(await _printJobsService.CompletingPrintJobs(projectId));
        }
    }
}
