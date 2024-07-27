using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.ResponseModels.DataBill
{
    public class DataResponseBill : DataResponseBase
    {
        public string BillName { get; set; }
        public string BillStatus { get; set;}
        public decimal TotalMoney { get; set;}
        public string TradingCode { get; set;}
        public string ProjectName { get; set;}
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime CreateTime { get; set;}
    }
}
