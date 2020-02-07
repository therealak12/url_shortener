using Xunit;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using src.Services.Bases;
using src.Services;
using src.Contexts;
using src.Models;

namespace tests.UnitTests
{
	public class UrlServiceTest
	{
		private readonly IUrlService urlService;

		public UrlServiceTest()
		{
			var services = new ServiceCollection();
	    		services.AddDbContext<AppDbContext>(options => options.UseNpgsql("Host=localhost;Database=url_shortener_test;Username=postgres;Password=postgres"));
			services.AddScoped<IUrlService, UrlService>();
			var serviceProvider = services.BuildServiceProvider();
			urlService = serviceProvider.GetService<IUrlService>();
		}

		[Fact]
		public void TestMapToShort()
		{
			string queryUrl = "https://www.example.com/bike.php?board=airport";
			string boolLogicUrl = "https://www.example.com/bike.php?board=airport&aftermath=branch";
			string fragmentUrl = "http://example.com/belief/bat.html#bite";

			Assert.Equal(8, urlService.MapToShort(queryUrl).Length);
			Assert.Equal(8, urlService.MapToShort(boolLogicUrl).Length);
			Assert.Equal(8, urlService.MapToShort(fragmentUrl).Length);
		}

		[Fact]
		public void TestSavingUrl()
		{
			try
			{
				UrlResponse toBeSaved = new UrlResponse(){LongUrl = "https://www.google.com", ShortUrl = "aaaaaaaa"};
				urlService.SaveUrlMap(toBeSaved);
			}
			catch (Exception e)
			{
				Console.WriteLine("Error occured while adding test data, error message is:	{0}", e.Message);
				Console.WriteLine("That might be becuase test data already exists");
			}

			UrlResponse savedResponse = urlService.GetSavedUrl("aaaaaaaa");
			Assert.Equal("https://www.google.com", savedResponse.LongUrl);
			Assert.Equal("aaaaaaaa", savedResponse.ShortUrl);
		}
	}
}
