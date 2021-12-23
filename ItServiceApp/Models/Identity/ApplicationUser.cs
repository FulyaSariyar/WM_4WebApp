using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ItServiceApp.Models.Identity
{
    public class ApplicationUser :IdentityUser
    {
        [StringLength(50)]
        public string name { get; set; }
        [StringLength(50)]
        public string Surname { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
