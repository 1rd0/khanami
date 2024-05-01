namespace khanami.Entities

{
    public class UserEmtity 
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Password  { get; set; } = string.Empty;
        public string Profession { get; set; } = string.Empty;
    }
}
