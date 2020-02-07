using System;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using tests.IntegrationTests.Infrastructure;
using src;
using src.Models;

namespace tests.IntegrationTests
{
	public class UrlControllerTest : IClassFixture<TestHost<Startup>>
	{
		private readonly HttpClient client;
		private readonly TestHost<Startup> server;

		public UrlControllerTest(TestHost<Startup> server)
		{
			this.server = server;
			client = server.CreateClient(new WebApplicationFactoryClientOptions{AllowAutoRedirect = false});
		}

		[Fact]
		public async Task CheckRedirection()
		{
			var response = await client.GetAsync("/GGGGGGGG");

			Assert.Equal(HttpStatusCode.Found, response.StatusCode);
			Assert.Equal("https://www.google.com/search?q=google", response.Headers.Location.AbsoluteUri);

			response = await client.GetAsync("/WWWWWWWW");

			Assert.Equal(HttpStatusCode.Found, response.StatusCode);
			Assert.Equal("https://en.wikipedia.org/wiki/Hashtag#In_popular_culture", response.Headers.Location.AbsoluteUri);
		}

		[Fact]
		public async Task CheckNoRedirection()
		{
			var response = await client.GetAsync("/lakjf");

			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
			Assert.Null(response.Headers.Location);
		}

		[Fact]
		public async Task CheckOkShortening()
		{
			UrlRequest request = new UrlRequest(){LongUrl = "https://www.google.com/"};
			var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

			var response = await client.PostAsync("/urls", stringContent);

			response.EnsureSuccessStatusCode();

			var stringResponse = await response.Content.ReadAsStringAsync();
			var urlResponse = JsonConvert.DeserializeObject<UrlResponse>(stringResponse);

			Assert.Equal(request.LongUrl, urlResponse.LongUrl);
			var match = Regex.Match(urlResponse.ShortUrl, @"^http:\/\/localhost:5000\/[a-z]{8}$", RegexOptions.IgnoreCase);
			Assert.True(match.Success && match.Length == 30);
		}

		[Fact]
		public async Task CheckBadShortening()
		{
			var request = new UrlRequest(){LongUrl = "https://www"};
			var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

			var response = await client.PostAsync("/urls", stringContent);
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}
	}
}
