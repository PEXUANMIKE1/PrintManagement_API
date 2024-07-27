using PrintManagerment_API.Doman.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class Design : BaseEnities
    {
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public int DesginerId {  get; set; }
        public User? Desginer { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public DateTime DesignTime { get; set; }
        public ConstantEnums.DesignStatus DesignStatus { get; set; }
        public int ApproverId { get; set; }
        public virtual ICollection<PrintJobs>? PrintJobs { get; set; }
    }
}
