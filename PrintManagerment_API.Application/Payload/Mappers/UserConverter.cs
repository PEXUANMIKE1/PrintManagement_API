using PrintManagerment_API.Application.Payload.ResponseModels.DataUsers;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.Mapper
{
    public class UserConverter
    {
        private readonly IBaseRepository<Team> _baseTeamRepository;
        public UserConverter(IBaseRepository<Team> baseTeamRepository)
        {
            _baseTeamRepository = baseTeamRepository;
        }
        public async Task<DataResponseUser> EntityDTO(User user)
        {
            var teamname = "None";
            if (user.TeamId != null)
            {
                var team = await _baseTeamRepository.GetByIdAsync(user.TeamId.Value);
                teamname = team.Name;
            }
            
           
            return new DataResponseUser
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                Avatar = user.Avatar,
                TeamName = teamname,
                CreateTime = user.CreateTime,
                UpdateTime = user.UpdateTime,
            };
        }
    }
}
