namespace src.Models
{
	public class UrlResponse
	{
		public string LongUrl { get; set; }
		public string ShortUrl { get; set; }

		public UrlResponse(string LongUrl, string ShortUrl)
		{
			this.LongUrl = LongUrl;
			this.ShortUrl = ShortUrl;
		}
	}
}
