using Hackathon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hackathon.Persistence
{
    public class ArticleEntityTypeConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Ignore(p => p.DomainEvents);

            builder.Property(p => p.ImageUrl).IsRequired();
            builder.Property(p => p.Body).IsRequired();
        }
    }
}
