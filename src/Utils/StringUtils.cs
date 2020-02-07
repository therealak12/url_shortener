using System;

namespace src.Utils
{
	public static class StringUtils
	{
		public static string GetRandomString(int length)
		{
			char[] validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
			char[] randomChars = new char[length];
			Random random = new Random();

			for (int i = 0; i < length; ++i)
			{
				randomChars[i] = validChars[random.Next(validChars.Length)];
			}
			
			return new string(randomChars);
		}
	}
}
