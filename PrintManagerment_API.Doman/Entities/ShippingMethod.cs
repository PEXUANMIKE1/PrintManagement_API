using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class ShippingMethod : BaseEnities
    {
        public string ShippingMethodName { get; set; } = string.Empty;
        public virtual ICollection<Delivery>? Deliveries { get; set; }
    }
}
