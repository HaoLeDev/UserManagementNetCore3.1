using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using User.Models.BaseModel;

namespace User.Models.Models
{
    [Table("Profiles")]
    public class Profile : BaseEntity
    {

        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(400)]
        public string MainPhotoUrl { get; set; }

        [MaxLength(400)]
        public string WebSite { get; set; }

        [MaxLength(4000)]
        public string Biography { get; set; }

        public bool Gender { get; set; }

        public bool IsPrivate { get; set; }

        [ForeignKey("UserId")]
        public AppUser UserProfile { get; set; }
    }
}
