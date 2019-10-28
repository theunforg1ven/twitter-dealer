using System;
using System.Collections.Generic;
using TweetSharp;

namespace TwitterDealer.Interfaces
{
	public interface IUserService
	{
		TwitterUser GetUserInfo();

		IEnumerable<TwitterStatus> GetUserTweets();

		IEnumerable<TwitterMedia> GetUserMedia();
	}
}
