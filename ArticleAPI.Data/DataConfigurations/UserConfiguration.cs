using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleAPI.Data.DataConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<Entities.User>
{
    public void Configure(EntityTypeBuilder<Entities.User> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.FirstName)
            .HasMaxLength(50);

        builder
            .Property(x => x.LastName)
            .HasMaxLength(50);
            
        builder
            .Property(x => x.Email)
            .HasMaxLength(50);

        builder
            .Property(x => x.PhoneNumber)
            .HasMaxLength(20);
    }
}