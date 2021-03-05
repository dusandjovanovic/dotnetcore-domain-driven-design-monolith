using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DDDMedical.Application.ViewModels
{
    public class ConsultationViewModel
    {
        [Key]
        public Guid Id { get; set; }
        
        [Key]
        public Guid DoctorId { get; set; }
        
        [Key]
        public Guid PatientId { get; set; }
        
        [Key]
        public Guid TreatmentRoomId { get; set; }
        
        [Required(ErrorMessage = "Registration date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Invalid format")]
        [DisplayName("Registration date")]
        public DateTime RegistrationDate { get; set; }
        
        [Required(ErrorMessage = "Consultation date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Invalid format")]
        [DisplayName("Consultation date")]
        public DateTime ConsultationDate { get; set; }
    }
}