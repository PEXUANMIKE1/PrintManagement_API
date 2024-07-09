using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.RequestModels.UserRequests
{
    public class Request_RefreshToken
    {
        public string RefreshToken { get; set; } = string.Empty;

    }
}
