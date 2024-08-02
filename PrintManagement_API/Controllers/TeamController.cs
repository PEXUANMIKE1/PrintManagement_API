using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using PrintManagerment_API.Application.Constants;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.RequestModels.TeamRequests;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataTeams;
using PrintManagerment_API.Doman.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace PrintManagement_API.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IDatabase _redisDb;
        public TeamController(ITeamService teamService, IConnectionMultiplexer redis)
        {
            _teamService = teamService;
            _redisDb = redis.GetDatabase();
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTeam()
        {
            var cacheKey = "AllTeams";
            var cachedTeams = await _redisDb.StringGetAsync(cacheKey); //tạo khóa cho dữ liệu team vào cache

            if(cachedTeams.HasValue) //nếu team đã có value trong cache từ trước thì lấy ra
            {
                var teams = JsonSerializer.Deserialize<ResponseObject<List<DataResponseTeam>>>(cachedTeams);
                return Ok(teams);
            }
            //nếu value chưa có hoặc hết hạn thì lấy ra data mới
            var result = await _teamService.GetAllTeam();
            await _redisDb.StringSetAsync(cacheKey, JsonSerializer.Serialize(result), TimeSpan.FromMinutes(30));// set time là 1 giờ sau data sẽ hết hạn
            return Ok(result);
        }
        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetTeamById(int teamId)
        {
            var cacheKey = $"Team:{teamId}";
            var cachedTeam = await _redisDb.StringGetAsync(cacheKey);

            if (cachedTeam.HasValue)
            {
                var team = JsonSerializer.Deserialize<ResponseObject<DataResponseTeam>>(cachedTeam);
                return Ok(team);
            }

            var result = await _teamService.GetTeamById(teamId);
            await _redisDb.StringSetAsync(cacheKey, JsonSerializer.Serialize(result), TimeSpan.FromMinutes(30));
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] Request_Team request)
        {
            var result = await _teamService.CreateTeam(request);
            await _redisDb.KeyDeleteAsync("AllTeams");
            return Ok(result);
        }
        [HttpPut("{teamId}")]
        public async Task<IActionResult> UpdateTeam(int teamId,[FromBody] Request_Team request)
        {
            var result = await _teamService.UpdateTeam(teamId, request);
            await _redisDb.KeyDeleteAsync($"Team:{teamId}");
            await _redisDb.KeyDeleteAsync("AllTeams");
            return Ok(result);
        }
        [HttpDelete("{teamId}")]
        public async Task<IActionResult> DeleteTeamById(int teamId)
        {
            var result = await _teamService.DeleteTeamById(teamId);
            await _redisDb.KeyDeleteAsync($"Team:{teamId}");
            await _redisDb.KeyDeleteAsync("AllTeams");
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> AddEmployeeInTeam([FromQuery] int userId, [FromQuery] int? teamId)
        {
            var result = await _teamService.AddEmployeeInTeam(userId, teamId);
            await _redisDb.KeyDeleteAsync($"Team:{teamId}");
            await _redisDb.KeyDeleteAsync("AllTeams");
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetTeamByTeamName(string teamName)
        {
            return Ok(await _teamService.GetTeamByTeamName(teamName));
        }

    }
}
