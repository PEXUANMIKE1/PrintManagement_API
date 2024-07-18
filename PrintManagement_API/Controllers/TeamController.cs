using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using PrintManagerment_API.Application.Constants;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.RequestModels.TeamRequests;
using PrintManagerment_API.Doman.Entities;

namespace PrintManagement_API.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTeam()
        {
            return Ok(await _teamService.GetAllTeam());
        }
        [HttpGet]
        public async Task<IActionResult> GetTeamById(int teamId)
        {
            return Ok(await _teamService.GetTeamById(teamId));
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] Request_Team request)
        {
            return Ok(await _teamService.CreateTeam(request));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTeam(int teamId,[FromBody] Request_Team request)
        {
            return Ok(await _teamService.UpdateTeam(teamId, request));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTeamById(int teamId)
        {
            return Ok(await _teamService.DeleteTeamById(teamId));
        }
        [HttpPut]
        public async Task<IActionResult> AddEmployeeInTeam(int userId, int teamId)
        {
            return Ok(await _teamService.AddEmployeeInTeam(userId,teamId));
        }
    }
}
