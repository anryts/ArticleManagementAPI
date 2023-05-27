using Gateway.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gateway.Data.DataConfigurations;

public class UserOPTConfiguration : IEntityTypeConfiguration<UserOTP>
{
    public void Configure(EntityTypeBuilder<UserOTP> builder)
    {
        builder
            .HasKey(compositeKey => new { compositeKey.UserId, compositeKey.Code });
    }
}