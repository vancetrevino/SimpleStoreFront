using System.ComponentModel.DataAnnotations;

namespace SimpleStoreFront.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [MaxLength(300, ErrorMessage="Message is too long.")]
        public string Message { get; set; }
    }
}
