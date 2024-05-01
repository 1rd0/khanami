using System.ComponentModel.DataAnnotations;

namespace khanami.Entities
{
    public record ItemCategory
    {
        [Key]
        public int Category_id { get; set; }
        
        public string CategoryName { get; set; }
    }
}
