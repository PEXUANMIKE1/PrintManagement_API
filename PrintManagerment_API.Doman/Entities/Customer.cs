using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class Customer:BaseEnities
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public virtual ICollection<Bill>? Bills { get; set; }
        public virtual ICollection<Delivery>? Deliveries { get; set; }
        public virtual ICollection<Project>? Projects { get; set; }
        public virtual ICollection<CustomerFeedBack>? CustomerFeedBacks { get; set; }
    }
}
