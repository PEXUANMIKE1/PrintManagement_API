using PrintManagerment_API.Doman.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class Delivery:BaseEnities
    {
        public int ShippingMethodId { get; set; }
        public ShippingMethod? ShippingMethod { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int DeliverId { get; set; } //id nhân viên giao hàng
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public DateTime EstimateDeliveryTime { get; set; } //thời gian giao dự kiến
        public DateTime? ActualDeliveryTime { get; set; } //thời gian giao thực tế
        public ConstantEnums.DeliveryStatus DeliveryStatus { get; set; } = ConstantEnums.DeliveryStatus.Pending;
        
    }
}
