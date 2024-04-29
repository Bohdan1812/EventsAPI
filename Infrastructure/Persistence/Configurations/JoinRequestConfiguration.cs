using Domain.JoinRequestAggregate;
using Domain.JoinRequestAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class JoinRequestConfiguration : IEntityTypeConfiguration<JoinRequest>
    {
        public void Configure(EntityTypeBuilder<JoinRequest> builder)
        {
            builder.ToTable("JoinRequests");

            builder.HasKey(j => j.Id);

            builder.Property(j => j.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => JoinRequestId.Create(value));

            
        }
    }
}
