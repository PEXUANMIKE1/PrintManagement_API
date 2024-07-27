using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.ResponseModels.DataPrintJobs
{
    public class DataResponsePrintJobs : DataResponseBase
    {
        public string PrintJobStatus { get; set; }
    }
}
