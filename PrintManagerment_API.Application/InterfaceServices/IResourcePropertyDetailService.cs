using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataResourcePropertyDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.InterfaceServices
{
    public interface IResourcePropertyDetailService
    {
        Task<ResponseObject<IEnumerable<DataResResourcePropertyDetail>>> GetAll();
    }
}
