using PrintManagerment_API.Application.Payload.RequestModels.UserRequests;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.InterfaceServices
{
    public interface IUserService
    {
        Task<ResponseObject<DataResponseUser>>GetUserById(int userId);
        Task<ResponseObject<IEnumerable<DataResponseUser>>>GetAllUser();
        Task<ResponseObject<IEnumerable<DataResponseUser>>>GetAllUserOfTeam(int teamId);
    }
}
