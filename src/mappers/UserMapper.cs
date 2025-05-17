using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller.src.dtos;
using Taller.src.models;

namespace Taller.src.mappers
{
    public class UserMapper
    {
        public static UserDto MapToDTO(User user) =>
            new()
            {
                FirtsName = user.FirtsName,
                LastName = user.LastName,
                Thelephone = user.Thelephone,
                Email = user.Email,
                Street = user.ShippingAddres?.Street ?? string.Empty,
                Number = user.ShippingAddres?.Number ?? string.Empty,
                Commune = user.ShippingAddres?.Commune ?? string.Empty,
                Region = user.ShippingAddres?.Region ?? string.Empty,
                PostalCode = user.ShippingAddres?.PostalCode ?? string.Empty,
            };
    }
}