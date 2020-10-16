using System;
using System.Collections.Generic;
using System.Text;

namespace User.ViewModels.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        public string CategoryName { get; set; }

        public string ParentId { get; set; }

        public string Description { get; set; }
    }
}
