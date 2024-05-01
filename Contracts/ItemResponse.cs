using System.ComponentModel.DataAnnotations;

namespace khanami.Contracts
{
    public record ItemResponse(
         int Id     ,
           string Name  
        ,
          string Description ,
          decimal Price  ,
          int Category,
          string Imgurl);

}
