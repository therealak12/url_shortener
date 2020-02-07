using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Http;

namespace src.Utils
{
	public static class UrlUtils
	{
		public static bool IsUrlValid(string url)
		{
			Uri uri;
			bool hasValidScheme = Uri.TryCreate(url, UriKind.Absolute, out uri)
        	            && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
			if (!hasValidScheme)
			{
				return false;
			}
			url = GetIdn(url);

			string url_validator = @"(((http|https)\:\/\/){0,1}((xn\-\-){0,1}[0-9a-z]+)((\.|\@)((xn\-\-){0,1}[0-9a-z]+))+(\/.+)*)(\/)*";
			var match = Regex.Match(url, url_validator, RegexOptions.IgnoreCase);
			return match.Success && match.Value.Length == url.Length;
		}

		public static String GetIdn(string url)
		{	
			UriBuilder builder = new UriBuilder(url);
			builder.Host = builder.Uri.IdnHost;
			return builder.Uri.AbsoluteUri;
		}
	}
}
