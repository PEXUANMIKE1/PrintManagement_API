using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.ResponseModels.DataDelivery
{
    public class DataResponseBillDelivery 
    {
        //data response Bill
        public int BillId {  get; set; }
        public string BillName { get; set; }
        public decimal TotalMoney { get; set; }
        public string TradingCode { get; set; }
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }
        public string SDT { get; set; }


        //data response Delivery
        public int DeliveryId { get; set; }
        public string ShippingMethodName { get; set; }
        public string DeliverName { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime EstimateDeliveryTime { get; set; } //thời gian giao dự kiến
        public DateTime? ActualDeliveryTime { get; set; } //thời gian giao thực tế
        public string DeliveryStatus { get; set; }
    }
}
