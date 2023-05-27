using ArticleAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleAPI.Data.DataConfigurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder
            .HasKey(x => x.Name);
        
        builder
            .Property(x => x.Name)
            .HasMaxLength(50);
        
        builder
            .HasMany(x => x.ArticleTags)
            .WithOne(x => x.Tag)
            .HasForeignKey(x => x.TagName);
    }
}