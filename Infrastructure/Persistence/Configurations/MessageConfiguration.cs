using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.ChatAggregate.Entities;
using Domain.MessageAggregate.ValueObjects;

namespace Infrastructure.Persistence.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");

            builder.HasKey(j => j.Id);

            builder.Property(j => j.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => MessageId.Create(value));
        }
    }
}
