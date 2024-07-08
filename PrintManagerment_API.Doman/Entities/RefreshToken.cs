using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class RefreshToken : BaseEnities
    {
        public string Token { get; set; } = string.Empty;
        public long UserId { get; set; }
        public virtual User? User { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
