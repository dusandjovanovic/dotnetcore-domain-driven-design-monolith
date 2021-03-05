using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DDDMedical.Application.ViewModels
{
    public class TreatmentRoomViewModel
    {
        [Key]
        public Guid Id { get; set; }
        
        [Key]
        public Guid TreatmentMachineId { get; set; }

        [Required(ErrorMessage =  "Name is required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Reservation day is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Invalid format")]
        [DisplayName("Reservation day")]
        public DateTime ReservationDay { get; set; }
    }
}