using Taller.Src.Models;

namespace Taller.Src.Interfaces
{
    public interface ITokenServices
    {
        string GenerateToken(User user, string role);
    }
}