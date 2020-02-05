namespace src.Models
{
	public class UrlRequest
	{
		public string LongUrl { get; set; }

		public UrlRequest(string LongUrl)
		{
			this.LongUrl = LongUrl;
		}
	}
}
