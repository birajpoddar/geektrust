using GeekTrust.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GeekTrust.Models
{
    class DoReMiContext : DbContext, IDbContext
	{
		private readonly ILogger _logger;

		public DoReMiContext(DbContextOptions options, ILogger<DoReMiContext> logger) : base(options)
		{
			_logger = logger;
			_logger.LogInformation("DBContext Initialized");
		}

        public DbSet<Plan> Plans { get; set; }
        public DbSet<TopUp> TopUps { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			// Defining Unique Keys for both Tables
			modelBuilder.Entity<Plan>().HasAlternateKey(p => new { p.Name, p.Type });
			modelBuilder.Entity<TopUp>().HasAlternateKey(t => new { t.Name, t.NumberOfDevicesSupported });
        }

		public new void SaveChanges()
        {
			base.SaveChanges();
        }
    }
}

