using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class ConfirmEmail : BaseEnities
    {
        public string ConfirmCode { get; set; } = string.Empty;
        public long UserId { get; set; }
        public virtual User? User { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsConfirmed { get; set; } = false;
    }
}
