using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using src.Contexts;
using src;
using tests.Utils;

namespace tests.IntegrationTests.Infrastructure
{
	public class TestHost<TStartup> : WebApplicationFactory<Startup>
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services => {
				var descriptor = services.SingleOrDefault(
						d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

				if (descriptor != null)
				{
					services.Remove(descriptor);
				}

				services.AddDbContext<AppDbContext>(options => 
				{
					options.UseInMemoryDatabase("InMemoryDbForTesting");
				});

				var sp = services.BuildServiceProvider();

				using (var scope  = sp.CreateScope())
				{
					var scopedServices = scope.ServiceProvider;
					var dbContext = scopedServices.GetRequiredService<AppDbContext>();
					var logger = scopedServices.GetRequiredService<ILogger<TestHost<TStartup>>>();

					dbContext.Database.EnsureCreated();
					try
					{
						DbUtils.InitializeDbForTests(dbContext);
					}
					catch (Exception e)
					{
						logger.LogError(e, "An error occured seeding the database, Error Message: {ex.Message}");
					}
				}
			});
		}
	}
}
