using System.ComponentModel.DataAnnotations;

namespace testProjectApis.Models
{
    public class RequestLogin
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
