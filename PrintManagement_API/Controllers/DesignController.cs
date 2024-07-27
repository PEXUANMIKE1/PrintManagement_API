using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintManagerment_API.Application.Constants;
using PrintManagerment_API.Application.ImplementServices;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.RequestModels.NewFolder;
using PrintManagerment_API.Application.Payload.RequestModels.UserRequests;

namespace PrintManagement_API.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class DesignController : ControllerBase
    {
        private readonly IDesignService _designService;
        public DesignController(IDesignService designService)
        {
            _designService = designService;
        }
        [HttpPost]
        public async Task<IActionResult> AddDesignInProject([FromForm] Request_Design request)
        {
            return Ok(await _designService.AddDesignInProject(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetDesignsOfProject(int projectId)
        {
            return Ok(await _designService.GetDesignsOfProject(projectId));
        }
        [HttpPut]
        public async Task<IActionResult> ApproveDesign(int projectId ,int designId, string action )
        {
            return Ok(await _designService.ApproveDesign(projectId, designId,action));
        }
    }
}
