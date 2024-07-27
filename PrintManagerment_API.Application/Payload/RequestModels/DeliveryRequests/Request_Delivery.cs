using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.RequestModels.DeliveryRequests
{
    public class Request_Delivery
    {
        public int DeliverId { get; set; } //id nhân viên giao hàng
        public int ProjectId { get; set; }
        public DateTime EstimateDeliveryTime { get; set; } //thời gian giao dự kiến
    }
}
