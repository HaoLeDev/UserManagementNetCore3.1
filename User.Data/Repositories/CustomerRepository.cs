using System;
using System.Collections.Generic;
using System.Text;
using User.Data.Context;
using User.Data.Infrastructure;
using User.Models.Models;

namespace User.Data.Repositories
{
    public interface ICustomerRepository: IRepository<Customer> { }
    public class CustomerRepository: EfCoreRepository<Customer, UserDbContext>, ICustomerRepository
    {
        public CustomerRepository(UserDbContext userDbContext) : base(userDbContext) { }
    }
}
