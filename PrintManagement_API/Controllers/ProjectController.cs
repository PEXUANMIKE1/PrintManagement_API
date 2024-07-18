using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintManagerment_API.Application.Constants;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.RequestModels.ProjectRequests;

namespace PrintManagement_API.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] Request_Project request)
        {
            return Ok(await _projectService.CreateProject(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProject()
        {
            return Ok(await _projectService.GetAllProject());
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProjectOfUser()
        {
            return Ok(await _projectService.GetAllProjectOfUser());
        }
    }
}
