using PrintManagerment_API.Doman.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class Project:BaseEnities
    { 
        public string ProjectName { get; set; } = string.Empty;
        public string RequestDescriptionFromCustomer { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public int EmployeeId { get; set; } //nhân viên phụ trách
        //[JsonIgnore]
        public User? Employee { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public int CustomerId { get; set; } //khách hàng của dự án
        //[JsonIgnore]
        public Customer? Customer { get; set; }
        public ConstantEnums.ProjectStatus ProjectStatus { get; set; }
        public virtual ICollection<Delivery>? Deliveries { get; set; }
        public virtual ICollection<CustomerFeedBack>? CustomerFeedBacks { get; set; }
        public virtual ICollection<Design>? Designs { get; set; }
    }
}
