using Microsoft.AspNetCore.Mvc;
using System;
using src.Services.Bases;
using src.Models;
using src.Utils;
using Newtonsoft.Json;

namespace src.Controllers {
	[Route("")]
	public class UrlsController : Controller {
		private readonly IUrlService urlService;

		public UrlsController(IUrlService urlService)
		{
			this.urlService = urlService;
		}

		[HttpPost("urls")]
		public IActionResult CreateShortUrl([FromBody] string requestJson)
		{
			try{
				UrlRequest request = JsonConvert.DeserializeObject<UrlRequest>(requestJson);
				string longUrl = request.LongUrl;
				if (!longUrl.Contains("http", StringComparison.OrdinalIgnoreCase))
				{
					longUrl = "http://" + longUrl;
				}
				if (!UrlUtils.IsUrlValid(longUrl))
				{
					return BadRequest();
				}
				longUrl = UrlUtils.GetIdn(longUrl);

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
			catch(Exception e)
			{
				Console.WriteLine("invalid format for request. the error message is {0}", e.Message);
				return BadRequest();
			}	
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
