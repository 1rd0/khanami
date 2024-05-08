using System.ComponentModel.DataAnnotations.Schema;

namespace khanami.Model
{
    public class Carts
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string ItemsCarts { get; set; }
    }
}
