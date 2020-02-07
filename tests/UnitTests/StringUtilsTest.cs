using System;
using System.Collections.Generic;
using Xunit;
using src.Utils;

namespace tests.UnitTests

{
    public class StringUtilsTest
    {
	[Fact]
        public void TestRandomString()
        {
		string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

		// The random string should be of correct length
		Assert.Equal(8, StringUtils.GetRandomString(8).Length);

		// The random string characters should be chosen from valid characters
		Assert.Contains(StringUtils.GetRandomString(1), validChars);

		// The random string should be different on average 90% of time
		List<string> generatedStrings = new List<string>();
		int testCount = 1000;
		int duplicateCount = 0;
		for (int i = 0; i < testCount; ++i)
		{
			string randomString = StringUtils.GetRandomString(5);
			if (generatedStrings.Contains(randomString))
			{
				duplicateCount += 1;
			}
		}
		Assert.True(duplicateCount / testCount * 100 < 10);
        }	
    }
}
