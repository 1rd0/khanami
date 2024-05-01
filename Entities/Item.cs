using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace khanami.Model
{
    public class Item
    {
        public int Id { get; set; }

      //  public int category_id { get; set; }
        [MaxLength(20)]

        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public decimal Price { get; set; }
         public int Category {get; set; }
        public string Imgurl { get; set; }

    }
}
