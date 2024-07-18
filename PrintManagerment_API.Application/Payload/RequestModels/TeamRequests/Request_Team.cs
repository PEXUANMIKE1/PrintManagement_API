﻿using PrintManagerment_API.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.RequestModels.TeamRequests
{
    public class Request_Team
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? ManagerId { get; set; }
    }
}
