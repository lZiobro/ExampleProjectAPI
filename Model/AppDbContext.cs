using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Configurations;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUserDetails> ApplicationUsersDetails { get; set; }
        public DbSet<Message> Messages { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
        }
            
    }
}
