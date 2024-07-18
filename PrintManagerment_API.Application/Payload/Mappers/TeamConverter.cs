using PrintManagerment_API.Application.Payload.ResponseModels.DataTeams;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.Mappers
{
    public class TeamConverter
    {
        private readonly IBaseRepository<User> _baseUserRepository;
        public TeamConverter(IBaseRepository<User> baseUserRepository)
        {
            _baseUserRepository = baseUserRepository;
        }
        public async Task<DataResponseTeam> EntityDTO(Team team)
        {
            var managerName = "";
            if(team.ManagerId != null)
            {
                managerName = _baseUserRepository.GetAsync(x => x.Id == team.ManagerId).Result.FullName;
            }
            return new DataResponseTeam
            {
                Id = team.Id,
                Name = team.Name,
                Description = team.Description,
                NumberOfMember = team.NumberOfMember,
                ManagerName = managerName,
            };
        }
    }
}
