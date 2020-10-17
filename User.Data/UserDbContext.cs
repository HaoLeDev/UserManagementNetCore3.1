using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Commons.Services;
using User.Models.Models;

namespace User.Data.Context
{
    public class UserDbContext : IdentityDbContext<AppUser>
    {
        private readonly ICurrentUserService currentUser;

        public UserDbContext(
            DbContextOptions<UserDbContext> options,
            ICurrentUserService currentUser)
            : base(options)
            => this.currentUser = currentUser;
        //IdentityDbContext<UserProfile>
        //const string connectionString = "Server=HAOLV;Database=HUserDb;user id=sa;password=123456;Trusted_Connection=false;";

        public UserDbContext() { }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

       // public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            //optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
