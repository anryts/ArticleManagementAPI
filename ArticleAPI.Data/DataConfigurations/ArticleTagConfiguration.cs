using ArticleAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleAPI.Data.DataConfigurations;

public class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
{
    public void Configure(EntityTypeBuilder<ArticleTag> builder)
    {
        builder
            .HasKey(compositeKey => new {compositeKey.ArticleId, compositeKey.TagName});
        
        builder.HasGeneratedTsVectorColumn(
                p => p.SearchVector, "english", p => new { p.TagName })
            .HasIndex(p => p.SearchVector)
            .HasMethod("GIN");

    }
}
