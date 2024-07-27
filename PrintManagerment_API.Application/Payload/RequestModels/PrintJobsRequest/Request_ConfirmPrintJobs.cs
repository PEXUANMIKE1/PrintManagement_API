using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.RequestModels.PrintJobsRequest
{
    public class Request_ConfirmPrintJobs
    {
        public int Id {  get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ResourceName { get; set; }
    }
}
