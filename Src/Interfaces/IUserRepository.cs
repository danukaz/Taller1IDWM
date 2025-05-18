using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Microsoft.AspNetCore.Identity;

using Taller.Src.Dtos;
using Taller.Src.Models;


namespace Taller.Src.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsersQueryable();
        Task<User?> GetUserByIdAsync(string id);
        Task<User?> GetUserByEmailAsync(string email);
        Task UpdateUserAsync(User user);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<IdentityResult> UpdatePasswordAsync(User user, string currentPassword, string newPassword);
        Task<IList<string>> GetUserRolesAsync(User user);
        Task<bool> IsInRoleAsync(User user, string roleName);
        void ClearCache();
    }
}