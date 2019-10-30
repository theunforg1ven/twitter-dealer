using System.Collections.Generic;
using TweetSharp;

namespace TwitterDealer.Interfaces
{
	public interface IUserService
	{
		TwitterUser GetUserInfo(string screenName);

		IEnumerable<TwitterStatus> GetUserTweets(string screenName);

		IEnumerable<TwitterMedia> GetUserMedia(string screenName, int mediaCount);
	}
}
