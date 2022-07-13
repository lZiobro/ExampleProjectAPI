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
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasKey(x => x.Id);

            builder.Property(x => x.SenderId).HasMaxLength(450).IsRequired(false).HasDefaultValue(null);
            builder.Property(x => x.SenderName).HasMaxLength(256);
            builder.Property(x => x.ReceiverId).HasMaxLength(450).IsRequired(false).HasDefaultValue(null);
            builder.Property(x => x.ReceiverName).HasMaxLength(256);
            builder.Property(x => x.Topic).HasMaxLength(255);
            builder.Property(x => x.Content).HasMaxLength(4095);
            builder.Property(x => x.DateSend).HasColumnType("datetime2").HasPrecision(0);
            builder.Property(x => x.Read).HasDefaultValue(false);
        }
    }
}