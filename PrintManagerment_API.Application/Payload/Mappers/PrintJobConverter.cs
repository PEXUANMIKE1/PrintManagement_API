using PrintManagerment_API.Application.Payload.ResponseModels.DataPrintJobs;
using PrintManagerment_API.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.Mappers
{
    public class PrintJobConverter
    {
        public DataResponsePrintJobs EntityDTO(PrintJobs printJobs)
        {
            return new DataResponsePrintJobs
            {
                Id = printJobs.Id,
                PrintJobStatus = printJobs.PrintJobStatus.ToString(),
            };
        }
    }
}
