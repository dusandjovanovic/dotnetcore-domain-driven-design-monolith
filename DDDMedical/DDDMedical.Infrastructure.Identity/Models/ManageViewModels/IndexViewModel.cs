using System.ComponentModel.DataAnnotations;

namespace DDDMedical.Infrastructure.Identity.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }
        public string StatusMessage { get; set; }        
        public bool IsEmailConfirmed { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        

    }
}