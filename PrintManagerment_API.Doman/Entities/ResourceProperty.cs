using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class ResourceProperty:BaseEnities
    {
        public string ResourcePropertyName { get; set; } = string.Empty;
        public int ResourcesId { get; set; }
        public Resources? Resources { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<ResourcePropertyDetail>? ResourcePropertyDetails { get; set; }
    }
}
