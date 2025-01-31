using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class AuthRequestDto
    {
        [MinLength(4, ErrorMessage = "Username must be between 4 and 20 characters")]
        [MaxLength(20, ErrorMessage = "Username must be between 4 and 20 characters")]
        [Required]
        public required string Username { get; set; }

        [MinLength(4, ErrorMessage = "Password must have at least 4 characters")]
        [MaxLength(2147483647, ErrorMessage = "Password must have at most 20 characters")]
        [Required]
        public required string Password { get; set; }
    }
}
