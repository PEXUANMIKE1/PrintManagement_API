using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.ResponseModels.DataProject
{
    public class DataResponseProject
    {
        public string ProjectName { get; set; } = string.Empty;
        public string RequestDescriptionFromCustomer { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string ProjectStatus { get; set; } = string.Empty;
    }
}
