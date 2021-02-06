using System.ComponentModel.DataAnnotations;

namespace DDDMedical.Infrastructure.Identity.Models.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Recovery code")]
        public string RecoverCode { get; set; }
    }
}