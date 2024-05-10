

using System.ComponentModel.DataAnnotations;

namespace khanami.Contracts
{
    public record OrderResponse
    (
        [Required] string name,
        [Required] string Orders
    );
}
