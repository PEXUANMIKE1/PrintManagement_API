using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class User:BaseEnities
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;    
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? TeamId { get; set; }
        public virtual Team? Team { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual ICollection<Permissions>? Permissions { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
        public virtual ICollection<KeyPerformanceIndicators>? KeyPerformanceIndicators { get; set; }
        public virtual ICollection<Bill>? Bills { get; set; }
        public virtual ICollection<Project>? Projects { get; set; }
        public virtual ICollection<CustomerFeedBack>? CustomerFeedBacks { get; set; }
        public virtual ICollection<ImportCoupon>? ImportCoupons { get; set; }
        public virtual ICollection<Design>? Designs { get; set; }
    }
}
