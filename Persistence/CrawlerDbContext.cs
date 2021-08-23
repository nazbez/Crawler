using Microsoft.EntityFrameworkCore;
using Crawler.DbLogic.Models;
using System.Data;
using System.Reflection;

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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CrawlerDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
