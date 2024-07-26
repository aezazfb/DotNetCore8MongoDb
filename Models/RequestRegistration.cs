using System.ComponentModel.DataAnnotations;

namespace testProjectApis.Models
{
    public class RequestRegistration
    {
        [Required]
        public string UserID { get; set; }

        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Firstname { get; set; }

        [Required]
        public required string Lastname { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
