using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using User.Models.BaseModel;

namespace User.Models.Models
{
    public class Customer: BaseEntity
    {
        [StringLength(100)]
        [Required(ErrorMessage ="Họ và tên đệm không được để trống")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Tên không được để trống")]
        public string LastName { get; set; }

        public bool Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
