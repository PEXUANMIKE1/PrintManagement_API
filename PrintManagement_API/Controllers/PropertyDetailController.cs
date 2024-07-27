using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintManagerment_API.Application.Constants;
using PrintManagerment_API.Application.ImplementServices;
using PrintManagerment_API.Application.InterfaceServices;

namespace PrintManagement_API.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class PropertyDetailController : ControllerBase
    {
        private readonly IResourcePropertyDetailService resourcePropertyDetailService;

        public PropertyDetailController(IResourcePropertyDetailService resourcePropertyDetailService)
        {
            this.resourcePropertyDetailService = resourcePropertyDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResourcePropertyDetail()
        {
            return Ok(await resourcePropertyDetailService.GetAll());
        }
    }
}
