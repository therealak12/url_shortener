using System.Collections.Generic;
using src.Contexts;
using src.Models;
using Microsoft.EntityFrameworkCore;

namespace tests.Utils
{
	public static class DbUtils
	{
		public static void InitializeDbForTests(AppDbContext dbContext)
		{
			foreach (var item in GetSeedingUrlResponses())
			{
				dbContext.UrlResponses.Add(item);
			}

			dbContext.SaveChanges();
		}

		public static void ReinitializeDbForTests(AppDbContext dbContext)
		{
			dbContext.UrlResponses.RemoveRange(dbContext.UrlResponses);
			InitializeDbForTests(dbContext);
		}

		public static List<UrlResponse> GetSeedingUrlResponses()
		{
			return new List<UrlResponse>()
			{
				new UrlResponse(){ LongUrl = "https://www.google.com/search?q=google", ShortUrl = "GGGGGGGG" },
				new UrlResponse(){ LongUrl = "https://www.tarafdari.com", ShortUrl = "TTTTTTTT" },
				new UrlResponse(){ LongUrl = "https://docs.microsoft.com/en-us/", ShortUrl = "DDDDDDDD" },
				new UrlResponse(){ LongUrl = "https://en.wikipedia.org/wiki/Hashtag#In_popular_culture", ShortUrl = "WWWWWWWW" }
			};
		}
	}
}
