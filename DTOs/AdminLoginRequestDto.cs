using System.ComponentModel.DataAnnotations;

namespace AnnapurnaEnterprises.Api.DTOs
{
    public class AdminLoginRequestDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}