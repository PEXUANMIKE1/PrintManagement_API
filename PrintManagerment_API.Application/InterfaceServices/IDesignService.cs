using PrintManagerment_API.Application.Payload.RequestModels.NewFolder;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.InterfaceServices
{
    public interface IDesignService
    {
        Task<ResponseObject<DataResponseDesign>> AddDesignInProject(Request_Design request);
        Task<ResponseObject<IEnumerable<DataResponseDesign>>> GetDesignsOfProject(int projectId);
        Task<ResponseObject<DataResponseDesign>> ApproveDesign(int projectId,int designId, string action);//có 2 lựa chọn action là Approve và Reject
    }
}
