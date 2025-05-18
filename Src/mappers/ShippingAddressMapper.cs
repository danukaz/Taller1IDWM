using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller.Src.Dtos;
using Taller.Src.Models;

namespace Taller.Src.Mappers
{
    public static class ShippingAddressMapper
    {
        public static ShippingAddress FromDto(CreateShippingAddressDto dto, string userId)
        {
            return new ShippingAddress
            {
                Street = dto.Street,
                Number = dto.Number,
                Commune = dto.Commune,
                Region = dto.Region,
                PostalCode = dto.PostalCode,
                UserId = userId
            };
        }

        public static ShippingAddressDto ToDto(ShippingAddress shippingAddress)
        {
            return new ShippingAddressDto
            {
                Street = shippingAddress.Street,
                Number = shippingAddress.Number,
                Commune = shippingAddress.Commune,
                Region = shippingAddress.Region,
                PostalCode = shippingAddress.PostalCode
            };
        }
    }
}