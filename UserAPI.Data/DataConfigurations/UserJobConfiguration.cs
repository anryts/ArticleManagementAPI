using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAPI.Data.Entities;

namespace UserAPI.Data.DataConfigurations;

public class UserJobConfiguration : IEntityTypeConfiguration<UserJob>
{
    public void Configure(EntityTypeBuilder<UserJob> builder)
    {
        builder
            .HasKey(compositeKey => new { compositeKey.UserId, compositeKey.JobId });
    }
}