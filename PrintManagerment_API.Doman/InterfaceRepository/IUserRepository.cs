using PrintManagerment_API.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.InterfaceRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUserName(string userName);
        Task<User> GetUserByPhoneNumber(string phoneNumber);
        Task AddListRoleForUserAsync(User user, List<string> listRoles);
        Task ChangeRoleForUserAsync(User user, string role);
        Task<IEnumerable<string>> GetRolesOfUserAsync(User user);
        Task DeleteRoleOfUserAsync(User user, List<string> roles);
    }
}
