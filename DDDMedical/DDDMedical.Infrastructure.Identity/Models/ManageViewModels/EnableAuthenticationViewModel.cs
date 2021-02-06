using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DDDMedical.Infrastructure.Identity.Models.ManageViewModels
{
    public class EnableAuthenticationViewModel
    {
        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at most {1} characters.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Verification code")]
        public string Code { get; set; }
        
        [ReadOnly(true)]
        public string SharedKey { get; set; }
        
        public string AuthenticatorUri { get; set; }
    }
}