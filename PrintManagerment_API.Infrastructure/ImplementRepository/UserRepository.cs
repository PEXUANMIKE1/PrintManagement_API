using Microsoft.EntityFrameworkCore;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using PrintManagerment_API.Infrastructure.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Infrastructure.ImplementRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Xử lý chuỗi
        private Task<bool> CompareStringAsync(string str1, string str2)
        {
            return Task.FromResult(string.Equals(str1.ToLowerInvariant(),str2.ToLowerInvariant()));
        }
        private async Task<bool> IsStringInListAsync(string inputString, List<string> listString)
        {
            if(inputString == null)
            {
                throw new ArgumentNullException(nameof(inputString));
            }
            if(listString == null)
            {
                throw new ArgumentNullException(nameof(listString));
            }
            foreach(var item in listString)
            {
                if(await CompareStringAsync(inputString, item))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        public async Task AddListRoleForUserAsync(User user, List<string> listRoles)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if(listRoles == null)
            {
                throw new ArgumentNullException(nameof(listRoles));
            }
            foreach(var role in listRoles.Distinct())
            {
                var roleOfUser = await GetRolesOfUserAsync(user);
                if(await IsStringInListAsync(role, roleOfUser.ToList()))
                {
                    throw new ArgumentException("Người dùng đã có quyền này rồi!");
                }
                else
                {
                    var roleItem = await _appDbContext.Roles.SingleOrDefaultAsync(x=>x.RoleCode.Equals(role));
                    if(roleItem == null)
                    {
                        throw new ArgumentNullException("Không tồn tại quyền này!");
                    }
                    _appDbContext.Permissions.Add(new Permissions
                    {
                        RoleId = roleItem.Id,
                        UserId = user.Id,
                    });
                }
            }
            _appDbContext.SaveChanges();
        }

        public async Task DeleteRoleOfUserAsync(User user, List<string> roles)
        {
            if(roles == null)
            {
                throw new ArgumentNullException(nameof(roles));
            }
            var listRoles = await GetRolesOfUserAsync(user);
            foreach (var role in listRoles)
            {
                foreach(var itemRole in roles)
                {
                    if(await CompareStringAsync(itemRole, role))
                    {
                        var roleObject = await _appDbContext.Roles.SingleOrDefaultAsync(x=>x.RoleCode.Equals(role));
                        var permission = await _appDbContext.Permissions.SingleOrDefaultAsync(x => x.RoleId == roleObject.Id && x.UserId == user.Id);
                        _appDbContext.Permissions.Remove(permission);
                        break;
                    }
                }
            }
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetRolesOfUserAsync(User user)
        {
            var roles = new List<string>();
            var listRoleOfPermission = _appDbContext.Permissions.Where(x=>x.UserId == user.Id).AsQueryable();
            foreach(var item in listRoleOfPermission.Distinct()) 
            {
                var role = _appDbContext.Roles.SingleOrDefault(x => x.Id == item.RoleId);
                roles.Add(role.RoleCode);
            }
            return roles.AsEnumerable();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _appDbContext.Users.SingleOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            return user;
        }

        public async Task<User> GetUserByPhoneNumber(string phoneNumber)
        {
            var user = await _appDbContext.Users.SingleOrDefaultAsync(x => x.Email.ToLower().Equals(phoneNumber.ToLower()));
            return user;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var user = await _appDbContext.Users.SingleOrDefaultAsync(x => x.Email.ToLower().Equals(userName.ToLower()));
            return user;
        }

        public async Task ChangeRoleForUserAsync(User user, string role)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            var roleOfUser = await GetRolesOfUserAsync(user);
            foreach (var item in roleOfUser)
            {
                
                if (item.ToLower().Equals(role.Trim().ToLower()))
                {
                    throw new ArgumentException("Người dùng đã có quyền này rồi!");
                }
                else
                {
                    var roleItem = await _appDbContext.Roles.SingleOrDefaultAsync(x => x.RoleCode.Equals(role.Trim()));
                    if (roleItem == null)
                    {
                        throw new ArgumentNullException("Không tồn tại quyền này!");
                    }
                    //Nếu người có role cũ thì đổi roleId trong bảng permission thành roleId mới
                    var permissUser = await _appDbContext.Permissions.SingleOrDefaultAsync(x=>x.UserId == user.Id);
                    if (permissUser != null)
                    {
                        permissUser.RoleId = roleItem.Id;
                        _appDbContext.Permissions.Update(permissUser);
                    }
                    else
                    {
                        //thêm role mới
                        _appDbContext.Permissions.Add(new Permissions
                        {
                            RoleId = roleItem.Id,
                            UserId = user.Id,
                        });
                    }
                }
            }
            _appDbContext.SaveChanges();
        }
    }
}
