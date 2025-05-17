using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller.Src.Data;
using Taller.Src.Dtos;
using Taller.Src.Interfaces;
using Taller.Src.Mappers;
using Taller.Src.Models;

using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Taller.Src.Repositories
{
    public class UserRepository(StoreContext store) : IUserRepository
    {
        private readonly StoreContext _context = store;
        public async Task CreateUserAsync(User user, ShippingAddress? shippingAddress)
        {
            await _context.Users.AddAsync(user);
            if (shippingAddress != null) await _context.ShippingAddress.AddAsync(shippingAddress);
        }

        public void DeleteUserAsync(User user, ShippingAddress shippingAddress)
        {
            _context.ShippingAddress.Remove(shippingAddress);
            _context.Users.Remove(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            var users = await _context.Users.Include(x => x.ShippingAddress).ToListAsync();
            return users.Select(UserMapper.MapToDTO);
        }

        public Task<UserDto> GetUserByIdAsync(string firstName)
        {
            var user = _context.Users.Include(x => x.s).FirstOrDefault(x => x.FirstName == firstName) ?? throw new Exception("User not found");
            return Task.FromResult(UserMapper.MapToDTO(user));
        }

        public void UpdateShippingAddressAsync(UserDto userDto)
        {
            var user = _context.Users.Include(x => x.ShippingAddress).FirstOrDefault(x => x.FirstName == userDto.FirstName) ?? throw new Exception("User not found");

            if (user.ShippingAddress == null)
            {
                user.ShippingAddress = new ShippingAddress
                {
                    Street = userDto.Street ?? string.Empty,
                    Number = userDto.Number ?? string.Empty,
                    Commune = userDto.Commune ?? string.Empty,
                    Region = userDto.Region ?? string.Empty,
                    PostalCode = userDto.PostalCode ?? string.Empty,
                };
            }
            else
            {
                user.ShippingAddress.Street = userDto.Street ?? string.Empty;
                user.ShippingAddress.Number = userDto.Number ?? string.Empty;
                user.ShippingAddress.Commune = userDto.Commune ?? string.Empty;
                user.ShippingAddress.Region = userDto.Region ?? string.Empty;
                user.ShippingAddress.PostalCode = userDto.PostalCode ?? string.Empty;
            }
            _context.ShippingAddress.Update(user.ShippingAddress);
            _context.Users.Update(user);
        }

        public void UpdateUser(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(x => x.FirstName == user.FirstName) ?? throw new Exception("User not found");
            if (existingUser != null)
            {
                existingUser.LastName = user.LastName;
                existingUser.Telephone = user.Telephone;
                existingUser.Email = user.Email;
                _context.Users.Update(existingUser);
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }
}