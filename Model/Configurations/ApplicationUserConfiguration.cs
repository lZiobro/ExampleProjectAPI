using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.isListed).HasDefaultValue(false);

            builder.HasOne(x => x.Details).WithOne(x => x.User).HasForeignKey<ApplicationUserDetails>(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Messages).WithOne(x => x.Receiver).HasForeignKey(x => x.ReceiverId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasMany(x => x.SendMessages).WithOne(x => x.Sender).HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}