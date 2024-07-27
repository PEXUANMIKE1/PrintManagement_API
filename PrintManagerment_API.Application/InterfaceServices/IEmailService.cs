using PrintManagerment_API.Application.Handle.HandleEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.InterfaceServices
{
    public interface IEmailService
    {
        Task<string> SendEmail(EmailMessage emailMessage);
    }
}
