using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DDDMedical.Application.ViewModels
{
    public class CustomerViewModel
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage =  "Name is required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Birth date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Invalid format")]
        [DisplayName("Birth date")]
        public DateTime BirthDate { get; set; }
    }
}