using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.Response
{
    public class ResponseMessage
    {
        public static string GetEmailSuccessMessage(string email)
        {
            return $"Mã đã được gửi đến: {email}";
        }
    }
}
