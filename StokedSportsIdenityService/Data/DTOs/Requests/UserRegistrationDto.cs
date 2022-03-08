using System.ComponentModel.DataAnnotations;

namespace StokedSportsIdenityService.Data.DTOs.Requests
{
    public class UserRegistrationDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}