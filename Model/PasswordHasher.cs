 
namespace khanami.Model
{
    public class PasswordHasher : IPasswordHasher
    {

        public string Generate(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool Verify(string password, string Hashedpassword) => BCrypt.Net.BCrypt.EnhancedVerify(password, Hashedpassword);


    }
}
