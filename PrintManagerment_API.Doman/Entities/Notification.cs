using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class Notification : BaseEnities
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; }
        public bool IsSeen { get; set; }
    }
}
