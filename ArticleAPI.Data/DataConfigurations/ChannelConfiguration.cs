using ArticleAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleAPI.Data.DataConfigurations;

public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder
            .HasKey(x => x.Id);
        
        builder
            .HasAlternateKey(x => x.Title);
        
        builder
            .Property(x => x.Title)
            .HasMaxLength(100);
        
        builder
            .Property(x => x.Description)
            .HasMaxLength(500);
        
        builder
            .HasMany(x => x.Articles)
            .WithOne(x => x.Channel);
    }
}