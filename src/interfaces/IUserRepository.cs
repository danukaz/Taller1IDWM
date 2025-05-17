using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Taller.src.dtos;
using Taller.src.models;

namespace Taller.src.interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAllUserAsync();
        Task<UserDto> GetUserByIdAsync(string firtsName);
        Task CreateUserAsync(User user, ShippingAddres? shippingAddres);
        void UpdateUser(User user);
        void UpdateShippingAddresAsync(UserDto userDto);
        void DeleteUserAsync(User user, ShippingAddres shippingAddres);
    }
}