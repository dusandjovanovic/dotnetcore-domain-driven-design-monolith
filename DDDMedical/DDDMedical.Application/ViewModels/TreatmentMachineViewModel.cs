using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DDDMedical.Application.ViewModels
{
    public class TreatmentMachineViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage =  "Name is required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }
    }
}