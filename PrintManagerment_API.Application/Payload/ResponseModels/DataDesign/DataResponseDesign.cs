using PrintManagerment_API.Doman.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.ResponseModels.DataDesign
{
    public class DataResponseDesign : DataResponseBase
    {
        public string ProjectName { get; set; } = string.Empty;
        public string DesignerName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime DesignTime { get; set; }
        public string DesignStatus { get; set; } = string.Empty;
        public string ApproverName { get; set; } = string.Empty;
    }
}
