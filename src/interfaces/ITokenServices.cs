using Taller.src.models;

namespace Taller.src.interfaces
{
    public interface ITokenServices
    {
        string GenerateToken(User user, string role);
    }
}