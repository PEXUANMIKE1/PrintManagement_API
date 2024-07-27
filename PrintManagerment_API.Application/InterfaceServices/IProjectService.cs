using PrintManagerment_API.Application.Payload.RequestModels.ProjectRequests;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataCustomer;
using PrintManagerment_API.Application.Payload.ResponseModels.DataDesign;
using PrintManagerment_API.Application.Payload.ResponseModels.DataProject;
using PrintManagerment_API.Application.Payload.ResponseModels.DataUsers;
using PrintManagerment_API.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.InterfaceServices
{
    public interface IProjectService
    {
        Task<ResponseObject<DataResponseProject>> CreateProject(Request_Project request);
        Task<ResponseObject<IEnumerable<DataResponseProject>>> GetAllProject();//admin
        Task<ResponseObject<IEnumerable<DataResponseProject>>> GetAllProjectOfUser();//chỉ lấy danh sách dự án của người tạo
        Task<ResponseObject<DataResponseProject>> GetProjectById(int projectId);//
        Task<ResponseObject<DataResponseCustomer>> GetCustomerOfProject(int projectId);//
        Task<ResponseObject<DataResponseEmployee>> GetEmployeeOfProject(int projectId);//
    }
}
