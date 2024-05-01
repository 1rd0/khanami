

using System.ComponentModel.DataAnnotations;

namespace khanami.Contracts
{
    public record RegisterUserRequest
    (
        [Required] string name,
        [Required] string password
    );
}
