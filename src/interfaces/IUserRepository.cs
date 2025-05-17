using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Taller.Src.Dtos;
using Taller.Src.Models;

namespace Taller.Src.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAllUserAsync();
        Task<UserDto> GetUserByIdAsync(string firstName);
        Task CreateUserAsync(User user, ShippingAddress? shippingAddress);
        void UpdateUser(User user);
        void UpdateShippingAddressAsync(UserDto userDto);
        void DeleteUserAsync(User user, ShippingAddress shippingAddress);
    }
}