using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace User.Models.BaseModel
{
    public abstract class  BaseEntity
    {
        [Key]
        [Column(TypeName = "nvarchar(128)")]
        public Guid Id { get; set; } = new Guid();

        [StringLength(128)]
        public string CreatedBy { get; set; }

        [StringLength(128)]
        public string UpdatedBy { get; set; }

        [Column(TypeName = "Datetime")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "Datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
