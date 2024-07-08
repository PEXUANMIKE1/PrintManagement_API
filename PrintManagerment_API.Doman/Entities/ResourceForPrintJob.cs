using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class ResourceForPrintJob:BaseEnities
    {
        public int ResourcePropertyDetailId { get; set; }
        public ResourcePropertyDetail? ResourcePropertyDetail { get; set; }
        public int PrintJobsId { get; set; }
        public PrintJobs? PrintJobs { get; set; }
    }
}
