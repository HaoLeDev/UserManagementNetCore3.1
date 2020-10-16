using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using User.Models.BaseModel;

namespace User.Models.Models
{
    [Table("AspNetUsers")]
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Column(TypeName ="Datetime")]
        public DateTime? DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        [StringLength(128)]
        public string CreatedBy { get; set; }
        public Profile Profile { get; set; }
    }
}
