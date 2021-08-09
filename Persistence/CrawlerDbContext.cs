using Microsoft.EntityFrameworkCore;
using Crawler.Models;
using Crawler.Persistence.EntityConfigurations;
using System.Data;

namespace Crawler.Persistence
{
    public class CrawlerDbContext : DbContext, IEfRepositoryDbContext
    { 
        public CrawlerDbContext(DbContextOptions<CrawlerDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Test> Tests { get; set; }
        public DbSet<TestResult> TestResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TestConfiguration());
            modelBuilder.ApplyConfiguration(new TestResultConfiguration());
        }
    }
}
