using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Taller.Src.Interfaces;
using Taller.Src.Models;

namespace Taller.Src.Repositories
{
    public class UserRepository(UserManager<User> userManager) : IUserRepository
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly Dictionary<string, (User User, DateTime Expiration)> _userCache = new();
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

        public IQueryable<User> GetUsersQueryable()
        {
            return _userManager.Users.Include(u => u.ShippingAddress).AsNoTracking().AsQueryable();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            // Intentar obtener del caché primero
            if (_userCache.TryGetValue(id, out var cachedEntry) && cachedEntry.Expiration > DateTime.UtcNow)
            {
                return cachedEntry.User;
            }

            var user = await _userManager.Users
                .Include(u => u.ShippingAddress)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                // Guardar en caché
                _userCache[id] = (user, DateTime.UtcNow.Add(_cacheDuration));
            }

            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userManager.Users
                .Include(u => u.ShippingAddress)
                .FirstOrDefaultAsync(u => u.NormalizedEmail == _userManager.NormalizeEmail(email));
        }

        public async Task UpdateUserAsync(User user)
        {
            // Invalidar caché al actualizar
            if (_userCache.ContainsKey(user.Id))
            {
                _userCache.Remove(user.Id);
            }

            await _userManager.UpdateAsync(user);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> UpdatePasswordAsync(User user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (result.Succeeded)
            {
                // Forzar cierre de sesión en otros dispositivos
                await _userManager.UpdateSecurityStampAsync(user);

                // Invalidar caché
                if (_userCache.ContainsKey(user.Id))
                {
                    _userCache.Remove(user.Id);
                }
            }

            return result;
        }


        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }
        public async Task<bool> IsInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }


        // Método para limpiar la caché
        public void ClearCache()
        {
            _userCache.Clear();
        }
    }
}