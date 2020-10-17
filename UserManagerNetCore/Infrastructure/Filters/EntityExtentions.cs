using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Models.Models;
using User.ViewModels.ViewModels;

namespace UserManagerNetCore.Infrastructure.Filters
{
    public static class EntityExtentions
    {
        public static void UpdateCategory(this Category category, CategoryViewModel categoryViewModel)
        {
            category.CategoryName = categoryViewModel.CategoryName;
            category.CreatedBy = categoryViewModel.CreatedBy;
            category.CreatedDate = categoryViewModel.CreatedDate;
            category.Deleted = categoryViewModel.Deleted;
            category.Description = categoryViewModel.Description;
            category.Id = categoryViewModel.Id;
            category.ParentId = categoryViewModel.ParentId;
            category.UpdatedBy = categoryViewModel.UpdatedBy;
            category.UpdatedDate = categoryViewModel.UpdatedDate;
        }

        public static void UpdateCustomer(this Customer customer, CustomerViewModel customerViewModel)
        {
            customer.FirstName = customerViewModel.FirstName;
            customer.LastName = customerViewModel.LastName;
            customer.CreatedBy = customerViewModel.CreatedBy;
            customer.CreatedDate = customerViewModel.CreatedDate;
            customer.Deleted = customerViewModel.Deleted;
            customer.Id = customerViewModel.Id;
            customer.UpdatedBy = customerViewModel.UpdatedBy;
            customer.UpdatedDate = customerViewModel.UpdatedDate;
            customer.Address = customerViewModel.Address;
            customer.Gender = customerViewModel.Gender;
            customer.Phone = customerViewModel.Phone;
        }
    }
}
