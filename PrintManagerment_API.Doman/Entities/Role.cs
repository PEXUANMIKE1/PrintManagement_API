using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class Role:BaseEnities
    {
        public string RoleCode { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
