namespace khanami.Interfaces
{
    public interface IUserService
    {
        Task<string> Login(string username, string password);
        Task Register(string username, string password);
    }
}