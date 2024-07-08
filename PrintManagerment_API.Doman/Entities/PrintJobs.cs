using PrintManagerment_API.Doman.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class PrintJobs:BaseEnities
    {
        public int DesignId { get; set; }
        public Design? Design { get; set; }
        public ConstantEnums.PrintJobStatus PrintJobStatus { get; set; } = ConstantEnums.PrintJobStatus.Pending;
        public virtual ICollection<ResourceForPrintJob>? ResourceForPrintJobs { get; set; }
    }
}
