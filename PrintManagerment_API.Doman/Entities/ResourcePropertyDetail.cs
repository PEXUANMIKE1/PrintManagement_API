using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class ResourcePropertyDetail:BaseEnities
    {
        public int ResourcePropertyId { get; set; }
        public ResourceProperty? ResourceProperty { get; set; }
        public string ResourcePropertyName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<ImportCoupon>? ImportCoupons { get; set; }
        public virtual ICollection<ResourceForPrintJob>? ResourceForPrintJobs { get; set; }
    }
}
