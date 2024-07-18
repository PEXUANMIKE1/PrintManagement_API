using PrintManagerment_API.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.ResponseModels.DataTeams
{
    public class DataResponseTeam : DataResponseBase
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int NumberOfMember { get; set; }
        public string ManagerName { get; set; }
    }
}
