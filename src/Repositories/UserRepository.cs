using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller.src.data;
using Taller.src.dtos;
using Taller.src.interfaces;
using Taller.src.mappers;
using Taller.src.models;

using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Taller.src.repositories
{
    public class UserRepository(StoreContext store) : IUserRepository
    {
        private readonly StoreContext _context = store;
        public async Task CreateUserAsync(User user, ShippingAddres? shippingAddres)
        {
            await _context.Users.AddAsync(user);
            if (shippingAddres != null) await _context.ShippingAddres.AddAsync(shippingAddres);
        }

        public void DeleteUserAsync(User user, ShippingAddres shippingAddres)
        {
            _context.ShippingAddres.Remove(shippingAddres);
            _context.Users.Remove(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            var users = await _context.Users.Include(x => x.ShippingAddres).ToListAsync();
            return users.Select(UserMapper.MapToDTO);
        }

        public Task<UserDto> GetUserByIdAsync(string firtsName)
        {
            var user = _context.Users.Include(x => x.ShippingAddres).FirstOrDefault(x => x.FirtsName == firtsName) ?? throw new Exception("User not found");
            return Task.FromResult(UserMapper.MapToDTO(user));
        }

        public void UpdateShippingAddresAsync(UserDto userDto)
        {
            var user = _context.Users.Include(x => x.ShippingAddres).FirstOrDefault(x => x.FirtsName == userDto.FirtsName) ?? throw new Exception("User not found");

            if (user.ShippingAddres == null)
            {
                user.ShippingAddres = new ShippingAddres
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
                user.ShippingAddres.Street = userDto.Street ?? string.Empty;
                user.ShippingAddres.Number = userDto.Number ?? string.Empty;
                user.ShippingAddres.Commune = userDto.Commune ?? string.Empty;
                user.ShippingAddres.Region = userDto.Region ?? string.Empty;
                user.ShippingAddres.PostalCode = userDto.PostalCode ?? string.Empty;
            }
            _context.ShippingAddres.Update(user.ShippingAddres);
            _context.Users.Update(user);
        }

        public void UpdateUser(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(x => x.FirtsName == user.FirtsName) ?? throw new Exception("User not found");
            if (existingUser != null)
            {
                existingUser.LastName = user.LastName;
                existingUser.Thelephone = user.Thelephone;
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