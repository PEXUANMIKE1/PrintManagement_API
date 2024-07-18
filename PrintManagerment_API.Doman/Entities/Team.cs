using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class Team:BaseEnities
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int NumberOfMember { get; set; }
        public DateTime CreateTime { get; set;}
        public DateTime UpdateTime { get; set; }
        public int? ManagerId { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}
