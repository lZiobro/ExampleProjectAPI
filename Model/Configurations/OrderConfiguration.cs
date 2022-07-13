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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Price).HasColumnType("decimal").HasPrecision(8, 2);


            builder.HasMany(x => x.Products).WithMany(x => x.Orders);
        }
    }
}
