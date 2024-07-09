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
        public DataResponseUser EntityDTO(User user)
        {
            var teamName = string.Empty;
            if (user.TeamId == null)
            {
                teamName = "None";
            }
            return new DataResponseUser
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                Avatar = user.Avatar,
                TeamName = teamName,
                CreateTime = user.CreateTime,
                UpdateTime = user.UpdateTime,
            };
        }
    }
}
