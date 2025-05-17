using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller.Src.Dtos
{
    public class AuthenticatedUserDto
    {
        public string FirtsName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Token { get; set; } = null!;

    }
}