using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.ResponseModels.DataNotification
{
    public class DataResponseNotify : DataResponseBase
    {
        public string Content { get; set; }
        public string Link { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsSeen { get; set; }
    }
}
