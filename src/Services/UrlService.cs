using Microsoft.EntityFrameworkCore;
using System.Linq;
using src.Services.Bases;
using src.Contexts;
using src.Utils;
using src.Models;
using System;

namespace src.Services
{
	public class UrlService : IUrlService
	{
		private readonly AppDbContext dbContext;

		public UrlService(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public string MapToShort(string longUrl)
		{
			bool exists;
			string randomString = StringUtils.GetRandomString(8);
			do
			{
				var existsTask = dbContext.UrlResponses.AnyAsync(u => u.ShortUrl == randomString);
				existsTask.Wait();
				exists = existsTask.Result;
			} while (exists);

			return randomString;
		}

		public void SaveUrlMap(UrlResponse urlResponse)
		{
			dbContext.UrlResponses.Add(urlResponse);
			dbContext.SaveChanges();
		}

		public UrlResponse GetSavedUrl(string shortUrl)
		{
			var urlResponse = dbContext.UrlResponses.Find(shortUrl);
			return urlResponse;
		}
	}
}
