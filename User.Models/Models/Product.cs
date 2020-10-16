using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using User.Models.BaseModel;

namespace User.Models.Models
{
    [Table("Products")]
    public class Product:BaseEntity
    {
        [Required(ErrorMessage ="Tên sản phẩm không được để trống!")]
        [StringLength(256)]
        public string ProductName { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public Guid CategoryId { get; set; }

        public int Price { get; set; }

        public int Amount { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [ForeignKey("CategoryId")]
        public Category Categories { get; set; }
    }
}
