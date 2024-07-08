using PrintManagerment_API.Doman.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class Resources:BaseEnities
    {
        public string ResourceName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int AvailableQuantity { get; set; }
        public ConstantEnums.ResourceType ResourceStatus { get; set; }
        public ConstantEnums.ResourceStatus ResourceType { get; set; }
        public virtual ICollection<ResourceProperty>? ResourcePropeties { get; set; }
    }
}
