using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.RequestModels.ProjectRequests
{
    public class Request_Project
    {
        //thông tin khách hàng yêu cầu dự án
        public string CustomerFullName { get; set; } = string.Empty;
        public string CustomerPhoneNumber { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;

        //thông tin dự án
        public string ProjectName { get; set; } = string.Empty;
        public string RequestDescriptionFromCustomer { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
    }
}
