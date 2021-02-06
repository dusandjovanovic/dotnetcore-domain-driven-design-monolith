using System.ComponentModel.DataAnnotations;

namespace DDDMedical.Infrastructure.Identity.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}