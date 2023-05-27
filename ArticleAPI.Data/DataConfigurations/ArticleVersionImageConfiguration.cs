using ArticleAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleAPI.Data.DataConfigurations;

public class ArticleVersionImageConfiguration : IEntityTypeConfiguration<ArticleVersionImage>
{
    public void Configure(EntityTypeBuilder<ArticleVersionImage> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.ImagePath).HasMaxLength(250);
        
    }
}