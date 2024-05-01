using khanami.Entities;

namespace khanami.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(UserEmtity user);
    }
}