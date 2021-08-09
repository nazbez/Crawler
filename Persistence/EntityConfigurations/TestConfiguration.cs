using Crawler.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crawler.Persistence.EntityConfigurations
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.Property(x => x.Url)
                .HasMaxLength(1024);
            builder.Property(x => x.SaveTime)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql
                ("GETUTCDATE()");
        }
    }
}
