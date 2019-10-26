using System;
using System.Collections.Generic;
using System.Text;
using TweetSharp;

namespace TwitterDealer.TwitterApi
{
	public static class AuthInit
	{
		public static TwitterService TwitterService { get; private set; }

		public static void AuthenticateTwitter()
		{
			TwitterService = new TwitterService(AuthSettings.ConsumerApiKey, AuthSettings.ConsumerApiSecretKey);

			TwitterService.AuthenticateWith(AuthSettings.AccessToken, AuthSettings.AccessTokenSecret);
		}
	}
}
