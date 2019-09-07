using Hackathon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hackathon.Persistence
{
    public class DataEntityTypeConfiguration : IEntityTypeConfiguration<Data>
    {
        public void Configure(EntityTypeBuilder<Data> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Ignore(p => p.DomainEvents);

            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.Temperature).IsRequired();
            builder.Property(p => p.RainFall).IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(Data.Articles));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
