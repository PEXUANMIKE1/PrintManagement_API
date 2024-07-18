using PrintManagerment_API.Application.Payload.RequestModels.TeamRequests;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataTeams;
using PrintManagerment_API.Application.Payload.ResponseModels.DataUsers;
using PrintManagerment_API.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.InterfaceServices
{
    public interface ITeamService
    {
        Task<ResponseObject<IEnumerable<DataResponseTeam>>> GetAllTeam();
        Task<ResponseObject<DataResponseTeam>> GetTeamById(int teamId);
        Task<ResponseObject<DataResponseTeam>> CreateTeam(Request_Team request);
        Task<ResponseObject<DataResponseTeam>> UpdateTeam(int teamId, Request_Team request);
        Task<ResponseObject<object>> DeleteTeamById(int teamId);
        Task<ResponseObject<object>> AddEmployeeInTeam(int userId, int teamId);
    }
}
