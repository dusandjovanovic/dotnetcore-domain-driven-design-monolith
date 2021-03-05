using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DDDMedical.Domain.Models;

namespace DDDMedical.Application.ViewModels
{
    public class DoctorViewModel
    {
        [Key]
        public Guid Id { get; set; }
        
        [Key]
        public Guid ReferenceId { get; set; }
        
        [Required(ErrorMessage =  "Name is required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Reservation day is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Invalid format")]
        [DisplayName("Reservation day")]
        public DateTime ReservationDay { get; set; }

        [Required(ErrorMessage = "Roles are required")]
        public ImmutableList<Role> Roles { get; protected set; }
        
        [Required(ErrorMessage = "Reservations are required")]
        public List<DateTime> Reservations { get; set; }
    }
}