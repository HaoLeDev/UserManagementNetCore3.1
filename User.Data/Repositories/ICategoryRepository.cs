using System;
using System.Collections.Generic;
using System.Text;
using User.Data.Context;
using User.Data.Infrastructure;
using User.Models.Models;

namespace User.Data.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
    }

    public class CategoryRepository:EfCoreRepository<Category,UserDbContext>, ICategoryRepository
    {
        public CategoryRepository(UserDbContext context) : base(context)
        {

        }
    }
}
