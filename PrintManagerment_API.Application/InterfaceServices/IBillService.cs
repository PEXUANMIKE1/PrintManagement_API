using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataBill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.InterfaceServices
{
    public interface IBillService
    {
        Task<ResponseObject<DataResponseBill>> GetBillByProjectId(int projectId);
        Task<ResponseObject<DataResponseBill>> CreateBill(int projectId);
    }
}
