using Xunit;
using src.Utils;

namespace tests.UnitTests
{
	public class UrlUtilsTest
	{
		[Fact]
	        public void TestValidUrls()
	        {
			Assert.True(UrlUtils.IsUrlValid("https://www.google.com"));
			Assert.True(UrlUtils.IsUrlValid("https://google.com"));
			Assert.True(UrlUtils.IsUrlValid("http://www.google.com"));
			Assert.True(UrlUtils.IsUrlValid("http://google.com"));
	        }

		[Fact]
		public void TestInvalidUrls()
		{
			Assert.False(UrlUtils.IsUrlValid(""), "Empty url is not a valid one.");
			Assert.False(UrlUtils.IsUrlValid("https//"), "Empty url is not a valid one");
			Assert.False(UrlUtils.IsUrlValid("https//www"), "Incomplete url");
			Assert.False(UrlUtils.IsUrlValid("https//www..google"), "Incomplete and Invalid url");
		}

		[Fact]
		public void TestGetIdn()
		{
			Assert.Equal("http://xn--mgbmg1g.xn--wgb4b2sveb/", UrlUtils.GetIdn("http://احمد.کریمی"));
			Assert.Equal("https://www.google.com/", UrlUtils.GetIdn("https://www.google.com/"));
		}
	}
}
