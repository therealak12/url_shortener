using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Contexts
{
	public class AppDbContext : DbContext
	{
		public DbSet<UrlResponse> UrlResponses { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<UrlResponse>().ToTable("url_response");
			builder.Entity<UrlResponse>().HasKey(u => u.ShortUrl);
			builder.Entity<UrlResponse>().Property(u => u.ShortUrl).IsRequired();
			builder.Entity<UrlResponse>().Property(u => u.LongUrl).IsRequired();
			builder.Entity<UrlResponse>().HasIndex(u => u.ShortUrl).IsUnique();
		}
	}
}
