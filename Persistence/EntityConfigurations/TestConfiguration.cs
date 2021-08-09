using Crawler.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crawler.Persistence.EntityConfigurations
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.Property(x => x.Url)
                .HasMaxLength(2048);
            builder.Property(x => x.SaveTime)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql
                ("GETUTCDATE()");
        }
    }
}
