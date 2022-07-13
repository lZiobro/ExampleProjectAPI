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
    public class ApplicationUserDetailsConfiguration : IEntityTypeConfiguration<ApplicationUserDetails>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserDetails> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserId).IsRequired().HasMaxLength(450);

            builder.Property(x => x.Race).HasMaxLength(50);
            builder.Property(x => x.Occupation).HasMaxLength(50);
            builder.Property(x => x.Experience).HasMaxLength(50);
            builder.Property(x => x.Home).HasMaxLength(50);
            builder.Property(x => x.HasEquipment).HasDefaultValue(false);
            builder.Property(x => x.AboutMe).HasMaxLength(4095).IsRequired(false);
            builder.Property(x => x.Likes).HasMaxLength(100);
            builder.Property(x => x.Dislikes).HasMaxLength(100);
            builder.Property(x => x.Specialty).HasMaxLength(50);
        }
    }
}
