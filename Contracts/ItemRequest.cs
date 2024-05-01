namespace khanami.Contracts
{
    public record ItemRequest(
        
         string Name
      ,
        string Description,
        decimal Price, int Category,
        string Imgurl);

}
