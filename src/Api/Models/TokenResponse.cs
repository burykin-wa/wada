using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class TokenResponse
    {
        [Required]
        public required string AccessToken { get; set; }
        
        [Required]
        public required string RefreshToken { get; set; }
    }
}
