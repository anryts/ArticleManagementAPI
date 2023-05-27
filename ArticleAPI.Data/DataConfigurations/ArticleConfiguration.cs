using ArticleAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleAPI.Data.DataConfigurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Entities.Article>
{
    public void Configure(EntityTypeBuilder<Entities.Article> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(100);

        builder.Property(x => x.ContentPath)
            .HasMaxLength(250);

        builder.Property(x => x.ShortIntro)
            .HasMaxLength(1000);

        builder.HasGeneratedTsVectorColumn(
            p => p.SearchVector, "english", p => new { p.Title })
            .HasIndex(p => p.SearchVector)
            .HasMethod("GIN");

        builder
            .HasMany(x => x.ArticleTags)
            .WithOne(x => x.Article);

        builder
            .HasMany(x => x.Comments)
            .WithOne(x => x.Article);
    }
}