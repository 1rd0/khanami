using System.ComponentModel.DataAnnotations;

namespace khanami.Contracts
{
    public record LoginUserRequest
    (
        [Required] string name,
          [Required] string password
        );
}
