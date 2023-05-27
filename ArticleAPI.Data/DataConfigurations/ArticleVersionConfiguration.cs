using ArticleAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleAPI.Data.DataConfigurations
{
    public class ArticleVersionConfiguration : IEntityTypeConfiguration<ArticleVersion>
    {
        public void Configure(EntityTypeBuilder<ArticleVersion> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(100);

            builder.Property(x => x.ContentPath)
                .HasMaxLength(250);

            builder.HasMany(x => x.ArticleVersionImages)
                .WithOne(x => x.ArticleVersion);
        }
    }
}
