using System.ComponentModel.DataAnnotations;

namespace khanami.Contracts
{
    public record EditCartRequest
    (
        [Required] string Username,
        [Required] List<CartItem> Shopcarts
    );

    public record CartItem
    (
        [Required] int id,
        [Required] int quantity
    );
}
