using Microsoft.AspNetCore.Http;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.RequestModels.NewFolder
{
    public class Request_Design
    {
        public int ProjectId { get; set; }
        public IFormFile FilePath { get; set; }
    }
}
