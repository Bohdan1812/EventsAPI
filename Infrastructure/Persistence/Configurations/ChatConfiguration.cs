using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.ChatAggregate;
using Domain.ChatAggregate.ValueObjects;

namespace Infrastructure.Persistence.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ChatId.Create(value));

            builder.OwnsMany(c => c.Messages, m =>
            {
                m.ToTable("Messages");

                m.WithOwner().HasForeignKey("ChatId");

                m.HasKey("Id", "ChatId");

                m.Property(m => m.Id)
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => MessageId.Create(value));
            });
            builder.Metadata.FindNavigation(nameof(Chat.Messages))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
