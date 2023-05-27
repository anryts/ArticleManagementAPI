using ArticleAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleAPI.Data.DataConfigurations;

public class ArticleImageConfiguration : IEntityTypeConfiguration<ArticleImage>
{
    public void Configure(EntityTypeBuilder<ArticleImage> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .Property(x => x.ImagePath)
            .HasMaxLength(250);
        
        builder
            .HasOne(x => x.Article)
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.ArticleId);
    }
}