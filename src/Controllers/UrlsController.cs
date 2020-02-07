using Microsoft.AspNetCore.Mvc;
using System;
using src.Services.Bases;
using src.Models;
using src.Utils;

namespace src.Controllers {
	[Route("")]
	public class UrlsController : Controller {
		private readonly IUrlService urlService;

		public UrlsController(IUrlService urlService)
		{
			this.urlService = urlService;
		}

		[HttpPost("urls")]
		public IActionResult CreateShortUrl([FromBody] UrlRequest request)
		{
			string longUrl = request.LongUrl;
			if (!UrlUtils.IsUrlValid(longUrl))
			{
				return BadRequest();
			}

			string shortUrl = urlService.MapToShort(longUrl);
			UrlResponse urlResponse = new UrlResponse()
				{
					LongUrl = longUrl,
					ShortUrl = shortUrl
				};
			urlService.SaveUrlMap(urlResponse);
			
			urlResponse.ShortUrl = "http://localhost:5000/" + shortUrl;
			return Ok(urlResponse);
		}

		[HttpGet("{shortUrl}")]
		public IActionResult RedirectToLong(string shortUrl)
		{
			UrlResponse savedUrl = urlService.GetSavedUrl(shortUrl);
			if (savedUrl == null)
			{
				return NotFound();
			}
			
			string longUrl = savedUrl.LongUrl;
			return Redirect(longUrl);
		}
	}
}
