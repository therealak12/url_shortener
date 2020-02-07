using System;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Http;

namespace src.Utils
{
	public static class UrlUtils
	{
		public static bool IsUrlValid(string url)
		{
			Uri uriResult;
	                bool hasValidScheme = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
        	            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

			string url_validator = @"(((http|https|news|ftp|file)\:\/\/){0,1}[0-9a-z]+((\.|\@)[0-9a-z]+)+(\/.+)*)(\/)*";
			var match = Regex.Match(url, url_validator);
			return match.Success && match.Value.Length == url.Length && hasValidScheme;
		}
	}
}
