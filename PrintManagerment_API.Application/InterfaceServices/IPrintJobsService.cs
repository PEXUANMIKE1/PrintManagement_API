using PrintManagerment_API.Application.Payload.RequestModels.PrintJobsRequest;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataPrintJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.InterfaceServices
{
    public interface IPrintJobsService
    {
        Task<ResponseObject<DataResponsePrintJobs>> CreatePrintJobs(int projectId);
        Task<ResponseObject<DataResponsePrintJobs>> GetPrintJobsByProjectId(int projectId);
        Task<ResponseObject<string>> StartPrintJobs(int projectId, List<Request_ConfirmPrintJobs> request);
        Task<ResponseObject<string>> CompletingPrintJobs(int projectId);
    }
}
