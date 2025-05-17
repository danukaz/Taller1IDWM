using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller.Src.Dtos;
using Taller.Src.Models;

namespace Taller.Src.Mappers
{
    public class UserMapper
    {
        public static UserDto MapToDTO(User user) =>
            new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Telephone = user.Telephone,
                Email = user.Email,
                Street = user.ShippingAddress?.Street ?? string.Empty,
                Number = user.ShippingAddress?.Number ?? string.Empty,
                Commune = user.ShippingAddress?.Commune ?? string.Empty,
                Region = user.ShippingAddress?.Region ?? string.Empty,
                PostalCode = user.ShippingAddress?.PostalCode ?? string.Empty,
            };
    }
}