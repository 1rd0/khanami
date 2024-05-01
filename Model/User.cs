namespace khanami.Model
{
    public class User
    {
        
        public int Id { get; set; }
        public string userName { get; set; }
        public string PasswordHashe { get; set; }
        public string Profession { get; set; } = string.Empty;

    }
}
