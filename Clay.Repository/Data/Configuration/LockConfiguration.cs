using Clay.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clay.Infrastructure.Data.Configuration
{
    public class LockConfiguration : IEntityTypeConfiguration<Lock>
    {
        public void Configure(EntityTypeBuilder<Lock> builder)
        {
            throw new NotImplementedException();
        }
    }
}
