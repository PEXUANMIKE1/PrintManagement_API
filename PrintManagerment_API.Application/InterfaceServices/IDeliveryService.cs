using PrintManagerment_API.Application.Payload.RequestModels.DeliveryRequests;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataDelivery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.InterfaceServices
{
    public interface IDeliveryService
    {
        Task<ResponseObject<IEnumerable<DataResponseDelivery>>> getAllDelivery();
        Task<ResponseObject<DataResponseBillDelivery>> GetBillDeliveryById(int deliveryId);
        Task<ResponseObject<IEnumerable<DataResponseDelivery>>> getDeliveryOfUser();
        Task<ResponseObject<DataResponseDelivery>> getDeliveryById(int deliveryId);
        Task<ResponseObject<DataResponseDelivery>> getDeliveryByProjectId(int projectId);
        Task<ResponseObject<DataResponseDelivery>> CreateDelivery(Request_Delivery request);
        Task<ResponseObject<string>> UpdateStatusDelivery(int deliveryId, string status); //nhân viên giao hàng
    }
}
