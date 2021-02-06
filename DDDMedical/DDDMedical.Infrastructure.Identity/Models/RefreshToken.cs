using System;
using System.ComponentModel.DataAnnotations;

namespace DDDMedical.Infrastructure.Identity.Models
{
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; }
        public string JwtId { get; set; }
        
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        
        public bool Used { get; set; }
        public bool Invalidated { get; set; }
        public string UserId { get; set; }
    }
}