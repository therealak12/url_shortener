using src.Models;

namespace src.Services.Bases
{
	public interface IUrlService
	{
		string MapToShort(string longUrl);

		void SaveUrlMap(UrlResponse urlResponse);

		UrlResponse GetSavedUrl(string shortUrl);
	}
}
