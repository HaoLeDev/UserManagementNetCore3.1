using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using User.Models.BaseModel;

namespace User.Models.Models
{
    [Table("Categories")]
    public class Category:BaseEntity
    {
        [StringLength(256)]
        [Required(ErrorMessage ="Tên danh mục không được để trống.")]
        public string CategoryName { get; set; }

        [StringLength(128)]
        public string ParentId { get; set; }

        [StringLength(256)]
        public string Description { get; set; }
    }
}
