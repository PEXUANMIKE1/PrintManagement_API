using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class Permissions:BaseEnities
    {
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public int RoleId { get; set; }
        public virtual Role? Role { get; set;}
    }
}
